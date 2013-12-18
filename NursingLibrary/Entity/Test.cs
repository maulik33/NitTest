using System;

namespace NursingLibrary.Entity
{
    [Serializable]
    public class Test
    {
        public Question Question { get; set; }

        public Product Product { get; set; }
        
        public String Type { get; set; }
        
        public String TestName { get; set; }
        
        public DateTime StartDate { get; set; }
        
        public DateTime EndDate { get; set; }
        
        public int TestId { get; set; }
        
        public int TestNumber { get; set; }
        
        public int TestSubGroup { get; set; }
        
        public int ProgramId { get; set; }
        
        public int UserId { get; set; }
        
        public DateTime StudentStartDate { get; set; }
        
        public DateTime StudentEndDate { get; set; }
        
        public int CohortId { get; set; }
        
        public int GroupId { get; set; }
        
        public DateTime GroupStartDate { get; set; }
        
        public DateTime GroupEndDate { get; set; }
        
        public int ProductId { get; set; }
        
        public DateTime StartDateAll { get; set; }
        
        public DateTime EndDateAll { get; set; }
        
        public String URL { get; set; }
        
        public int PopupHeight { get; set; }
        
        public int PopupWidth { get; set; }
        
        public DateTime ActvationTime { get; set; }
        
        public int TimeActivated { get; set; }
        
        public int SecureTestS { get; set; }
        
        public int SecureTestD { get; set; }
        
        public int ScramblingS { get; set; }
        
        public int ScramblingD { get; set; }
        
        public int RemediationS { get; set; }
        
        public int RemediationD { get; set; }
        
        public int ExplanationS { get; set; }
        
        public int ExplanationD { get; set; }
        
        public int LevelOfDifficultyS { get; set; }
        
        public int LevelOfDifficultyD { get; set; }
        
        public int NursingProcessS { get; set; }
        
        public int NursingProcessD { get; set; }
        
        public int ClinicalConceptsS { get; set; }
        
        public int ClinicalConceptsD { get; set; }
        
        public int DemographicsS { get; set; }
        
        public int DemographicsD { get; set; }
        
        public int ClientNeedsS { get; set; }
        
        public int ClientNeedsD { get; set; }
        
        public int BloomsS { get; set; }
        
        public int BloomsD { get; set; }
        
        public int TopicS { get; set; }
        
        public int TopicD { get; set; }
        
        public int SpecialtyAreaS { get; set; }
        
        public int SpecialtyAreaD { get; set; }
        
        public int SystemS { get; set; }
        
        public int SystemD { get; set; }
        
        public int CriticalThinkingS { get; set; }
        
        public int CriticalThinkingD { get; set; }
        
        public int ReadingS { get; set; }
        
        public int ReadingD { get; set; }
        
        public int MathS { get; set; }
        
        public int MathD { get; set; }
        
        public int WritingS { get; set; }
        
        public int WritingD { get; set; }
        
        public int RemedationTimeS { get; set; }
        
        public int RemedationTimeD { get; set; }
        
        public int ExplanationTimeS { get; set; }
        
        public int ExplanationTimeD { get; set; }
        
        public int TimeStampS { get; set; }
        
        public int TimeStampD { get; set; }
        
        public int ActiveTest { get; set; }
        
        public string DefaultGroup { get; set; }
        
        public string ReleaseStatus { get; set; }

        public int SecondPerQuestion { get; set; }

        public int ProgramofStudyId { get; set; }

        public string ProgramofStudyName { get; set; }
    }
}
