namespace MailsManager.Ui.MailFramework
{
    using System.Net.Http;

    using MailsManager.Ui.MailClients;

    public interface IMailClient
    {
        MailResponse Send(Mail mail);
    }
}