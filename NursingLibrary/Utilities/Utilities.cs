using System;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Security.Cryptography;
using System.Text;
using System.DirectoryServices;
using System.Web;
using NursingLibrary.Common;

namespace NursingLibrary.Utilities
{
    public static class Utilities
    {
        public static string HttpPost(string url, byte[] postData)
        {
            string response;
            var webRequest = WebRequest.Create(url);
            webRequest.Method = "POST";
            webRequest.ContentType = "application/x-www-form-urlencoded";
            webRequest.ContentLength = postData.Length;

            using (var postStreamData = webRequest.GetRequestStream())
            {
                postStreamData.Write(postData, 0, postData.Length);
            }

            using (var webResponse = (HttpWebResponse)webRequest.GetResponse())
            {
                using (var reader = new StreamReader(webResponse.GetResponseStream()))
                {
                    response = reader.ReadToEnd();
                }
            }

            return response;
        }

        public static string BytesToHex(byte[] bytes)
        {
            var hexString = new StringBuilder(bytes.Length);
            for (int i = 0; i < bytes.Length; i++)
            {
                hexString.Append(bytes[i].ToString("X2"));
            }

            return hexString.ToString();
        }

        public static string CreateShaHash(string key, string ts, string enrollmentId)
        {
            using (var shaHash = new SHA512Managed())
            {
                var hashAsByte = Encoding.UTF8.GetBytes(string.Concat(key, ts, enrollmentId));
                var encryptedBuffer = shaHash.ComputeHash(hashAsByte);
                return BytesToHex(encryptedBuffer);
            }
        }

        public static string GetIsoDate(DateTime dateTime)
        {
            return dateTime.ToUniversalTime().ToString("yyyy-MM-dd HH:mm:ss");
        }

        public static bool IsQueryParameterExist(string key)
        {
            bool keyExist = false;
            string[] keys = HttpContext.Current.Request.QueryString.AllKeys;
            if (keys.Length > 0)
            {
                foreach (string k in keys)
                {
                    if (k.ToLower() == key.ToLower())
                    {
                        keyExist = true;
                        break;
                    }
                }
            }

            return keyExist;
        }

        public static decimal GetSize(long sizeInBytes)
        {
            return Math.Round((decimal)sizeInBytes / (decimal)Math.Pow(1024, 2), 2);
        }

        public static string EscapeIds(params string[] ids)
        {
            return string.Join("_", ids);
        }

        public static string GetDocumentContentType(string fileExtension)
        {
            fileExtension = fileExtension.ToLower();
            switch (fileExtension)
            {
                case ".htm":
                case ".html":
                case ".log":
                    return "text/HTML";
                case ".txt":
                    return "text/plain";
                case ".doc":
                    return "application/msword";
                case ".tiff":
                case ".tif":
                    return "image/tiff";
                case ".asf":
                    return "video/x-ms-asf";
                case ".avi":
                    return "video/avi";
                case ".zip":
                    return "application/zip";
                case ".rar":
                    return "application/x-rar-compressed";
                case ".xls":
                case ".csv":
                    return "application/vnd.ms-excel";
                case ".gif":
                    return "image/gif";
                case ".jpg":
                case "jpeg":
                    return "image/jpeg";
                case ".bmp":
                    return "image/bmp";
                case ".wav":
                    return "audio/wav";
                case ".mp3":
                    return "audio/mpeg3";
                case ".mpg":
                case "mpeg":
                    return "video/mpeg";
                case ".rtf":
                    return "application/rtf";
                case ".asp":
                    return "text/asp";
                case ".pdf":
                    return "application/pdf";
                case ".fdf":
                    return "application/vnd.fdf";
                case ".ppt":
                    return "application/mspowerpoint";
                case ".dwg":
                    return "image/vnd.dwg";
                case ".msg":
                    return "application/msoutlook";
                case ".xml":
                case ".sdxl":
                    return "application/xml";
                case ".xdp":
                    return "application/vnd.adobe.xdp+xml";
                case ".xlsx":
                    return "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                case ".docx":
                    return "application/vnd.openxmlformats- officedocument.wordprocessingml.document";
                case ".pptx":
                    return "application/vnd.openxmlformats-officedocument.presentationml.presentation";
                default:
                    return "application/octet-stream";
            }
        }

        public static ImpersonateUserBase GetUserImpersonation()
        {
            if (KTPApp.HelpfulDocImpersonationRequired)
            {
                return new ImpersonateUser(KTPApp.ImpersonateUserName, KTPApp.ImpersonateUserDomain, KTPApp.ImpersonateUserPassword);
            }
            return new ImpersonateUserBase();
        }

        public static bool IsValidEmailAddress(string emailId)
        {
            bool isValidMail = true;
            try
            {
                MailAddress validateMail = new MailAddress(emailId);
            }
            catch (Exception ex)
            {
                string message = ex.Message;
                isValidMail = false;
            }

            return isValidMail;
        }

        public static bool IsValidDomainUser(string kecuserName, string kecpassword)
        {
            var isdomainuser = false;
            try
            {
                var entry = new DirectoryEntry(KTPApp.LdapAuthPath, kecuserName, kecpassword);

                //Bind to the native AdsObject to force authentication.
                object obj = entry.NativeObject;

                isdomainuser = true;
            }
            catch (Exception)
            { }
           
            return isdomainuser;
        }
    }
}
