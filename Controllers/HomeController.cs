using System;
using System.Collections.Generic;
using EMARKET_MVC.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using EMARKET_MVC.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Session;
using System.Xml.Linq;
using Microsoft.CodeAnalysis;
using NuGet.ContentModel;


namespace EMARKET_MVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }
        public IActionResult Index()
        {
            return View();
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

        [HttpPost]
        public IActionResult Login(string CustomerId, string password)
        {
            EMARKETContext context = new();

            if (CustomerId == null && password == null)
            {
                TempData["mesaj"] = "Lütfen kullanıcı bilgilerini girin";
                return RedirectToAction("Index", "Home");
            }
            else
            {

                List<Customer> customers = context.Customers.ToList();
                var userInDb = customers.FirstOrDefault
                    (x => x.CustomerId == CustomerId && x.Password == password);

                if (userInDb != null)
                {
                    List<Customer> customer = context.Customers.ToList();
                    var User = customer.First(a => a.CustomerId == CustomerId);
                    string user = User.CustomerId;
                    HttpContext.Session.SetString("session", user);
                    return RedirectToAction("MainPage", "Home");
                }
                else
                {
                    TempData["mesaj"] = "Kullanıcı bilgileri hatalı";
                    return RedirectToAction("Index", "Home");
                }
            }

        }
        public IActionResult Logout()
        {
            return RedirectToAction("Index", "Home");
        }
        public IActionResult MainPage()
        {
            EMARKETContext context = new();
            var UserID = HttpContext.Session.GetString("session");
            List<Customer> customers = context.Customers.ToList();
            var User = customers.First(a => a.CustomerId == UserID);
            //string user = User.CustomerId;
            var name = User.Name;
            var surname = User.Surname;
            //ViewData["user"] = user;
            ViewData["name"] = name;
            ViewData["surname"] = surname;
            TempData["UserID"] = UserID;
            return View();
        }
        //[HttpPost]
        public IActionResult ProductList()
        {
            EMARKETContext context = new();
            List<Product> products = context.Products.ToList();
            return View(products);
        }

        public async Task<IActionResult> Details(string? ProductId)
        {
            EMARKETContext context = new();
            var product = await context.Products.FirstOrDefaultAsync(p => p.ProductId == ProductId);
            ViewData["product"] = product;
            TempData["ProductId"] = ProductId;
            return View(product);
        }
        public async Task<IActionResult> Basket(string? ProductId)
        {

            EMARKETContext context = new();
            var basket = new Basket { };
            var UserID = HttpContext.Session.GetString("session");
            var basketID = string.Join("", "1234567890qwertyuiopasdfghjklzxcvbnm".OrderBy(p => Guid.NewGuid()).Take(10));
            basket.BasketId = basketID;
            basket.ProductId = ProductId;
            basket.CustomerId = UserID;
            await context.Baskets.AddAsync(basket);
            context.SaveChanges();
            ViewData["mesaj"] = "Sepete eklendi";
            TempData["UserID"] = UserID;
            return View();
        }
        public async Task<IActionResult> UserBasket (string? CustomerId)
        {
            EMARKETContext context = new();
            var basket =  context.Baskets.Where(b => b.CustomerId == CustomerId).ToList();
            return View(basket);
        }

        public async Task<IActionResult> Order(string? ProductId)
        {
            EMARKETContext context = new();
            var order = new Order { };
            var UserID = HttpContext.Session.GetString("session");
            var ORDER_ID = string.Join("", "1234567890qwertyuiopasdfghjklzxcvbnm".OrderBy(p => Guid.NewGuid()).Take(10));
            order.OrderId = ORDER_ID;
            order.ProductId = ProductId;
            order.CustomerId = UserID;
            context.Orders.Add(order);

            var basket = new Basket { };
            List<Basket> baskets = context.Baskets.ToList();
            var OldOrder = baskets.FirstOrDefault(b => b.ProductId == ProductId && b.CustomerId == UserID);

            context.Orders.Add(order);
            context.Baskets.Remove(OldOrder);
            context.SaveChanges();
            ViewData["onay"] = "Sipariş Onaylandı";

            return View();
        }
        public async Task<IActionResult> MyOrder(string? CustomerId)
        {
            EMARKETContext context = new();
            var order = context.Orders.Where(b => b.CustomerId == CustomerId).ToList();
            return View(order);
        }
        public ActionResult BasketReport()
        {
            EMARKETContext context = new();
            List<Basket> basket = context.Baskets.ToList();
            return View(basket);
        }
        public ActionResult OrderReport()
        {
            EMARKETContext context = new();
            List<Order> order = context.Orders.ToList();
            return View(order);
        }

    }

}