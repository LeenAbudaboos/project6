using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Fruitables.Models;

namespace Fruitables.Controllers
{
    public class registrationsController : Controller
    {
        private fruitablesEntities5 db = new fruitablesEntities5();

        // GET: registrations
        public ActionResult Index()
        {
            return View(db.registrations.ToList());
        }

        // GET: registrations/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            registration registration = db.registrations.Find(id);
            if (registration == null)
            {
                return HttpNotFound();
            }
            return View(registration);
        }

        // GET: registrations/Create
        public ActionResult registrations()
        {
            return View();
        }

        // POST: registrations/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult registrations([Bind(Include = "id,img,name,email,password,Confirm_password,status")] registration registration)
        {
            if (ModelState.IsValid)
            {
                db.registrations.Add(registration);
                db.SaveChanges();
                return RedirectToAction("Index");
            }


            return View(registration);
        }



        public ActionResult Profile(int id)
        {
            var user = db.registrations.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Profile(registration model)
        {
            if (ModelState.IsValid)
            {
                db.Entry(model).State = EntityState.Modified;
                db.SaveChanges();
                ViewBag.Message = "Profile updated successfully!";
            }
            return View(model);
        }





        //login
        public ActionResult login()
        {
            return View();
        }


    }










}

    


   

