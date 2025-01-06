using System.Web.Mvc;

namespace DusColl.Controllers
{
    public class ErrorController : Controller
    {
        //
        // GET: /Error/

        public ActionResult Index(string aspxerrorpath)
        {
            if (Session["Account"] == null)
            {
                return RedirectToRoute("DefaultExpired");
            }
            else
            {
                if (!string.IsNullOrWhiteSpace(aspxerrorpath))
                    return RedirectToAction("RequestPush");
            }
            return View();
        }

        public ActionResult UnsupportBrowser()
        {
            return View("unSupportBrowser");
        }

        public ActionResult RequestPush()
        {
            return View("GateError");
        }
    }
}