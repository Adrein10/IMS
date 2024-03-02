﻿using IMS.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace IMS.Controllers
{
    public class HomeController : Controller
    {
        private readonly IMSContext context;
        private readonly IWebHostEnvironment web;


        //private readonly ILogger<HomeController> _logger;

        //public HomeController(ILogger<HomeController> logger)
        //{
        //    _logger = logger;
        //}
        public HomeController(IMSContext context , IWebHostEnvironment web)
        {
            this.context = context;
            this.web = web;
        }
        public IActionResult Index() 
        {
            var show = context.Products.ToList();
            return View(show);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Images img)
        {
            if(ModelState.IsValid)
            {
                string folder = Path.Combine(web.WebRootPath, "images");
                string filename = img.Image.FileName;
                string file = Path.Combine(folder, filename);
                img.Image.CopyTo(new FileStream(file,FileMode.Create));

                Product product = new Product()
                {
                    Name = img.Name,
                    Quantity = img.Quantity,
                    Image = filename
                };
                context.Products.Add(product);
                context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View();
        }
        public IActionResult Delete(int id) 
        {
            var show = context.Products.Find(id);
            return View(show);
        }
        [HttpPost]
        public IActionResult Delete(int id,Product product)
        {
            context.Products.Remove(product);
            context.SaveChanges();
            return RedirectToAction("Index");
        }
        public IActionResult Edit(int id)
        {
            var show = context.Products.Find(id);
            return View(show);
        }
        [HttpPost]
        public IActionResult Edit(int id ,Images img)
        {
            var show = context.Products.Find(id);
            if(show != null)
            {
                show.Name = img.Name;
                show.Quantity = img.Quantity;
                if (img.Image != null)
                {
                    string folder = Path.Combine(web.WebRootPath, "images");
                    string filename = img.Image.FileName;
                    string file = Path.Combine(folder, filename);
                    img.Image.CopyTo(new FileStream(file, FileMode.Create));

                    show.Image = filename;
                }
                context.Products.Update(show);
                context.SaveChanges();
                return RedirectToAction("Index");

            }
            return View();
        }
        public IActionResult Details(int id)
        {
            var show = context.Products.Find(id);
            return View(show);
        }
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}