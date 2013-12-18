using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Globalization;
using System.Linq;
using System.Web;

namespace LtiLibrary.Common
{
    public class LtiRequest
    {
        private static readonly Dictionary<string, LtiRoles> Urns = new Dictionary<string, LtiRoles>(StringComparer.InvariantCultureIgnoreCase);

        /// <summary>
        /// Primarily used to build a lookup table of role URNs
        /// </summary>
        static LtiRequest()
        {
            var type = typeof(LtiRoles);
            foreach (LtiRoles ltiRole in Enum.GetValues(type))
            {
                var memInfo = type.GetMember(ltiRole.ToString());
                var attributes = memInfo[0].GetCustomAttributes(typeof(UrnAttribute), false);
                var urn = ((UrnAttribute)attributes[0]).Urn;
                Urns.Add(urn, ltiRole);
            }
        }

        public LtiRequest()
        {
            CustomParameters = new NameValueCollection();
            LaunchPresentationLocale = CultureInfo.CurrentCulture.Name;
            LtiMessageType = LtiConstants.LtiMessageType;
            LtiVersion = LtiConstants.LtiVersion;
            Roles = new List<LtiRoles>();
        }

        /// <summary>
        /// This is an opaque identifier that uniquely identifies the context that contains the link being launched. This parameter is recommended.
        /// </summary>
        public string ContextId { get; set; }

        /// <summary>
        /// A plain text label for the context – intended to fit in a column. This parameter is recommended.
        /// </summary>
        public string ContextLabel { get; set; }

        /// <summary>
        /// A plain text title of the context – it should be about the length of a line. This parameter is recommended.
        /// </summary>
        public string ContextTitle { get; set; }

        /// <summary>
        /// This string is a comma-separated list of URN values that identify the type of context. At a minimum, the list MUST include a URN value drawn from the LIS vocabulary (see Appendix A). The assumed namespace of these URNs is the LIS vocabulary so TCs can use the handles when the intent is to refer to an LIS context type. If the TC wants to include a context type from another namespace, a fully-qualified URN should be used. This parameter is optional.
        /// </summary>
        public LisContextTypes? ContextType { get; set; }

        public NameValueCollection CustomParameters { get; private set; }

        public string CustomParametersAsQuerystring
        {
            get
            {
                var httpValueCollection = HttpUtility.ParseQueryString(string.Empty);
                httpValueCollection.Add(CustomParameters);
                return httpValueCollection.ToString();
            }
            set { CustomParameters = HttpUtility.ParseQueryString(value); }
        }

        /// <summary>
        /// Language, country and variant as represented using the IETF Best Practices for Tags for Identifying Languages (BCP-47) available at http://www.rfc-editor.org/rfc/bcp/bcp47.txt.
        /// </summary>
        public string LaunchPresentationLocale { get; set; }

        /// <summary>
        /// This is a URL to an LMS-specific CSS URL. There are no standards that describe exactly what CSS classes, etc. should be in this CSS. The TC could send its standard CSS URL that it would apply to its local tools. The TC should include styling for HTML tags to set font, color, etc. and also include its proprietary tags used to style its internal tools.
        /// </summary>
        public string LaunchPresentationCssUrl { get; set; }

        /// <summary>
        /// The value should be either ‘frame’, ‘iframe’ or ‘window’. This field communicates the kind of browser window/frame where the TC has launched the tool. The TP can ignore this parameter and detect its environment through JavaScript, but this parameter gives the TP the information without requiring the use of JavaScript if the tool prefers. This parameter is recommended.
        /// </summary>
        public PresentationTargets? LaunchPresentationDocumentTarget { get; set; }

        /// <summary>
        /// The height of the window or frame where the content from the tool will be displayed. The tool can ignore this parameter and detect its environment through JavaScript, but this parameter gives the TP the information without requiring the use of JavaScript if the tool prefers. This parameter is recommended.
        /// </summary>
        public int? LaunchPresentationHeight { get; set; }

