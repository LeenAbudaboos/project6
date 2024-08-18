using Fruitables.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Fruitables.Controllers
{
    public class DefaultController : Controller
    {
        private fruitablesEntities5 db = new fruitablesEntities5();

        public ActionResult Index()
        {
            var products = db.products.ToList();
            return View(products);
        }

        // عرض تفاصيل المنتج
        public ActionResult Details(int id)
        {
            var product = db.products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // إضافة منتج إلى السلة
        [HttpPost]
        public ActionResult AddToCart(int id)
        {
            var product = db.products.Find(id);
            if (product != null)
            {
                var cartItem = db.CartItems.SingleOrDefault(c => c.ProductId == id);
                if (cartItem != null)
                {
                    cartItem.Quantity++;
                }
                else
                {
                    db.CartItems.Add(new CartItem
                    {
                        ProductId = id,
                        Quantity = 1
                    });
                }
                db.SaveChanges();
            }

            return RedirectToAction("Cart");
        }

        // عرض السلة
        public ActionResult Cart()
        {
            var cartItems = db.CartItems.Include("Product").ToList();
            return View(cartItems);
        }

        // عرض نموذج إضافة منتج جديد
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(product model)
        {
            if (ModelState.IsValid)
            {
                db.products.Add(model);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(model);
        }
    }
}
