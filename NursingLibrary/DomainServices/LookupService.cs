using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using NursingLibrary.BusinessLayer;
using NursingLibrary.DTC;
using NursingLibrary.Entity;
using NursingLibrary.Interfaces;

namespace NursingLibrary.DomainServices
{
    public class LookupService
    {
        private IAdminService _adminService;
        private ICMSRepository _cmsRepository;

        public LookupService(IAdminService adminService, ICMSRepository cmsRepository)
        {
            _adminService = adminService;
            _cmsRepository = cmsRepository;
        }

        public void SyncQuestionLookupData(Question question)
        {
            Sync(question, CustomFRLookupSyncManager.ProcessingMode.QuestionUpdate);
        }

        public void SyncQuestionTestMappings(IEnumerable<TestQuestion> currentTestQuestions, IEnumerable<TestQuestion> latestTestQuestions)
        {
            IEnumerable<int> deletedQuestions = currentTestQuestions.Select(p => p.QId).Except(latestTestQuestions.Select(n => n.QId));
            IEnumerable<int> insertedQuestions = latestTestQuestions.Select(p => p.QId).Except(currentTestQuestions.Select(n => n.QId));

            foreach (var qId in insertedQuestions)
            {
                SyncQuestionTestMappings(false, qId);
            }

            foreach (var qId in deletedQuestions)
            {
                SyncQuestionTestMappings(true, qId);
            }
        }

        private void SyncQuestionTestMappings(bool isDelete, int qId)
        {
            Question question = _cmsRepository.GetQuestions(string.Empty, qId, string.Empty, false, string.Empty).FirstOrDefault();
            if (question == null)
            {
                throw new ApplicationException(string.Format("{0} returned a null Question object.", qId));
            }

            Sync(question, isDelete ? CustomFRLookupSyncManager.ProcessingMode.RemovedFromTest : CustomFRLookupSyncManager.ProcessingMode.AddedToTest);
        }

        private void Sync(Question question, CustomFRLookupSyncManager.ProcessingMode mode)
        {
            CustomFRLookupData customFRLookupData = _cmsRepository.GetCustomFRLookupMappings(question.Id, question.ProgramofStudyId);
            if (question.RemediationId > 0)
            {
                CustomFRLookupSyncManager customFRSync = new CustomFRLookupSyncManager(mode, question, customFRLookupData);
                customFRSync.AnalyzeQuestion();
                FetchDataOnAnalysisResult(customFRSync, question);
                MapLookups(customFRSync, question);
                UpdateMappings(customFRSync, question.ProgramofStudyId);
                RemovePreviousMappings(customFRSync);
            }
        }

        private void RemovePreviousMappings(CustomFRLookupSyncManager customFRSync)
        {
            foreach (var lookupMapping in customFRSync.GetUpdatedLookupMappings(EntityAction.Delete))
            {
                _cmsRepository.DeleteLookupMapping(lookupMapping.Id);
            }

            // Check if topic needs to be deleted
            Lookup previousTopic = customFRSync.GetAnalysisResults(LookupType.CustomizedFRTopics, EntityAction.Delete);
            if (previousTopic != null)
            {
                _cmsRepository.DeleteLookup(previousTopic.Id);
            }
        }

        private void UpdateMappings(CustomFRLookupSyncManager customFRSync, int programofstudyId)
        {
            LookupType systemCategory, psychiatricCategory, managementofCareCategory;
            LookupTopicTypes(out systemCategory, out psychiatricCategory, out managementofCareCategory, programofstudyId);
            UpdateMappings(customFRSync, systemCategory, programofstudyId);
            UpdateMappings(customFRSync, psychiatricCategory, programofstudyId);
            UpdateMappings(customFRSync, managementofCareCategory, programofstudyId);
        }

