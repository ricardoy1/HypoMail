namespace MailsManager.Ui.MailFramework
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;
    using System.Net.Http;

    using MailsManager.Ui.MailClients;

    public class MailSender
    {

        private static object _lock = new object();

        private static MailSender _instance;

        private static readonly IEnumerable<IMailClient> _mailClients;

        static MailSender()
        {
            _mailClients = MailClientFactory.GetMailClients();

            if (!_mailClients.Any())
            {
                throw new Exception("There are no configured e-mail client.");
            }
        }

        public static MailSender GetInstance()
        {
            lock (_lock)
            {
                if (_instance == null)
                {
                    _instance = new MailSender();
                }

                return _instance;
            }
        }

        public SendMailResult SendMail(Mail mail)
        {
            HttpResponseMessage response = null;

            foreach (var mailClient in _mailClients)
            {
                response = mailClient.Send(mail);

                if (response.StatusCode == HttpStatusCode.OK)
                {
                    return new SendMailResult { Message = this.GetMessage(response), Status = Status.Success };
                }
            }

            return new SendMailResult
                       {
                           Status = Status.Error,
                           Message = this.GetMessage(response)
                       }; 
        }

        private string GetMessage(HttpResponseMessage response)
        {
            if (response == null)
            {
                return "Uknown error while trying to send the e-mail.";
            }

            switch (response.StatusCode)
            {
                case HttpStatusCode.OK:
                    return "Mail successfully sent.";
                case HttpStatusCode.Unauthorized:
                    return "The user is not authorized to send an e-mail.";
                default:
                    return "Uknown error while trying to send the e-mail.";
            }
        }
    }
}