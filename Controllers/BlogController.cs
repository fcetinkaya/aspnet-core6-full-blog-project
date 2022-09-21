using BusinessLayer.Concrete;
using BusinessLayer.ValidationRulles;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;
using FluentValidation.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BlogDemoNetCore.Controllers
{
    [AllowAnonymous]
    public class BlogController : Controller
    {
        private BlogManager bm = new BlogManager(new EfBlogRepository());
  CategoryManager cm = new CategoryManager(new EfCategoryRepository());

        public IActionResult Index()
        {
            var values = bm.GetListWithCategory();
            return View(values);
        }

        public IActionResult BlogReadAll(int id)
        {
            ViewBag.bid = id;
            var values = bm.GetBlogByID(id);
            return View(values);
        }
        public IActionResult BlogListByWriter(int id)
        {
            id = 1;
            var values = bm.GetListWithCategoryByWriter(id);
            return View(values);
        }
        [HttpGet]

        public IActionResult BlogAdd()
        {
          
            List<SelectListItem> categoryvalues = (from x in cm.GetList()
                                                   select new SelectListItem
                                                   {
                                                       Text = x.CategoryName,
                                                       Value = x.CategoryID.ToString()
                                                   }).ToList();
            ViewBag.cv = categoryvalues;
            return View();
        }

        [HttpPost]
        public IActionResult BlogAdd(Blog p)
        {
            BlogValidator bv = new BlogValidator();
            ValidationResult result = bv.Validate(p);
            if (result.IsValid)
            {
                p.BlogStatus = true;
                p.BlogCreateDate = DateTime.Parse(DateTime.Now.ToShortDateString());
                p.WriterID = 1;
                bm.TAdd(p);
                return RedirectToAction("BlogListByWriter", "Blog");
            }
            else
            {
                foreach (var item in result.Errors)
                {
                    ModelState.AddModelError(item.PropertyName, item.ErrorMessage);
                }
            }
            return View();


            bm.TAdd(p);
            return View();
        }

        public IActionResult DeleteBlog(int id)
        {
            var blogValue = bm.GetById(id);
            bm.TDelete(blogValue);
            return RedirectToAction("BlogListByWriter");
        }
             
        [HttpGet]
        public IActionResult EditBlog(int id)
        {
            List<SelectListItem> categoryvalues = (from x in cm.GetList()
                                                   select new SelectListItem
                                                   {
                                                       Text = x.CategoryName,
                                                       Value = x.CategoryID.ToString()
                                                   }).ToList();
            ViewBag.cv = categoryvalues;

            var blogvalue = bm.GetById(id);
            return View(blogvalue);
        }

        [HttpPost]
        public IActionResult EditBlog(Blog b)
        {
            b.WriterID = 1;
            bm.TUpdate(b);
            return View();
        }
    }
}