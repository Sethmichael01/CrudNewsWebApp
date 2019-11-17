using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using DAL;
using System.Globalization;

namespace WebApp.Controllers
{
    static class DateTimeExtensions
    {
        public static string DateTimes(this DateTime dateTime)
        {
            return CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(dateTime.Month);
        }
    }
    public class ProductController : Controller
    {
        
        DatabaseContext databaseContext = new DatabaseContext();
        public IActionResult Index()
        {
            DateTime dateTime = DateTime.Now;
            ViewBag.Month = DateTime.Now.DateTimes();
            List<Product> data = databaseContext.Products.ToList();
            return View(data);
        }
        public IActionResult Create()
        {
            ViewBag.CategoryList = databaseContext.Categories.ToList();
            return View();
        }
        [HttpPost]
        public IActionResult Create(Product product)
        {
            databaseContext.Products.Add(product);
            databaseContext.SaveChanges();
            return RedirectToAction("Index");
        }
        public IActionResult Edit(int Id)
        {
            ViewBag.CategoryList = databaseContext.Categories.ToList();
            Product product = databaseContext.Products.Find(Id);
            return View("Create",product);
        }
        [HttpPost]
        public IActionResult Edit(Product product)
        {
            databaseContext.Products.Update(product);
            databaseContext.SaveChanges();
            return RedirectToAction("Index");
        }
        public IActionResult Delete(int Id)
        {
            Product product = databaseContext.Products.Find(Id);
            if (product != null)
            {
                databaseContext.Products.Remove(product);
                databaseContext.SaveChanges();
            }
            return RedirectToAction("Index");
        }
    }
}