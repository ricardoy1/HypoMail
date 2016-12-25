namespace MailsManager.Ui.MailFramework
{

    public enum Status
    {
        Success,
        Error
    }

    public class SendMailResult
    {
        public string Message { get; set; }

        public Status Status { get; set; }
    }
}