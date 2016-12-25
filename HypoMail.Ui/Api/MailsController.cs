namespace MailsManager.Ui.Api
{
    using System.Net;
    using System.Net.Http;
    using System.Web.Http;

    using MailsManager.Ui.MailClients;
    using MailsManager.Ui.MailFramework;

    /// <summary>
    /// Mails Web API controller.
    /// </summary>
    public class MailsController : ApiController
    {
        public MailsController()
        {
        }

        [HttpPost]
        public HttpResponseMessage Send(Mail mail)
        {
            var result = MailSender.GetInstance().SendMail(mail);

            if (result.Status == Status.Error)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, new { Message = result.Message });
            }

            return Request.CreateResponse(HttpStatusCode.OK, new { Message = result.Message });
        }
    }
}
