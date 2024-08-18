using Fruitables.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Fruitables.Controllers
{
    public class ProductsController : Controller
    {
        private fruitablesEntities5 db = new fruitablesEntities5();

        // عرض قائمة المنتجات
        public ActionResult Index()
        {
            var products = db.products.ToList();
            return View(products);
        }

        // عرض نموذج إضافة منتج جديد
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(product model, HttpPostedFileBase upload)
        {
            if (ModelState.IsValid)
            {
                if (upload != null && upload.ContentLength > 0)
                {
                    string path = Path.Combine(Server.MapPath("~/Images"), Path.GetFileName(upload.FileName));
                    upload.SaveAs(path);
                    model.img = "/Images/" + Path.GetFileName(upload.FileName);
                }

                db.products.Add(model);
                db.SaveChanges();

                return RedirectToAction("Home");
            }

            return View(model);
        }

        public ActionResult About()
        {
            return View();
        }
      
        public ActionResult Details(int id)
        {
            var product = db.products.Find(id);

            return View("DetailsItme");
        }

        public ActionResult DetailsItme(int id)
        {
            var product = db.products.Find(id);

            return View(product);
        }

        public ActionResult Home()
        {

            return View();

        }

        public ActionResult AddToCart()
        {

            var cart = db.CartItems.Include(x=>x.product).ToList();

            return View(cart);

        }

        // إضافة منتج إلى السلة
        [HttpPost]
        public ActionResult Add(int id)
        {
            var product = db.products.Find(id);
            if (product != null)
            {
                var cartItem = db.CartItems.SingleOrDefault(c => c.ProductId == id);
                if (cartItem != null)
                {
                    cartItem.Quantity++;
                    db.Entry(cartItem).State = EntityState.Modified;

                }
                else
                {
                    CartItem cartItem1 = new CartItem();
                    cartItem1.Quantity = 1;
                    cartItem1.registrationid =1; //لازم اغيرو بناء على اليوزر الي داخل 
                    cartItem1.ProductId = id;
                    cartItem = cartItem1;
                    db.CartItems.Add(cartItem);
                   
                }
                db.SaveChanges();
            }

            return RedirectToAction("AddToCart");
        }





        [HttpPost]
        public ActionResult RemoveFromCart(int id)
        {
            var cartItem = db.CartItems.Find(id);
            if (cartItem != null)
            {
                db.CartItems.Remove(cartItem);
                db.SaveChanges();
            }
            return RedirectToAction("Index");
        }
        //public ActionResult ViewCart()
        //{
        //    var cartItems = db.CartItems
        //        .Include(c => c.id  )  
        //        .ToList();

        //    return View(cartItems);
        //}

        // عرض سلة التسوق
        //public ActionResult showitem()
        //{
        //    var cartItems = db.CartItems.Include("Product").ToList();
        //    return View(cartItems);
        //}

    }
}
