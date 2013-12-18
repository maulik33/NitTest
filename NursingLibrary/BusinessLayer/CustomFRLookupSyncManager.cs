using System;
using System.Collections.Generic;
using System.Linq;
using NursingLibrary.Common;
using NursingLibrary.DTC;
using NursingLibrary.Entity;
using NursingLibrary.Interfaces;

namespace NursingLibrary.BusinessLayer
{
    internal sealed class CustomFRLookupSyncManager
    {
        // TODO: Improvise the object model.
        private readonly CustomFRLookupData _customFRLookupData;
        private readonly Question _question;
        private IDictionary<LookupType, Lookup> _lookup;
        private IDictionary<LookupMapping, EntityAction> _lookupMapping;
        private IDictionary<Lookup, EntityAction> _lookupAction;
        private ProcessingStage _currentStage;
        private IList<LookupMapping> _existingCategoryTopicMappingsForNewId;
        private IList<LookupMapping> _prevTopicExistingMappings;
        private ProcessingMode _mode;

        internal CustomFRLookupSyncManager(ProcessingMode mode, Question question, CustomFRLookupData customFRLookupData)
        {
            _mode = mode;
            _question = question;
            _customFRLookupData = customFRLookupData;
           _lookup = new Dictionary<LookupType, Lookup>()
                    {
                        {_question.ProgramofStudyId == 1 ? LookupType.CustomizedFRSystemCategory :LookupType.PNCustomizedFRSystemCategory , null},
                        {_question.ProgramofStudyId == 1 ? LookupType.CustomizedFRPsychiatricCategory:LookupType.PNCustomizedFRPsychiatricCategory, null},
                        {question.ProgramofStudyId == 1 ? LookupType.CustomizedFRManagementofCareCategory:LookupType.PNCustomizedFRManagementofCareCategory, null},
                        {LookupType.CustomizedFRTopics, null}
                    };
            _lookupMapping = new Dictionary<LookupMapping, EntityAction>();
            _lookupAction = new Dictionary<Lookup, EntityAction>();
        }

        internal enum ProcessingMode
        {
            QuestionUpdate,
            RemovedFromTest,
            AddedToTest
        }

        private enum ProcessingStage
        {
            Analysis,
            FetchAdditionalData,
            DeleteOrphanRecords
        }

        public CustomFRQuestion CurrentFRCategory { get; private set; }

        public void AnalyzeQuestion()
        {
            Init();

            _currentStage = ProcessingStage.Analysis;
            AnalyzeTopic();
            AnalyzeSystem();
            AnalyzePsychiatric();
            AnalyzeManagementOfCare();

            _currentStage = ProcessingStage.FetchAdditionalData;
        }

        public Lookup GetAnalysisResults(LookupType type, EntityAction actionToFilter)
        {
            return _lookupAction.Where(l => l.Value == actionToFilter && l.Key.Type == type).Select(l => l.Key).SingleOrDefault();
        }

        public void AssignLookup(LookupType type, Lookup lookup)
        {
            if (_currentStage != ProcessingStage.FetchAdditionalData)
            {
                throw new ApplicationException("This method is not supposed to be called during this Stage of processing.");
            }

            if (_lookup[type] != null)
            {
                throw new ApplicationException(string.Format("{0} has already been assigned a valid lookup object. Multiple assignments are not permitted for this operation.", type));
            }

            _lookup[type] = lookup;
        }

        public Lookup GetLookup(LookupType type)
        {
            return _lookup[type];
        }

        public void PerformMappings(IList<LookupMapping> existingCategoryTopicMappingsForNewId,
            IList<LookupMapping> prevTopicExistingMappings,int programofStudyId)
        {
            _existingCategoryTopicMappingsForNewId = existingCategoryTopicMappingsForNewId;
            _prevTopicExistingMappings = prevTopicExistingMappings;

            DoMapping(programofStudyId);
        }

        public IEnumerable<LookupMapping> GetUpdatedLookupMappings(EntityAction action)
        {
            return _lookupMapping.Where(m => m.Value == action).Select(m => m.Key);
        }

        public void AnalyzeTestQuestionMapping(bool isDelete)
        {
            AnalyzeQuestion();
        }

