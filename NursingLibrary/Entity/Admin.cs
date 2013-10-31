using System;

namespace NursingLibrary.Entity
{
    [Serializable]
    public class Admin
    {
        public int UserId { get; set; }

        public string UserName { get; set; }
        
        public string UserPassword { get; set; }
        
        public string FirstName { get; set; }
        
        public string LastName { get; set; }
        
        public string Email { get; set; }
        
        public string AdminType { get; set; }
        
        public int SecurityLevel { get; set; }
        
        public DateTime? AdminDeleteDate { get; set; }
        
        public DateTime? AdminCreateDate { get; set; }
        
        public DateTime? AdminUpdateDate { get; set; }
        
        public int AdminCreateUser { get; set; }
        
        public int AdminUpdateUser { get; set; }
        
        public int AdminDeleteUser { get; set; }
        
        public Institution Institution { get; set; }
        
        public bool UploadAccess { get; set; }

        public string FullName
        {
            get
            {
                return LastName + " " + FirstName;
            }
        }
    }
}
