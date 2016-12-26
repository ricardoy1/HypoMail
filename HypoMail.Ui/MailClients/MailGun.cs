namespace MailsManager.Ui.MailClients
{
    using System;
    using System.Collections.Generic;
    using System.Configuration;
    using System.Net.Http;
    using System.Net.Http.Headers;
    using System.Text;
    using System.Web.Configuration;

    using MailsManager.Ui.MailFramework;

    public class MailGun : IMailClient
    {
        

        public HttpResponseMessage Send(Mail mail)
        {
            var apiKey = WebConfigurationManager.AppSettings["mailGunKey"];
            var domain = WebConfigurationManager.AppSettings["mailGunDomain"];

            if (apiKey == null || domain == null)
            {
                throw new ConfigurationErrorsException("Main Gun configuration is incomplete.");
            }

            using (var client = new HttpClient())
            {

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(
                    "Basic",
                    Convert.ToBase64String(Encoding.UTF8.GetBytes(apiKey)));

                var form = new Dictionary<string, string>();
                form["from"] = mail.From;

                if (!string.IsNullOrEmpty(mail.To))
                {
                    form["to"] = mail.To;
                }

                if (!string.IsNullOrEmpty(mail.Cc))
                {
                    form["cc"] = mail.Cc;
                }

                if (!string.IsNullOrEmpty(mail.Bcc))
                {
                    form["bcc"] = mail.Bcc;
                }

                form["subject"] = mail.Subject;
                form["text"] = mail.Message;

                var response =
                    client.PostAsync(
                        string.Format("https://api.mailgun.net/v3/{0}/messages", domain),
                        new FormUrlEncodedContent(form)).Result;

                return response;
            }
        }
    }
}
