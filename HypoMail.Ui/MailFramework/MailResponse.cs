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
                    return "The e-mail that you are trying to send is not valid and the e-mail server has rejected it. "
                           + "Please, make sure you have completed all mandatory fields and make sure those fields are valid: i.e: the e-mail address is a valid e-mail address.";

                case HttpStatusCode.Unauthorized:
                    return "The user is not authorized to send an e-mail. This is because the credentials are not valid or have expired. "
                           + "Please, contact technical support to resolve this problem.";

                case HttpStatusCode.Forbidden:
                    return "The e-mail server has rejected the connection. This could be happening due to a problem on the e-mail server "
                           + "or because the e-mail server connection has not been properly set. Please, contact technical support to resolve this problem.";

                case HttpStatusCode.NotFound:
                    return "The e-mail service could not be found. This could be happening due to a problem on the e-mail server "
                           + "or because the e-mail server connection has not been properly set. Please, contact technical support to resolve this problem.";

                case HttpStatusCode.MethodNotAllowed:
                    return "The e-mail service has rejected the operation due to lack of permissions. It is possible that the e-mail server connection has not been properly set. "
                           + "Please, contact technical support to resolve this problem.";

                case HttpStatusCode.RequestEntityTooLarge:
                    return "The e-mail that you are trying to send is too big and it was rejected by the e-mail server.";

                case HttpStatusCode.InternalServerError:
                    return "An error occurred in the e-mail server while your e-mail was being processed. If the problem persists, please contact technical support to resolve this problem.";

                case HttpStatusCode.ServiceUnavailable:
                    return "The e-mail server is not currently available. Please, try again in a few moments. If the problem persists, please contact technical support to resolve this problem.";

                case HttpStatusCode.RequestTimeout:
                    return
                        "The e-mail server has timed-out while trying to send your e-mail. Please, try again in a few moments. If the problem persists, please contact technical support to resolve this problem.";

                default:
                    return "Uknown error while trying to send the e-mail: There has been an error with code: Http-" + this.HttpResponse.StatusCode + " "
                           + "Please, contact technical support providing with those details.";
            }
        }
    }
}