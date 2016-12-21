namespace MailsManager.Ui.MailClients
{
    using System.Net.Http;
    using System.Threading.Tasks;

    public interface IMailClient
    {
        HttpResponseMessage Send(Mail mail);
    }
}