using System.Web.Mvc;

namespace mvc_todo.Controllers
{
    public class ErrorController : Controller
    {        
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult NotFound()
        {                        
            return View();
        }
    }
}