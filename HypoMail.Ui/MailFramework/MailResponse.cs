namespace MailsManager.Ui.MailFramework
{
    using System.Net;
    using System.Net.Http;

    public class MailResponse
    {
        public HttpResponseMessage HttpResponse { get; private set; }

        public string Message { get; set; }

        public MailResponse(HttpResponseMessage httpResponse)
        {
            this.HttpResponse = httpResponse;
        }

        public MailResponse()
        {
         
        }

        public bool IsOk()
        {
            if (this.HttpResponse == null)
            {
                return false;
            }

            return 
                this.HttpResponse.StatusCode == HttpStatusCode.OK || 
                this.HttpResponse.StatusCode == HttpStatusCode.Accepted;
        }

        public string GetErrorMessage()
        {
            if (this.HttpResponse == null)
            {
                return this.Message;
            }

            switch (this.HttpResponse.StatusCode)
            {
                case HttpStatusCode.BadRequest:
                    return "The e-mail that you are trying to send contains errors that prevents it from being sent.";

                case HttpStatusCode.Unauthorized:
                    return "The user is not authorized to send an e-mail. This is because the credentials are not valid or expired.";

                case HttpStatusCode.Forbidden:
                    return "The e-mail server has rejected the connection.";

                case HttpStatusCode.NotFound:
                    return "The e-mail service could not be found.";

                case HttpStatusCode.MethodNotAllowed:
                    return "The e-mail service has rejected the operation due to lack of permissions.";

                case HttpStatusCode.RequestEntityTooLarge:
                    return "The e-mail that you are trying to send is too big and it was rejected by the e-mail server.";

                case HttpStatusCode.InternalServerError:
                    return "An error occurred in the e-mail server while your e-mail was being processed.";

                case HttpStatusCode.ServiceUnavailable:
                    return "The e-mail server is not currently available. Please, try again in a few moments.";

                default:
                    return "Uknown error while trying to send the e-mail.";
            }
        }
    }
}