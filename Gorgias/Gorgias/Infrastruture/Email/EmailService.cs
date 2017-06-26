using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Gorgias.Infrastruture.Email
{
    public class EmailService : IIdentityMessageService
    {
        public async Task SendAsync(IdentityMessage message)
        {
            await configSMTPasync(message);
        }

        // send email via smtp service
        private async Task configSMTPasync(IdentityMessage message)
        {
            // Plug in your email service here to send an email.
            string SMTPUser = ConfigurationManager.AppSettings["SMTPUser"], SMTPPassword = ConfigurationManager.AppSettings["SMTPPassword"];
            SmtpClient smtp = new SmtpClient();
            //if you are using your smtp server, then change your host like "smtp.yourdomain.com"
            smtp.Host = ConfigurationManager.AppSettings["SMTPHost"];
            //chnage your port for your host
            smtp.Port = 587; //or you can also use port# 587 25
            smtp.Credentials = new System.Net.NetworkCredential(SMTPUser, SMTPPassword);
            //smtp.UseDefaultCredentials = true;
            //if you are using secure authentication using SSL/TLS then "true" else "false"
            smtp.EnableSsl = true;         
            //smtp.UseDefaultCredentials = false;SMTPUser
            
            MailMessage mail = new MailMessage();
            mail.From = new MailAddress(ConfigurationManager.AppSettings["EmailFrom"], ConfigurationManager.AppSettings["EmailFrom"]);
            mail.To.Add(message.Destination);
            mail.Subject = message.Subject;
            mail.Body = message.Body;
            mail.IsBodyHtml = true;
            mail.Priority = MailPriority.High;
            try {
                await smtp.SendMailAsync(mail);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            //var credentialUserName = "info@ourdoamin.com";
            //var sentFrom = "noreply@ourdoamin.com";
            //var pwd = "ourpassword";

            // Configure the client:
            //System.Net.Mail.SmtpClient client = new System.Net.Mail.SmtpClient("mail.ourdomain.com");

            //client.Port = 25;
            //client.DeliveryMethod = System.Net.Mail.SmtpDeliveryMethod.Network;
            //client.UseDefaultCredentials = false;

            //// Creatte the credentials:
            //System.Net.NetworkCredential credentials = new System.Net.NetworkCredential(credentialUserName, pwd);
            //client.EnableSsl = false;
            //client.Credentials = credentials;

            //// Create the message:
            //var mail = new System.Net.Mail.MailMessage(sentFrom, message.Destination);
            //mail.Subject = message.Subject;
            //mail.Body = message.Body;

           //await client.SendMailAsync(mail);
        }
    }
}