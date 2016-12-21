namespace MailsManager.Ui.Api
{
    using System.Web.Http;

    using MailsManager.Ui.MailClients;

    // [Authorize]
    // TODO: Uncomment the above line when security is implemented: JIRA-1234

    /// <summary>
    /// Mails Web API controller.
    /// </summary>
    public class MailsController : ApiController
    {
        public MailsController()
        {
        }

        public void Send(Mail mail)
        {
            // MailGun client = new MailGun();
            SendGrid client = new SendGrid();
            var result = client.Send(mail);
        }
    }
}
