namespace MailsManager.Ui.Controllers
{
    using System.Web.Mvc;

    // [Authorize]
    // TODO: Uncomment the above line when security is implemented: JIRA-1234

    /// <summary>
    /// Main view controller.
    /// </summary>
    public class HomeController : Controller
    {
        /// <summary>
        /// Main view action.
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            return this.View();
        }
    }
}
