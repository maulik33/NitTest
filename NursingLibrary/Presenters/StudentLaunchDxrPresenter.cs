using System;
using System.Text;
using NursingLibrary.Interfaces;
using NursingLibrary.Presenters.Controller;

namespace NursingLibrary.Presenters
{
    public class StudentLaunchDxrPresenter : StudentPresenter<ILaunchDxrView>
    {
        #region Constructors

        public StudentLaunchDxrPresenter(IStudentAppController appController)
            : base(appController)
        {
        }

        #endregion Constructors

        #region Methods

        public override void OnViewLoaded()
        {
            base.OnViewLoaded();
            string timeStamp = Utilities.Utilities.GetIsoDate(DateTime.Now);
            string dxrHash = Utilities.Utilities.CreateShaHash(View.DxrKey, timeStamp, Student.EnrollmentId);
            string postData = string.Format("dxac=kplogin&eid={0}&cid={1}&ts={2}&st={3}&first_name={4}&last_name={5}",
                                            Student.EnrollmentId, View.ContentId, timeStamp, dxrHash, Student.FirstName,
                                            Student.LastName);
            byte[] postBuffer = Encoding.UTF8.GetBytes(postData);

            string response = Utilities.Utilities.HttpPost(View.DxUrl, postBuffer);
            View.RegisterScript(response);
        }

        #endregion Methods
    }
}