        private void DoMapping(int programofstudyId)
        {
             var systemCategory =programofstudyId == 1 ? LookupType.CustomizedFRSystemCategory: LookupType.PNCustomizedFRSystemCategory;
             var psychiatricCategory = programofstudyId == 1 ? LookupType.CustomizedFRPsychiatricCategory:LookupType.PNCustomizedFRPsychiatricCategory;
             var managementofCareCategory = programofstudyId == 1 ? LookupType.CustomizedFRManagementofCareCategory:LookupType.PNCustomizedFRManagementofCareCategory;

             MapCategoryTopic(systemCategory, _lookup[systemCategory], _lookup[LookupType.CustomizedFRTopics], programofstudyId);
             MapCategoryTopic(psychiatricCategory, _lookup[psychiatricCategory], _lookup[LookupType.CustomizedFRTopics], programofstudyId);
             MapCategoryTopic(managementofCareCategory, _lookup[managementofCareCategory], _lookup[LookupType.CustomizedFRTopics], programofstudyId);
             RemoveUnusedTopicLookup();
        }

        private void RemoveUnusedTopicLookup()
        {
            if (_prevTopicExistingMappings == null
                || _prevTopicExistingMappings.Count == 0)
            {
                return;
            }

            var currentTopicMappings = from m in _prevTopicExistingMappings
                                       join l in _lookup.Values.Where(p => p != null)
                                       on m.LookupId equals l.Id
                                       select m;

            // If other mappings exist for the topic, remove it from delete list
            if (_prevTopicExistingMappings.Except(currentTopicMappings).Count() > 0)
            {
                _lookupAction.Remove(_lookupAction.Where(p => p.Value == EntityAction.Delete
                    && p.Key.Type == LookupType.CustomizedFRTopics).Select(l => l.Key).FirstOrDefault());
            }
        }

        private void MapCategoryTopic(LookupType type, Lookup category, Lookup topic,int programofstudyId)
        {
            bool removePrevMappings = true;

            if (_mode == ProcessingMode.AddedToTest
                || _mode == ProcessingMode.QuestionUpdate)
            {
                removePrevMappings = CreateCategoryTopicMapping(category, topic, type, removePrevMappings, programofstudyId);
            }

            if (removePrevMappings
                && (_mode == ProcessingMode.RemovedFromTest
                || _mode == ProcessingMode.QuestionUpdate))
            {
                RemovePreviousLookupMappings(type);
            }
        }

        private bool CreateCategoryTopicMapping(Lookup category, Lookup topic, LookupType type, bool removePrevMappings, int programofstudyId)
        {
            if (category != null && topic != null)
            {
                EntityAction action = EntityAction.None;
                bool insertRemediationMapping = false;
                bool insertTestMapping = false;

                // Check if mapping already exists
                LookupMapping categoryTopicMapping = null;
                if (_existingCategoryTopicMappingsForNewId != null)
                {
                    categoryTopicMapping = _existingCategoryTopicMappingsForNewId
                        .Where(m => m.LookupId == category.Id && m.MappedTo == topic.Id).FirstOrDefault();
                }

                // Create a new mapping
                if (categoryTopicMapping == null)
                {
                    var topicMapping = programofstudyId == 1
                                           ? LookupType.Lookup123TopicMapping
                                           : LookupType.Lookup456TopicMapping;
                    categoryTopicMapping = new LookupMapping(0, topicMapping, category.Id, topic.Id);
                    categoryTopicMapping.SetCategoryType(type);
                    action = EntityAction.Insert;
                }
                else
                {
                    removePrevMappings = false;
                    CurrentFRCategory.CategoryTopics[type] = categoryTopicMapping;
                }

                _lookupMapping.Add(categoryTopicMapping, action);

                if (action == EntityAction.Insert)
                {
                    insertRemediationMapping = true;
                    insertTestMapping = true;
                }
                else
                {
                    if (_customFRLookupData.IsRemediationMapped)
                    {
                        LookupMapping existingRemediationMapping = _customFRLookupData.RemediationMappings.Values.Where(p => p.MappedTo == _question.Id
                            && p.LookupId == categoryTopicMapping.Id).FirstOrDefault();
                        if (existingRemediationMapping == null)
                        {
                            insertRemediationMapping = true;
                        }
                        else
                        {
                            CurrentFRCategory.RemediationMappings[type] = existingRemediationMapping;
                        }
                    }

                    if (_customFRLookupData.IsTestMapped)
                    {
                        LookupMapping existingTestMapping = _customFRLookupData.TestMappings.Values.Where(p => p.MappedTo == _question.Id
                            && p.LookupId == categoryTopicMapping.Id).FirstOrDefault();
                        if (existingTestMapping == null)
                        {
                            insertTestMapping = true;
                        }
                        else
                        {
                            CurrentFRCategory.TestMappings[type] = existingTestMapping;
                        }
                    }
                }

                if (_customFRLookupData.IsRemediationMapped && insertRemediationMapping)
                {
                    var remMapping = programofstudyId == 1
                                                 ? LookupType.Type17QuestionMappingforRemediation
                                                 : LookupType.Type20QuestionMappingforRemediation;
                    LookupMapping remediationMapping = new LookupMapping(0, remMapping, categoryTopicMapping.Id, _question.Id);
                    remediationMapping.SetCategoryType(type);
                    _lookupMapping.Add(remediationMapping, EntityAction.Insert);
                }

                if (_customFRLookupData.IsTestMapped && insertTestMapping)
                {
                    var questMapping = programofstudyId == 1
                                           ? LookupType.Type17QuestionMappingforTests
                                           : LookupType.Type20QuestionMappingforTests;
                    LookupMapping testMapping = new LookupMapping(0, questMapping, categoryTopicMapping.Id, _question.Id);
                    testMapping.SetCategoryType(type);
                    _lookupMapping.Add(testMapping, EntityAction.Insert);
                }
            }

            return removePrevMappings;
        }

