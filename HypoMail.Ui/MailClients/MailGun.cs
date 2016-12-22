namespace MailsManager.Ui.MailClients
{
    using System;
    using System.Collections.Generic;
    using System.Net.Http;
    using System.Net.Http.Headers;
    using System.Text;

    public class MailGun : IMailClient
    {
        private const string ApiKey = "key-XXXXXXXXXXXXXXXXXXXXXXXXXX";

        public HttpResponseMessage Send(Mail mail)
        {
            var apiKey = string.Format("api:{0}", ApiKey);

            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(
                    "Basic",
                    Convert.ToBase64String(Encoding.UTF8.GetBytes(apiKey)));

                var form = new Dictionary<string, string>();
                form["from"] = mail.From;
                form["to"] = string.Join(",", mail.To);
                form["cc"] = string.Join(",", mail.Cc);
                form["bcc"] = string.Join(",", mail.Bcc);
                form["subject"] = mail.Subject;
                form["text"] = mail.Message;

                var response =
                    client.PostAsync(
                        "https://api.mailgun.net/v3/hypodomain.com/messages",
                        new FormUrlEncodedContent(form)).Result;

                return response;
            }
        }
    }
}
