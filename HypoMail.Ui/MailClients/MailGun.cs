namespace MailsManager.Ui.MailClients
{
    using System;
    using System.Collections.Generic;
    using System.Net.Http;
    using System.Net.Http.Headers;
    using System.Text;
    using System.Web.Configuration;

    using MailsManager.Ui.MailFramework;

    public class MailGun : IMailClient
    {
        private const string Domain = "hypodomain.com";

        public HttpResponseMessage Send(Mail mail)
        {
            var apiKey = WebConfigurationManager.AppSettings["mailGunKey"];

            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(
                    "Basic",
                    Convert.ToBase64String(Encoding.UTF8.GetBytes(apiKey)));

                var form = new Dictionary<string, string>();
                form["from"] = mail.From;
                form["to"] = mail.To;
                form["cc"] = mail.Cc;
                form["bcc"] = mail.Bcc;
                form["subject"] = mail.Subject;
                form["text"] = mail.Message;

                var response =
                    client.PostAsync(
                        string.Format("https://api.mailgun.net/v3/{0}/messages", apiKey),
                        new FormUrlEncodedContent(form)).Result;

                return response;
            }
        }
    }
}
