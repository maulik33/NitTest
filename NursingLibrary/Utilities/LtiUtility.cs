using LtiLibrary.Common;
using LtiLibrary.Consumer;
using NursingLibrary.Common;
using NursingLibrary.Entity;
using NursingLibrary.Interfaces;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;

namespace NursingLibrary.Utilities
{
    public class LtiUtility
    {
        /// <summary>
        ///  returns LIT request object which includes Consumer resource ,oauth required parameters for Authentication
        ///  and lti supported parameters information
        /// </summary>
        /// <param name="provider">Provider Information Object</param>
        /// <param name="ltiResourceInfo">Consumer Resources Information Object</param>
        /// <returns>LTI request object</returns>
        public static LtiOutboundRequestViewModel CreateLtiRequest(LtiProvider provider, LtiResourceInfo ltiResourceInfo)
        {
            var uniqueResourceId = string.Format("{0}_{1}_{2}", ltiResourceInfo.ProductId, ltiResourceInfo.UserId, ltiResourceInfo.TestId);
            var ltiRequest = new LtiOutboundRequest
            {
                ConsumerKey = provider.ConsumerKey,
                ConsumerSecret = provider.ConsumerSecret,
                ResourceLinkId = uniqueResourceId,
                Url = provider.Url
            };

            // Tool
            ltiRequest.ToolConsumerInfoProductFamilyCode = "Kaplan Nursing IT";
            ltiRequest.ToolConsumerInfoVersion = Assembly.GetExecutingAssembly().GetName().Version.ToString();

            // Context
            ltiRequest.ContextId = ltiResourceInfo.ProductId.ToString(CultureInfo.InvariantCulture); 
            ltiRequest.ContextTitle = ltiResourceInfo.TestName;

            // Instance
           // ltiRequest.ToolConsumerInstanceGuid = "~/".ToAbsoluteUrl();
            var titles = Assembly.GetExecutingAssembly().GetCustomAttributes<AssemblyTitleAttribute>();
            var assemblyTitleAttributes = titles as AssemblyTitleAttribute[] ?? titles.ToArray();
            if (assemblyTitleAttributes.Any())
            {
                ltiRequest.ToolConsumerInstanceName = assemblyTitleAttributes.First().Title;
            }
            ltiRequest.ResourceLinkTitle = provider.Title;
            ltiRequest.ResourceLinkDescription = provider.Description;

            // User - Student / Instructor
            ltiRequest.UserId = ltiResourceInfo.UserId.ToString(CultureInfo.InvariantCulture);
            ltiRequest.AddRoles(GetLtiRolesForUser(ltiResourceInfo.UserType));

            if (!string.IsNullOrEmpty(ltiResourceInfo.LaunchPresentationReturnUrl))
                ltiRequest.LaunchPresentationReturnUrl = "~/".ToAbsoluteUrl() + ltiResourceInfo.LaunchPresentationReturnUrl;


            // Outcomes

            //ltiRequest.LisOutcomeServiceUrl = ""; // Post URL to Nursing (Callback)
            //ltiRequest.LisResultSourcedId = "1";

            var userIdParam = "custom_student_id=" + ltiResourceInfo.StudentId;
            ltiRequest.AddCustomParameters(userIdParam);
            if (ltiResourceInfo.TestId != 0)
            {
                var testIdParam = "custom_test_id=" + ltiResourceInfo.TestId.ToString(CultureInfo.InvariantCulture);
                ltiRequest.AddCustomParameters(testIdParam);
            }
            if (!string.IsNullOrEmpty(ltiResourceInfo.FirstName) && !string.IsNullOrEmpty(ltiResourceInfo.LastName))
            {
                var firstName = "custom_first_name=" + ltiResourceInfo.FirstName;
                var lastName = "custom_last_name=" + ltiResourceInfo.LastName;

                ltiRequest.AddCustomParameters(firstName);
                ltiRequest.AddCustomParameters(lastName);
            }

            var institutionIdParam = "custom_institution_id=" + ltiResourceInfo.InstitutionId;
            ltiRequest.AddCustomParameters(institutionIdParam);
            return ltiRequest.GetLtiRequestModel();
        }

        private static IList<LtiRoles> GetLtiRolesForUser(string userType)
        {
            var roles = new List<LtiRoles>
                {
                    userType == UserType.Student.ToString() ? LtiRoles.Learner : LtiRoles.Instructor
                };
            return roles;
        }
    }
}