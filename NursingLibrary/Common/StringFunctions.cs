using System;
using System.Web;

namespace NursingLibrary.Common
{
    /// <summary>
    /// Summary description for StringFunctions
    /// </summary>

    public static class StringFunctions
    {
        public static string Left(string param, int length)
        {
            string result = param.Substring(0, length);
            return result;
        }

        public static string Right(string param, int length)
        {
            string result = param.Substring(param.Length - length, length);
            return result;
        }

        public static string Mid(string param, int startIndex, int length)
        {
            string result = param.Substring(startIndex, length);
            return result;
        }

        public static string Mid(string param, int startIndex)
        {
            string result = param.Substring(startIndex);
            return result;
        }

        public static int WordCount(string text)
        {
            string tmpStr = text.Replace("\t", " ").Trim();
            tmpStr = tmpStr.Replace("\n", " ");
            tmpStr = tmpStr.Replace("\r", " ");

            while (tmpStr.IndexOf("  ", StringComparison.Ordinal) != -1)
            {
                tmpStr = tmpStr.Replace("  ", " ");
            }

            return tmpStr.Split(' ').Length;
        }

        public static string QuestionActivitiesSpace(string text)
        {
            string tmpStr = text.Replace("<QuestionID=", "  <QuestionID=").Trim();
            tmpStr = tmpStr.Replace("<ActivitieID=", "  <ActivitieID=").Trim();
            tmpStr = tmpStr.Replace("/>", "/>  ").Trim();

            return tmpStr;
        }

        public static string AnswerSpace(string text)
        {
            string tmpStr = text.Replace("<Answer=", "  <Answer=").Trim();
            tmpStr = tmpStr.Replace("/>", "/>  ").Trim();

            return tmpStr;
        }

        /// <summary>
        /// Converts the provided app-relative path into an absolute Url containing the full host name
        /// </summary>
        /// <param name="relativeUrl">App-Relative path</param>
        /// <returns>Provided relativeUrl parameter as fully qualified Url</returns>
        /// <example>~/path/to/foo to http://www.web.com/path/to/foo</example>
        public static string ToAbsoluteUrl(this string relativeUrl)
        {
            if (string.IsNullOrEmpty(relativeUrl))
                return relativeUrl;

            if (HttpContext.Current == null)
                return relativeUrl;

            if (relativeUrl.StartsWith("/"))
                relativeUrl = relativeUrl.Insert(0, "~");
            if (!relativeUrl.StartsWith("~/"))
                relativeUrl = relativeUrl.Insert(0, "~/");

            var url = HttpContext.Current.Request.Url;
            var port = url.Port != 80 ? (":" + url.Port) : String.Empty;

            return String.Format("{0}://{1}{2}{3}",url.Scheme, url.Host, port, VirtualPathUtility.ToAbsolute(relativeUrl));
        }
    }
}