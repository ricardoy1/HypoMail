namespace MailsManager.Ui.MailFramework
{
    using System.Net.Http;

    using MailsManager.Ui.MailClients;

    public interface IMailClient
    {
        HttpResponseMessage Send(Mail mail);
    }
}