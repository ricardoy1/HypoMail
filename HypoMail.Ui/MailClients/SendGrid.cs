using System;

namespace MailsManager.Ui.MailClients
{
    using System.Net.Http;
    using System.Net.Http.Headers;

    public class SendGrid : IMailClient
    {
        // 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
        // https://api.sendgrid.com/v3/mail/send
        public HttpResponseMessage Send(Mail mail)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://api.sendgrid.com");

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(
                    "Bearer",
                    "XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX");
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
