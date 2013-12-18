using System;

namespace NursingLibrary.Entity
{
    [Serializable]
    public class AnswerChoice
    {
        public Question Question { get; set; }

        public int Id { get; set; }
        
        public int AnswerId { get; set; }
        
        public string Aindex { get; set; }
        
        public string Atext { get; set; }
        
        public int Correct { get; set; }
        
        public int AnswerConnectId { get; set; }
        
        public int ActionType { get; set; }
        
        public int InitialPosition { get; set; }
        
        public string Unit { get; set; }

        public string AlternateAText { get; set; }
    }
}