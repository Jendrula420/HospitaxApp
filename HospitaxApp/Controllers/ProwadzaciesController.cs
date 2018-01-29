using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using HospitaxApp.Models;

namespace HospitaxApp.Controllers
{
    public class ProwadzaciesController : Controller
    {
        private HospitaxDBEntities db = new HospitaxDBEntities();

  
        public ActionResult Index()
        {
            return View(db.Prowadzacies.ToList());
        }

    
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Prowadzacy prowadzacy = db.Prowadzacies.Find(id);
            if (prowadzacy == null)
            {
                return HttpNotFound();
            }
            return View(prowadzacy);
        }

     
        public ActionResult Create()
        {
            return View();
        }

     
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ProwadzacyID,Imie,Nazwisko,Tytul")] Prowadzacy prowadzacy)
        {
            if (ModelState.IsValid)
            {
                db.Prowadzacies.Add(prowadzacy);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(prowadzacy);
        }

     
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Prowadzacy prowadzacy = db.Prowadzacies.Find(id);
            if (prowadzacy == null)
            {
                return HttpNotFound();
            }
            return View(prowadzacy);
        }

   
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ProwadzacyID,Imie,Nazwisko,Tytul")] Prowadzacy prowadzacy)
        {
            if (ModelState.IsValid)
            {
                db.Entry(prowadzacy).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(prowadzacy);
        }

    
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Prowadzacy prowadzacy = db.Prowadzacies.Find(id);
            if (prowadzacy == null)
            {
                return HttpNotFound();
            }
            return View(prowadzacy);
        }

    
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Prowadzacy prowadzacy = db.Prowadzacies.Find(id);
            db.Prowadzacies.Remove(prowadzacy);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
