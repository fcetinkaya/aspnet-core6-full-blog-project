using BusinessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using Microsoft.AspNetCore.Mvc;

namespace BlogDemoNetCore.Controllers
{
    public class CategoryController : Controller
    {
        private CategoryManager cm = new CategoryManager(new EfCategoryRepository());

        public IActionResult Index()
        {
            var List = cm.GetList();
            return View(List);
        }
    }
}