        /// <summary>
        /// Fully qualified URL where the TP can redirect the user back to the TC interface. This URL can be used once the TP is finished or if the TP cannot start or has some technical difficulty. In the case of an error, the TP may add a parameter called lti_errormsg that includes some detail as to the nature of the error. The lti_errormsg value should make sense if displayed to the user. If the tool has displayed a message to the end user and only wants to give the TC a message to log, use the parameter lti_errorlog instead of lti_errormsg. If the tool is terminating normally, and wants a message displayed to the user it can include a text message as the lti_msg parameter to the return URL. If the tool is terminating normally and wants to give the TC a message to log, use the parameter lti_log. This data should be sent on the URL as a GET – so the TP should take care to keep the overall length of the parameters small enough to fit within the limitations of a GET request. This parameter is recommended.
        /// </summary>
        public string LaunchPresentationReturnUrl { get; set; }

        /// <summary>
        /// The width of the window or frame where the content from the tool will be displayed. The tool can ignore this parameter and detect its environment through JavaScript, but this parameter gives the TP the information without requiring the use of JavaScript if the tool prefers. This parameter is recommended.
        /// </summary>
        public int? LaunchPresentationWidth { get; set; }

        /// <summary>
        /// This field should be no more than 1023 characters long. This value should not change from one launch to the next and in general, the TP can expect that there is a one-to-one mapping between the lis_outcome_service_url and a particular oauth_consumer_key. This value might change if there was a significant re-configuration of the TC system or if the TC moved from one domain to another. The TP can assume that this URL generally does not change from one launch to the next but should be able to deal with cases where this value rarely changes. The service URL may support various operations / actions. The Basic Outcomes Service Provider will respond with a response of 'unimplemented' for actions it does not support. This field is required if the TC is accepting outcomes for any launches associated with the resource_link_id.
        /// </summary>
        public string LisOutcomeServiceUrl { get; set; }

        /// <summary>
        /// These fields contain information about the user account that is performing this launch. The names of these data items are taken from LIS [LIS, 11]. The precise meaning of the content in these fields is defined by LIS. These parameters are recommended unless they are suppressed because of privacy settings.
        /// </summary>
        public string LisPersonNameGiven { get; set; }

        public string LisPersonNameFamily { get; set; }

        public string LisPersonNameFull { get; set; }

        public string LisPersonContactEmailPrimary { get; set; }

        /// <summary>
        /// This field contains an identifier that indicates the LIS Result Identifier (if any) associated with this launch. This field identifies a unique row and column within the TC gradebook. This field is unique for every combination of context_id / resource_link_id / user_id. This value may change for a particular resource_link_id / user_id from one launch to the next. The TP should only retain the most recent value for this field for a particular resource_link_id / user_id. This field is optional.
        /// </summary>
        public string LisResultSourcedId { get; set; }

        /// <summary>
        /// This indicates that this is a basic launch message. This allows a TP to accept a number of different LTI message types at the same launch URL. This parameter is required.
        /// </summary>
        public string LtiMessageType { get; set; }

        /// <summary>
        /// This indicates which version of the specification is being used for this particular message. Since launches for version 1.1 are upwards compatible with 1.0 launches, this value is not advanced for LTI 1.1. This parameter is required.
        /// </summary>
        public string LtiVersion { get; set; }

        /// <summary>
        /// A plain text description of the link’s destination, suitable for display alongside the link. Typically no more than a few lines long. This parameter is optional.
        /// </summary>
        public string ResourceLinkDescription { get; set; }

        /// <summary>
        /// This is an opaque unique identifier that the TC guarantees will be unique within the TC for every placement of the link. If the tool / activity is placed multiple times in the same context, each of those placements will be distinct. This value will also change if the item is exported from one system or context and imported into another system or context. This parameter is required.
        /// </summary>
        public string ResourceLinkId { get; set; }

        /// <summary>
        /// A list of URN values for roles. If this list is non-empty, it should contain at least one role from the LIS System Role, LIS Institution Role, or LIS Context Role vocabularies (see Appendix A). The assumed namespace of these URNs is the LIS vocabulary of LIS Context Roles so TCs can use the handles when the intent is to refer to an LIS context role. If the TC wants to include a role from another namespace, a fully-qualified URN should be used. Usage of roles from non-LIS vocabularies is discouraged as it may limit interoperability. This parameter is recommended.
        /// </summary>
        public IList<LtiRoles> Roles { get; set; }