        private void UpdateMappings(CustomFRLookupSyncManager customFRSync, LookupType type, int programofstudyId)
        {
            var topicMapping = programofstudyId == 1 ? LookupType.Lookup123TopicMapping : LookupType.Lookup456TopicMapping;
            var remediationMapping = programofstudyId == 1 ? LookupType.Type17QuestionMappingforRemediation : LookupType.Type20QuestionMappingforRemediation;
            var testMapping = programofstudyId == 1 ? LookupType.Type17QuestionMappingforTests : LookupType.Type20QuestionMappingforTests;

            LookupMapping categoryTopicMapping = customFRSync.GetUpdatedLookupMappings(EntityAction.Insert)
                .Where(m => m.Type == topicMapping && m.CategoryType == type).FirstOrDefault();
            int mappingId = 0;
            if (categoryTopicMapping != null)
            {
                mappingId = _cmsRepository.InsertLookupMapping(
                    categoryTopicMapping.Type, categoryTopicMapping.LookupId, categoryTopicMapping.MappedTo);
            }

            IEnumerable<LookupMapping> otherMappings = customFRSync.GetUpdatedLookupMappings(EntityAction.Insert)
                .Where(m => m.CategoryType == type && (m.Type == remediationMapping || m.Type == testMapping));
            foreach (var lookupMapping in otherMappings)
            {
                _cmsRepository.InsertLookupMapping(lookupMapping.Type,
                    lookupMapping.LookupId == 0 ? mappingId : lookupMapping.LookupId,
                    lookupMapping.MappedTo);
            }
        }

        private void FetchDataOnAnalysisResult(CustomFRLookupSyncManager customFRSync, Question question)
        {
            LookupType systemCategory, psychiatricCategory, managementofCareCategory;
            LookupTopicTypes(out systemCategory, out psychiatricCategory, out managementofCareCategory,
                              question.ProgramofStudyId);

            FetchCategoryData(customFRSync, LookupType.CustomizedFRTopics, 0);
            FetchCategoryData(customFRSync, systemCategory, question.SystemId.ToInt());
            FetchCategoryData(customFRSync, psychiatricCategory, question.SpecialtyAreaId.ToInt());
            FetchCategoryData(customFRSync, managementofCareCategory, question.ClientNeedsCategoryId.ToInt());
        }

        private void FetchCategoryData(CustomFRLookupSyncManager customFRSync, LookupType type, int id)
        {
            Lookup lookup = customFRSync.GetAnalysisResults(type, EntityAction.Insert);
            if (lookup != null)
            {
                int lookupId = id;
                Lookup category = null;
                if (type == LookupType.CustomizedFRTopics)
                {
                    Lookup topic = _cmsRepository.GetLookup(lookup.DisplayText, LookupType.CustomizedFRTopics);
                    if (topic == null)
                    {
                        lookupId = _cmsRepository.InsertLookup(0, LookupType.CustomizedFRTopics, lookup.DisplayText, 0);
                        topic = _cmsRepository.GetLookup(lookupId);
                    }

                    lookupId = topic.Id;
                    category = topic;
                }
                else
                {
                    category = _cmsRepository.GetLookup(id, type);
                }

                customFRSync.AssignLookup(type, category);
            }
        }

        private void MapLookups(CustomFRLookupSyncManager customFRSync, Question question)
        {
            IList<LookupMapping> existingCategoryTopicMappingsForNewId = null;
            IList<LookupMapping> prevTopicExistingMappings = null;

            int topicId = customFRSync.GetLookup(LookupType.CustomizedFRTopics).Id;
            LookupType topicMapping = question.ProgramofStudyId == 1 ? LookupType.Lookup123TopicMapping : LookupType.Lookup456TopicMapping;
            existingCategoryTopicMappingsForNewId = _cmsRepository.GetLookupMappings(topicId.ToString(), topicMapping, true);

            Lookup previousTopic = customFRSync.GetAnalysisResults(LookupType.CustomizedFRTopics, EntityAction.Delete);
            if (previousTopic != null)
            {
                prevTopicExistingMappings = _cmsRepository.GetLookupMappings(previousTopic.Id.ToString(),
                    topicMapping, true);
            }

            customFRSync.PerformMappings(existingCategoryTopicMappingsForNewId, prevTopicExistingMappings, question.ProgramofStudyId);
        }

        private void LookupTopicTypes(out LookupType systemCategory, out LookupType psychiatricCategory, out LookupType managementofCareCategory, int programofstudyId)
        {
            systemCategory = programofstudyId == 1 ? LookupType.CustomizedFRSystemCategory : LookupType.PNCustomizedFRSystemCategory;
            psychiatricCategory = programofstudyId == 1 ? LookupType.CustomizedFRPsychiatricCategory : LookupType.PNCustomizedFRPsychiatricCategory;
            managementofCareCategory = programofstudyId == 1 ? LookupType.CustomizedFRManagementofCareCategory : LookupType.PNCustomizedFRManagementofCareCategory;
        }
    }
}
