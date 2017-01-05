namespace MailsManager.Ui.MailFramework
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class Recipient
    {
        public string Type { get; set; }

        public string Address { get; set; }
    }

    public class Mail
    {
        public const string TO = "TO";

        public const string CC = "CC";

        public const string BCC = "BCC";

        public string To { get; private set; }

        public string Cc { get; private set; }

        public string Bcc { get; private set; }

        private IEnumerable<Recipient> _recipients;

        public IEnumerable<Recipient> Recipients {
            get
            {
                return this._recipients;
            }

            set
            {
                this._recipients = value;

                this.To = string.Join(",", this.GetRecipientsByType(TO));
                this.Cc = string.Join(",", this.GetRecipientsByType(CC));
                this.Bcc = string.Join(",", this.GetRecipientsByType(BCC));

            }
        }

        public IEnumerable<string> GetRecipientsByType(string type)
        {
            if (this.Recipients == null)
            {
                return Enumerable.Empty<string>();
            }

            return this.Recipients
                .Where(r => string.Compare(r.Type, type, StringComparison.OrdinalIgnoreCase) == 0)
                .Select(r => r.Address);
        }

        public string Subject { get; set; }

        public string Message { get; set; }

        public string From { get; set; }
    }
}