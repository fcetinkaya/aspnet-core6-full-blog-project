using BusinessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using Microsoft.AspNetCore.Mvc;

namespace BlogDemoNetCore.ViewComponents.Blog
{
    public class LastBlogs : ViewComponent
    {
        private BlogManager bm = new BlogManager(new EfBlogRepository());

        public IViewComponentResult Invoke()
        {
            var values = bm.GetListLastBlogs();
            return View(values);
        }
    }
}