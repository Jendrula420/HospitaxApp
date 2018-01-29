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
    public class ProtokoliesController : Controller
    {
        private HospitaxDBEntities db = new HospitaxDBEntities();

    
        public ActionResult Index()
        {
            var protokolies = db.Protokolies.Include(p => p.Prowadzacy);
            return View(protokolies.ToList());
        }

   
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Protokoly protokoly = db.Protokolies.Find(id);
            if (protokoly == null)
            {
                return HttpNotFound();
            }
            return View(protokoly);
        }

     
        public ActionResult Create()
        {
            ViewBag.ProwadzacyID = new SelectList(db.Prowadzacies, "ProwadzacyID", "Imie");
            return View();
        }

      
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ProtokolyID,Kurs,Data,ProwadzacyID")] Protokoly protokoly)
        {
            if (ModelState.IsValid)
            {
                db.Protokolies.Add(protokoly);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ProwadzacyID = new SelectList(db.Prowadzacies, "ProwadzacyID", "Imie", protokoly.ProwadzacyID);
            return View(protokoly);
        }

     
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Protokoly protokoly = db.Protokolies.Find(id);
            if (protokoly == null)
            {
                return HttpNotFound();
            }
            ViewBag.ProwadzacyID = new SelectList(db.Prowadzacies, "ProwadzacyID", "Nazwisko", protokoly.ProwadzacyID);
            return View(protokoly);
        }

       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ProtokolyID,Kurs,Data,ProwadzacyID")] Protokoly protokoly)
        {
            if (ModelState.IsValid)
            {
                db.Entry(protokoly).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ProwadzacyID = new SelectList(db.Prowadzacies, "ProwadzacyID", "Imie", protokoly.ProwadzacyID);
  
            return View(protokoly);
        }

 
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Protokoly protokoly = db.Protokolies.Find(id);
            if (protokoly == null)
            {
                return HttpNotFound();
            }
            return View(protokoly);
        }

    
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Protokoly protokoly = db.Protokolies.Find(id);
            db.Protokolies.Remove(protokoly);
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
