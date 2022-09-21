using BusinessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using Microsoft.AspNetCore.Mvc;

namespace BlogDemoNetCore.ViewComponents.Blog
{
    public class WriterLastBlog : ViewComponent
    {
        private BlogManager bm = new BlogManager(new EfBlogRepository());

        public IViewComponentResult Invoke(int id)
        {
            var values = bm.GetBlogListByWriter(id);
            return View(values);
        }
    }
}