        private void RemovePreviousLookupMappings(LookupType type)
        {
            // Remove previous mappings
            Lookup prevLookup = CurrentFRCategories().Where(c => c.Type == type).FirstOrDefault();

            if (prevLookup != null
                && CurrentFRCategory.Topic != null)
            {
                var prevCategoryTopicMapping = _customFRLookupData.CategoryTopicMappings
                    .Where(m => m.Value.Key == prevLookup && m.Value.Value == CurrentFRCategory.Topic).Select(m => m.Key).FirstOrDefault();

                if (CurrentFRCategory.RemediationMappings[type] == null)
                {
                    // Remove previous Remediation mappings
                    var prevRemediationMapping = _customFRLookupData.RemediationMappings.Values
                        .Where(m => m.LookupId == prevCategoryTopicMapping.Id && m.MappedTo == _question.Id).FirstOrDefault();
                    if (prevRemediationMapping != null)
                    {
                        _lookupMapping.Add(prevRemediationMapping, EntityAction.Delete);
                    }
                }

                if (CurrentFRCategory.TestMappings[type] == null)
                {
                    // Remove previous Test mappings
                    var prevTestMapping = _customFRLookupData.TestMappings.Values
                        .Where(m => m.LookupId == prevCategoryTopicMapping.Id && m.MappedTo == _question.Id).FirstOrDefault();
                    if (prevTestMapping != null)
                    {
                        _lookupMapping.Add(prevTestMapping, EntityAction.Delete);
                    }
                }

                if (CurrentFRCategory.RemediationMappings[type] == null
                    && CurrentFRCategory.TestMappings[type] == null)
                {
                    // Check if there are any more mappings for Category - Topic combination
                    if (_customFRLookupData.RemediationMappings.Values.Where(m => m.MappedTo != _question.Id
                            && m.LookupId == prevCategoryTopicMapping.Id).Count() == 0
                        && _customFRLookupData.TestMappings.Values.Where(m => m.MappedTo != _question.Id
                            && m.LookupId == prevCategoryTopicMapping.Id).Count() == 0)
                    {
                        _lookupMapping.Add(prevCategoryTopicMapping, EntityAction.Delete);
                    }
                }
            }
        }

        private IEnumerable<Lookup> CurrentFRCategories()
        {
            if (CurrentFRCategory.System != null)
            {
                yield return CurrentFRCategory.System;
            }

            if (CurrentFRCategory.Psychiatric != null)
            {
                yield return CurrentFRCategory.Psychiatric;
            }

            if (CurrentFRCategory.ManagementOfCare != null)
            {
                yield return CurrentFRCategory.ManagementOfCare;
            }
        }

        private void Init()
        {
            CurrentFRCategory = new CustomFRQuestion(_question.Id,_question.ProgramofStudyId);

            CurrentFRCategory.Topic = (from m in _customFRLookupData.RemediationMappings.Values
                                       join m2 in _customFRLookupData.CategoryTopicMappings
                                       on m.LookupId equals m2.Key.Id
                                       join t in _customFRLookupData.Topics.Values
                                       on m2.Value.Value.Id equals t.Id
                                       where m.MappedTo == _question.Id
                                       select t).FirstOrDefault();

            CurrentFRCategory.System = AssignCategory(_customFRLookupData.SystemCategories);
            CurrentFRCategory.Psychiatric = AssignCategory(_customFRLookupData.PsychiatricCategory);
            CurrentFRCategory.ManagementOfCare = AssignCategory(_customFRLookupData.ManagementOfCareCategory);
        }

