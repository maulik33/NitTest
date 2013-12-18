using System;
using NursingLibrary.Interfaces;

namespace NursingLibrary.Entity
{
    public class Question
    {
        public Product Product { get; set; }

        public Test Test { get; set; }

        public int RemediationTime { get; set; }

        public int ExplanationTime { get; set; }

        public int TimeSpendForQuestion { get; set; }

        public string Stem { get; set; }

        public string Url { get; set; }

        public string Explanation { get; set; }

        public int RemediationId { get; set; }

        public Remediation RemediationObj { get; set; }

        public QuestionType Type { get; set; }

        public QuestionFileType FileType { get; set; }

        public string TopicTitleId { get; set; }

        public int Id { get; set; }

        public int UserTestId { get; set; }

        public string ItemTitle { get; set; }

        public QuestionPointer Pointer { get; set; }

        public int QuestionNumber { get; set; }

        public int Active { get; set; }

        public string AnswserTrack { get; set; }

        public string OrderedIndexes { get; set; }

        public int Correct { get; set; }

        public string AnswerChanges { get; set; }

        public string QuestionId { get; set; }

        public int SourceNumber { get; set; }

        public string XMLQID { get; set; }

        public string QuestionType { get; set; }

        public string ClientNeedsId { get; set; }

        public string ClientNeedsCategoryId { get; set; }

        public string NursingProcessId { get; set; }

        public string LevelOfDifficultyId { get; set; }

        public string DemographicId { get; set; }

        public string CognitiveLevelId { get; set; }

        public string CriticalThinkingId { get; set; }

        public string IntegratedConceptsId { get; set; }

        public string ClinicalConceptsId { get; set; }

        public string Stimulus { get; set; }

        public string Remediation { get; set; }

        public string SpecialtyAreaId { get; set; }

        public string SystemId { get; set; }

        public string ReadingCategoryId { get; set; }

        public string ReadingId { get; set; }

        public string WritingCategoryId { get; set; }

        public string WritingId { get; set; }

        public string MathCategoryId { get; set; }

        public string MathId { get; set; }

        public string ProductLineId { get; set; }

        public string TypeOfFileId { get; set; }

        public string Statisctics { get; set; }

        public string CreatorId { get; set; }

        public string DateCreated { get; set; }

        public string EditorId { get; set; }

        public string DateEdited { get; set; }

        public string EditorId_2 { get; set; }

        public string DateEdited_2 { get; set; }

        public string Source_SBD { get; set; }

        public string WhoOwns { get; set; }

        public string WhereUsed { get; set; }

        public string PointBiserialsId { get; set; }

        public string Feedback { get; set; }

        public int Deleted { get; set; }

        public string TestNumber { get; set; }

        public float Q_Norming { get; set; }

        public string ReleaseStatus { get; set; }

        public string ExhibitTab1 { get; set; }

        public string ExhibitTab2 { get; set; }

        public string ExhibitTab3 { get; set; }

        public string ListeningFileUrl { get; set; }

        public string ExhibitTitle1 { get; set; }

        public string ExhibitTitle2 { get; set; }

        public string ExhibitTitle3 { get; set; }

        public string QuestionNumberString { get; set; }

        public string AlternateStem { get; set; }

        public string AccreditationCategoriesId { get; set; }

        public string QSENKSACompetenciesId { get; set; }

        public string CorrectAnswer { get; set; }

        public string Unit { get; set; }

        public int ProgramofStudyId { get; set; }

        public string ProgramofStudyName { get; set; }

        public int CreatedBy { get; set; }

        public DateTime? CreatedOn { get; set; }

        public int? UpdatedBy { get; set; }

        public DateTime? UpdatedOn { get; set; }

        public int ReleasedBy { get; set; }

        public DateTime? ReleaseDate { get; set; }

        public string ConceptsId { get; set; }
    }
}
