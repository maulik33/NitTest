using System.ComponentModel.DataAnnotations;

namespace NursingLibrary.Entity
{
    public class LtiProvider
    {
        [Display(Name = "Key")]
        public string ConsumerKey { get; set; }

        [Display(Name = "Secret")]
        public string ConsumerSecret { get; set; }

        [Display(Name = "Custom Parameters")]
        public string CustomParameters { get; set; }

        public string Description { get; set; }

        [Display(Name = "Resource Link Title")]
        public string Title { get; set; }

        [Display(Name = "Active")]
        public bool Active { get; set; }

        public int Id { get; set; }

        public bool IsLtiLink
        {
            get { return !string.IsNullOrEmpty(ConsumerKey) && !string.IsNullOrEmpty(ConsumerSecret); }
        }

        public string Name { get; set; }

        [Display(Name = "URL")]
        public string Url { get; set; }
    }

    public class LtiResourceInfo
    {
        public string UserId { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string UserType { get; set; }

        public int ProductId { get; set; }

        public int TestId { get; set; }

        public string TestName { get; set; }

        public string InstitutionId { get; set; }

        public string LaunchPresentationReturnUrl { get; set; }

        public string StudentId { get; set; }
    }
}
