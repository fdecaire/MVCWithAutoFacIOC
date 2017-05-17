using System.Web.Mvc;
using BusinessLogic;

namespace MVCwithAutoFacIOC.Controllers
{
    public class HomeController : Controller
    {
	    private readonly ISalesProducts _salesProducts;

	    public HomeController(ISalesProducts salesProducts)
	    {
		    _salesProducts = salesProducts;
	    }

        // GET: Home
        public ActionResult Index()
        {
			// read the first 10 names, from cache if available, otherwise from database
	        var nameList = _salesProducts.Top10ProductNames();

	        ViewBag.ProductNames = nameList;
            return View();
        }
    }
}