        public string RolesAsString
        {
            get { return string.Join(",", Roles.ToList()); }
            set
            {
                Roles = new List<LtiRoles>();
                if (string.IsNullOrWhiteSpace(value)) return;
                foreach (var roleName in value.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    LtiRoles role;
                    if (Enum.TryParse(roleName, true, out role))
                    {
                        Roles.Add(role);
                    }
                    else
                    {
                        if (Urns.ContainsKey(roleName))
                        {
                            Roles.Add(Urns[roleName]);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// A comma separated list of the user_id values which the current user can access as a mentor. The typical use case for this parameter is where the Mentor role represents a parent, guardian or auditor. It may be used in different ways by each TP, but the general expectation is that the mentor will be provided with access to tracking and summary information, but not necessarily the user’s personal content or assignment submissions. In order to accommodate user_id values which contain a comma, each user_id should be url-encoded. This also means that each user_id from the comma separated list should url-decoded before a TP uses it. This parameter is optional and should only be used when one of the roles passed for the current user is for urn:lti:role:ims/lis/Mentor.
        /// </summary>
        public string RoleScopeMentor { get; set; }

        /// <summary>
        /// A plain text title for the resource. This is the clickable text that appears in the link. This parameter is recommended.
        /// </summary>
        public string ResourceLinkTitle { get; set; }

        /// <summary>
        /// In order to better assist tools in using extensions and also making their user interface fit into the TC's user interface that they are being called from, each TC is encouraged to include the this parameter. This parameter is recommended.
        /// </summary>
        public string ToolConsumerInfoProductFamilyCode { get; set; }

        /// <summary>
        /// This field should have a major release number followed by a period. The format of the minor release is flexible. The TP should be flexible when parsing this field. This parameter is recommended.
        /// </summary>
        public string ToolConsumerInfoVersion { get; set; }

        /// <summary>
        /// An email contact for the TC instance. This parameter is recommended.
        /// </summary>
        public string ToolConsumerInstanceContactEmail { get; set; }

        /// <summary>
        /// This is a plain text user visible field – it should be about the length of a line. This parameter is optional.
        /// </summary>
        public string ToolConsumerInstanceDescription { get; set; }

        /// <summary>
        /// This is a unique identifier for the TC. A common practice is to use the DNS of the organization or the DNS of the TC instance. If the organization has multiple TC instances, then the best practice is to prefix the domain name with a locally unique identifier for the TC instance. In the single-tenancy case, the tool consumer data can be often be derived from the oauth_consumer_key. In a multi-tenancy case this can be used to differentiate between the multiple tenants within a single installation of a Tool Consumer. This parameter is strongly recommended in systems capable of multi-tenancy.
        /// </summary>
        public string ToolConsumerInstanceGuid { get; set; }

        /// <summary>
        /// This is a plain text user visible field – it should be about the length of a column. This parameter is recommended.
        /// </summary>
        public string ToolConsumerInstanceName { get; set; }

        /// <summary>
        /// This is the URL of the consumer instance. This parameter is optional.
        /// </summary>
        public string ToolConsumerInstanceUrl { get; set; }

        public string Url { get; set; }

        /// <summary>
        /// Uniquely identifies the user. This should not contain any identifying information for the user. Best practice is that this field should be a TC-generated long-term “primary key” to the user record – not the “logical key". At a minimum, this value needs to be unique within a TC. This parameter is recommended.
        /// </summary>
        public string UserId { get; set; }

        /// <summary>
        /// This attribute specifies the URI for an image of the user who launches this request. This image is suitable for use as a "profile picture" or an avatar representing the user. It is expected to be a relatively small graphic image file using a widely supported image format (i.e., PNG, JPG, or GIF) with a square aspect ratio. This parameter is optional.
        /// </summary>
        public string UserImage { get; set; }
    }
}