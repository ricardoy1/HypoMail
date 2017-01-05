namespace MailsManager.Ui.MailClients
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net.Http;
    using System.Net.Http.Headers;
    using System.Web;
    using System.Web.Configuration;

    using MailsManager.Ui.MailFramework;

    public class SendGrid : IMailClient
    {
        public MailResponse Send(Mail mail)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://api.sendgrid.com");
                var apiKey = WebConfigurationManager.AppSettings["sendGridKey"];

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(
                    "Bearer",
                    apiKey);
                client.DefaultRequestHeaders.TryAddWithoutValidation("Content-Type", "application/json");



                var toList = mail.GetRecipientsByType(Mail.TO);
                var ccList = mail.GetRecipientsByType(Mail.CC);
                var bccList = mail.GetRecipientsByType(Mail.BCC);

                var personalizations = 
                    new List<object>
                    {
                        new
                            {
                                to = toList.Select(a => new { email = a }).ToArray()
                            }
                    };

                if (ccList.Any())
                {
                    personalizations.Add(new { cc = ccList.Select(a => new { email = a }).ToArray() });
                }

                if (bccList.Any())
                {
                    personalizations.Add(new { bcc = bccList.Select(a => new { email = a }).ToArray() });
                }

                var dynamicMessage =
                    new
                        {
                            personalizations = personalizations,
                            from = new
                                {
                                    email = mail.From
                                },
                            subject = mail.Subject,
                            content = 
                                new[]
                                {
                                    new
                                        {
                                            type = "text/plain", 
                                            value = mail.Message
                                        }
                                }
                        };

                try
                {
                    var response = client.PostAsJsonAsync("https://api.sendgrid.com/v3/mail/send", dynamicMessage).Result;

                    return new MailResponse(response);

                }
                catch (AggregateException ex)
                {
                    var message = "There has been an error while trying to connect to the e-mail server.";
                    ex.Handle(x =>
                    {
                        if (x is HttpRequestException)
                        {
                            message = "The e-mail client is not connected to the network.";
                            return true;
                        }

                        return false;
                    });

                    return new MailResponse { Message = message };
                }
            }
        }
    }
}
