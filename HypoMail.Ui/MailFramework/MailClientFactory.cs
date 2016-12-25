namespace MailsManager.Ui.MailFramework
{
    using System;
    using System.Collections.Generic;
    using System.Collections.Specialized;
    using System.Configuration;
    using System.Linq;

    public static class MailClientFactory
    {
        public static IEnumerable<IMailClient> GetMailClients()
        {
            var config = ConfigurationManager.GetSection("mailClients") as NameValueCollection;

            if (config == null)
            {
                throw new Exception("MailClients section was not found.");
            }

            return config.AllKeys
                .Select(key => Activator.CreateInstance(null, config[key]).Unwrap())
                .OfType<IMailClient>()
                .ToList();
        }
    }
}