namespace NursingLibrary.Entity
{
    public class UserAnswer
    {
        public int ID { get; set; }

        public int UserTestID { get; set; }
        
        public int QID { get; set; }
        
        public int AnswerID { get; set; }
        
        public string AIndex { get; set; }
        
        public string AText { get; set; }
        
        public int Correct { get; set; }
        
        public int AnswerConnectID { get; set; }
        
        public int AType { get; set; }
        
        public int initialPos { get; set; }
        
        public string Stimulus { get; set; }
        
        public string Unit { get; set; }

        public string AlternateAText { get; set; }

        public string ScrambledAIndex { get; set; }
    }
}
