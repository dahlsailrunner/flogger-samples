using Flogging.Web.Filters;
using System.Web.Mvc;

namespace mvc_todo.Controllers
{
    public class HomeController : Controller
    {
        [TrackUsage(Constants.ProductName, Constants.LayerName, "View Home")]
        public ActionResult Index()
        {
            return View();
        }

        [TrackUsage(Constants.ProductName, Constants.LayerName, "View About")]
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        [TrackUsage(Constants.ProductName, Constants.LayerName, "View Contact")]
        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}