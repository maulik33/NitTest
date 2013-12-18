using System.Linq;
using System.Security.Cryptography;
using System.Text;
using NursingLibrary.Common;
using NursingLibrary.Interfaces;
using NursingLibrary.Presenters.Controller;

namespace NursingLibrary.Presenters
{
    public class StudentPayPresenter : StudentPresenter<IStudentPayView>
    {
        #region Constructors

        public StudentPayPresenter(IStudentAppController appController)
            : base(appController)
        {
        }

        #endregion Constructors

        #region Methods

        public override void OnViewLoaded()
        {
            base.OnViewLoaded();
            View.AddPostToKaptestScript(Student.KaplanUserId, Student.EnrollmentId, GetToken());
        }

        private string GetToken()
        {
            StringBuilder encryptedToken = new StringBuilder();
            if (Student != null)
            {
                MD5 md = MD5.Create();
                string token = Student.KaplanUserId + Student.EnrollmentId + KTPApp.AccountParameter;
                byte[] inputBytes = Encoding.ASCII.GetBytes(token);
                byte[] hash = md.ComputeHash(inputBytes);
                for (int i = 0; i < hash.Length; i++)
                {
                    encryptedToken.Append(hash[i].ToString("x2"));
                }
            }

            return encryptedToken.ToString();
        }

        #endregion Methods
    }
}
