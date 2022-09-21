using BusinessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using Microsoft.AspNetCore.Mvc;

namespace BlogDemoNetCore.Controllers
{
    public class AboutController : Controller
    {
        private AboutManager mg = new AboutManager(new EfAboutRepository());

        public IActionResult Index()
        {
            var values = mg.GetList();
            return View(values);
        }

        public PartialViewResult SocialMediaAbout()
        {
            return PartialView();
        }
    }
}