        private Lookup AssignCategory(IDictionary<int, Lookup> categories)
        {
            return (from m in _customFRLookupData.RemediationMappings.Values
                    join m2 in _customFRLookupData.CategoryTopicMappings
                    on m.LookupId equals m2.Key.Id
                    join c in categories.Values
                    on m2.Value.Key.Id equals c.Id
                    where m.MappedTo == _question.Id
                    select c).FirstOrDefault();
        }

        private void AnalyzeTopic()
        {
            EntityAction action = EntityAction.None;
            string newTopicTitle = _question.RemediationObj.TopicTitle;
            if (CurrentFRCategory.Topic == null
                && false == string.IsNullOrEmpty(newTopicTitle))
            {
                action = EntityAction.Insert;
            }
            else if (newTopicTitle != CurrentFRCategory.Topic.DisplayText
                && false == string.IsNullOrEmpty(newTopicTitle))
            {
                action = EntityAction.Update;
            }
            else if (true == string.IsNullOrEmpty(newTopicTitle))
            {
                action = EntityAction.Delete;
            }

            CreateLookupObject(LookupType.CustomizedFRTopics, 0, newTopicTitle, CurrentFRCategory.Topic, action);
        }

        private void AnalyzeSystem()
        {
            var systemCategory = _question.ProgramofStudyId == 1
                                     ? LookupType.CustomizedFRSystemCategory
                                     : LookupType.PNCustomizedFRSystemCategory;
            AnalyzeCategory(systemCategory, _question.SystemId.ToInt(), CurrentFRCategory.System, null);
        }

        private void AnalyzeManagementOfCare()
        {
            if (_question.ProgramofStudyId == 1)
            {
                AnalyzeCategory(LookupType.CustomizedFRManagementofCareCategory, _question.ClientNeedsCategoryId.ToInt(),
                 CurrentFRCategory.ManagementOfCare, KTPApp.ManagementOfCareCategoryId);
            }
            else
            {
                AnalyzeCategory(LookupType.PNCustomizedFRManagementofCareCategory, _question.ClientNeedsCategoryId.ToInt(),
                 CurrentFRCategory.ManagementOfCare, KTPApp.PNManagementOfCareCategoryId);
            }
        }

        private void AnalyzePsychiatric()
        {
          if (_question.ProgramofStudyId == 1)
            {
                AnalyzeCategory( LookupType.CustomizedFRPsychiatricCategory, _question.SpecialtyAreaId.ToInt(),
                CurrentFRCategory.Psychiatric, KTPApp.PsychiatricCategoryId); 
            }
            else
            {
                 AnalyzeCategory( LookupType.PNCustomizedFRPsychiatricCategory, _question.SpecialtyAreaId.ToInt(),
                CurrentFRCategory.Psychiatric, KTPApp.PNPsychiatricCategoryId); 
            }
        }

        private void CreateLookupObject(LookupType type, int originalId, string displayText, Lookup previousLookup, EntityAction analysisResult)
        {
            if (analysisResult == EntityAction.None)
            {
                _lookup[type] = previousLookup;
            }
            else
            {
                if (analysisResult == EntityAction.Insert
                    || analysisResult == EntityAction.Update)
                {
                    Lookup lookup = new Lookup(0, type, originalId, displayText);
                    _lookupAction.Add(lookup, EntityAction.Insert);
                }

                if (type == LookupType.CustomizedFRTopics
                    && (analysisResult == EntityAction.Update
                    || analysisResult == EntityAction.Delete))
                {
                    _lookupAction.Add(previousLookup, EntityAction.Delete);
                }
            }
        }

        private void AnalyzeCategory(LookupType type, int newCategoryId, Lookup categoryLookup, int? filteredCategoryId)
        {
            EntityAction action = EntityAction.None;
            int currentCategoryId = (categoryLookup == null) ? 0 : categoryLookup.Id;
            if (newCategoryId != currentCategoryId)
            {
                if (newCategoryId > 0)
                {
                    if (currentCategoryId == 0)
                    {
                        if (false == filteredCategoryId.HasValue || newCategoryId == filteredCategoryId.Value)
                        {
                            action = EntityAction.Insert;
                        }
                    }
                    else
                    {
                        if (false == filteredCategoryId.HasValue || newCategoryId == filteredCategoryId.Value)
                        {
                            action = EntityAction.Update;
                        }
                        else
                        {
                            action = EntityAction.Delete;
                        }
                    }
                }
                else
                {
                    if (currentCategoryId > 0)
                    {
                        action = EntityAction.Delete;
                    }
                }
            }

            CreateLookupObject(type, newCategoryId, string.Empty, categoryLookup, action);
        }
    }
}
