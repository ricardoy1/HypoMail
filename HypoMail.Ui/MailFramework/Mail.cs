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

                this.To = string.Join(",", GetRecipientsByType(this._recipients, "TO"));
                this.Cc = string.Join(",", GetRecipientsByType(this._recipients, "CC"));
                this.Bcc = string.Join(",", GetRecipientsByType(this._recipients, "BCC"));

            }
        }

        private static IEnumerable<string> GetRecipientsByType(IEnumerable<Recipient> recipients, string type)
        {
            if (recipients == null)
            {
                return Enumerable.Empty<string>();
            }

            return recipients
                .Where(r => string.Compare(r.Type, type, StringComparison.OrdinalIgnoreCase) == 0)
                .Select(r => r.Address);
        }

        public string Subject { get; set; }

        public string Message { get; set; }

        public string From { get; set; }
    }
}