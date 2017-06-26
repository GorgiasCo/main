using Gorgias.Business.DataTransferObjects.Email;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Web;

namespace Gorgias.Infrastruture.Core.Email
{
    public static class EmailHelper
    {
        public static bool Send(EmailDTO email, Dictionary<string, string> values)
        {
            try
            {
                string Body = PrepareBody(values);

                string SMTPUser = ConfigurationManager.AppSettings["SMTPUser"], SMTPPassword = ConfigurationManager.AppSettings["SMTPPassword"];

                MailMessage mail = new MailMessage();
                mail.From = new MailAddress(SMTPUser, ConfigurationManager.AppSettings["EmailFrom"]);
                mail.To.Add(email.TO);
                mail.Subject = email.Subject;
                mail.Body = Body;
                mail.IsBodyHtml = true;
                mail.Priority = MailPriority.High;

                SmtpClient smtp = new SmtpClient();
                //if you are using your smtp server, then change your host like "smtp.yourdomain.com"
                smtp.Host = ConfigurationManager.AppSettings["SMTPHost"];
                //chnage your port for your host
                smtp.Port = 587; //or you can also use port# 587
                smtp.Credentials = new System.Net.NetworkCredential(SMTPUser, SMTPPassword);
                //if you are using secure authentication using SSL/TLS then "true" else "false"
                smtp.EnableSsl = true;

                smtp.Send(mail);
                return true;

            }
            catch (SmtpException ex)
            {
                return false;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public static string PrepareBody(Dictionary<string, string> values)
        {
            StreamReader objStreamReader = new StreamReader(HttpContext.Current.Server.MapPath("~/content/mailtemplate.html"));
            string bodyMsg = objStreamReader.ReadToEnd();

            foreach (KeyValuePair<string, string> obj in values)
            {
                bodyMsg = bodyMsg.Replace(obj.Key, obj.Value);
            }
            return bodyMsg;
        }

        public static string PrepareBody(Dictionary<string, string> values, string filename)
        {
            StreamReader objStreamReader = new StreamReader(HttpContext.Current.Server.MapPath("~/content/" + filename));
            string bodyMsg = objStreamReader.ReadToEnd();

            foreach (KeyValuePair<string, string> obj in values)
            {
                bodyMsg = bodyMsg.Replace(obj.Key, obj.Value);
            }
            return bodyMsg;
        }

    }
}