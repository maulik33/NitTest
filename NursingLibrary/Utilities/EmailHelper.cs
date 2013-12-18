using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Mail;
using System.Text;
using NursingLibrary.DTC;

namespace NursingLibrary.Utilities
{
    public class EmailHelper
    {
        public static void Send(string toAdress, string fromAddress, string subject, string emailBody)
        {
            MailMessage mailMessage = new MailMessage();
            mailMessage.To.Add(new MailAddress(toAdress));
            mailMessage.From = new MailAddress(fromAddress);
            mailMessage.Subject = subject;

            SmtpClient smtpClient = new SmtpClient();
            object userState = mailMessage;
            ////The following method will fail as SMTP host was not specifed
            smtpClient.Host = ConfigurationManager.AppSettings["EmailServer"];
            smtpClient.Port = ConfigurationManager.AppSettings["EmailPort"].ToInt();
            mailMessage.Body = emailBody;
            mailMessage.Priority = System.Net.Mail.MailPriority.High;
            mailMessage.IsBodyHtml = true;

            ////System.Net.NetworkCredential NetworkCred = new System.Net.NetworkCredential();
            ////NetworkCred.UserName = "shodhan.kini@marlabs.com";
            ////NetworkCred.Password = "";
            ////smtpClient.UseDefaultCredentials = true;
            ////smtpClient.Credentials = NetworkCred;

            smtpClient.Send(mailMessage);
        }
    }
}
