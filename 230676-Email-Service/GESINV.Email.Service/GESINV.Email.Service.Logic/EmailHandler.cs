using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;
using GESINV.Email.Service.Domain;
using GESINV.Email.Service.Logic.Interfaces;
using GESINV.Email.Service.Domain.Utils;

namespace GESINV.Email.Service.Logic
{
    public class EmailHandler : IEmailHandler
    {
        public void SendEmail(string fromMail, string toMail, string subject, string body)
        {
            string smtpPassword = Environment.GetEnvironmentVariable(EnvarionmentVariablesNames.GESINV_SMTP_PASSWORD)
                ?? throw new Exception();
            string smtpMail = Environment.GetEnvironmentVariable(EnvarionmentVariablesNames.GESINV_SMTP_MAIL)
                ?? throw new Exception();
            string smtpHost = Environment.GetEnvironmentVariable(EnvarionmentVariablesNames.GESINV_SMTP_HOST)
                ?? throw new Exception();

            var email = new MimeMessage();

            email.From.Add(new MailboxAddress(StringUtils.ObtenerNombreDeEmail(fromMail), fromMail));
            email.To.Add(new MailboxAddress(StringUtils.ObtenerNombreDeEmail(fromMail), toMail));

            email.Subject = subject;
            email.Body = new TextPart(MimeKit.Text.TextFormat.Html)
            {
                Text = $"{body}"
            };

            using (var smtp = new SmtpClient())
            {
                try
                {
                    //465 - SslOnConnect
                    //587 - StartTls 
                    smtp.Connect(smtpHost, 587, MailKit.Security.SecureSocketOptions.StartTls);
                    // Note: only needed if the SMTP server requires authentication
                    smtp.Authenticate(smtpMail, smtpPassword);

                    smtp.Send(email);
                    smtp.Disconnect(true);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Exception caught in SendEmail(): {0}",
                    ex.ToString());
                }
            }
        }
    }
}
