namespace MailsManager.Ui.MailClients
{
    using System;
    using System.Net.Http;
    using System.Net.Http.Headers;
    using System.Web.Configuration;

    using MailsManager.Ui.MailFramework;

    public class SendGrid : IMailClient
    {
        public HttpResponseMessage Send(Mail mail)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://api.sendgrid.com");
                var apiKey = WebConfigurationManager.AppSettings["sendGridKey"];

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(
                    "Bearer",
                    apiKey);
                client.DefaultRequestHeaders.TryAddWithoutValidation("Content-Type", "application/json");

                var dynamicMessage =
                    new
                        {
                            personalizations = new[] { new { to = new[] { new { email = mail.To } } } },
                            from = new { email = mail.From },
                            subject = mail.Subject,
                            content = new[] { new { type = "text/plain", value = mail.Message } }
                        };

                var result = client.PostAsJsonAsync("https://api.sendgrid.com/v3/mail/send", dynamicMessage).Result;

                return result;
            }
        }
    }
}
