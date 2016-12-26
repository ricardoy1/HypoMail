namespace MailsManager.Ui.MailFramework
{
    using System;
    using System.Collections.Generic;
    using System.Collections.Specialized;
    using System.Configuration;
    using System.Linq;
    using System.Reflection;

    /// <summary>
    /// Factory of email clients.
    /// It will retrieve the list of email clients from the config file.
    /// </summary>
    public static class MailClientFactory
    {

        /// <summary>
        /// List of new Email clients.
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<IMailClient> GetMailClients()
        {
            var config = ConfigurationManager.GetSection("mailClients") as NameValueCollection;

            if (config == null)
            {
                throw new Exception("MailClients section was not found.");
            }

            return config.AllKeys
                .Select(key => Activator.CreateInstance(Assembly.GetExecutingAssembly().GetName().Name, config[key]).Unwrap())
                .OfType<IMailClient>()
                .ToList();
        }
    }
}