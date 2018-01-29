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
    public class HospitujaciesController : Controller
    {
        private HospitaxDBEntities db = new HospitaxDBEntities();

      
        public ActionResult Index()
        {
            var hospitujacies = db.Hospitujacies.Include(h => h.Protokoly).Include(h => h.Prowadzacy);
            return View(hospitujacies.ToList());
        }

      
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Hospitujacy hospitujacy = db.Hospitujacies.Find(id);
            if (hospitujacy == null)
            {
                return HttpNotFound();
            }
            return View(hospitujacy);
        }

    
        public ActionResult Create()
        {
            ViewBag.ProtokolyID = new SelectList(db.Protokolies, "ProtokolyID", "Kurs");
            ViewBag.ProwadzacyID = new SelectList(db.Prowadzacies, "ProwadzacyID", "Imie");
            return View();
        }

     
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "HospitujacyID,ProwadzacyID,ProtokolyID")] Hospitujacy hospitujacy)
        {
            if (ModelState.IsValid)
            {
                db.Hospitujacies.Add(hospitujacy);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ProtokolyID = new SelectList(db.Protokolies, "ProtokolyID", "Kurs", hospitujacy.ProtokolyID);
            ViewBag.ProwadzacyID = new SelectList(db.Prowadzacies, "ProwadzacyID", "Imie", hospitujacy.ProwadzacyID);
            return View(hospitujacy);
        }

      
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Hospitujacy hospitujacy = db.Hospitujacies.Find(id);
            if (hospitujacy == null)
            {
                return HttpNotFound();
            }
            ViewBag.ProtokolyID = new SelectList(db.Protokolies, "ProtokolyID", "Kurs", hospitujacy.ProtokolyID);
            ViewBag.ProwadzacyID = new SelectList(db.Prowadzacies, "ProwadzacyID", "Imie", hospitujacy.ProwadzacyID);
            return View(hospitujacy);
        }

      
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "HospitujacyID,ProwadzacyID,ProtokolyID")] Hospitujacy hospitujacy)
        {
            if (ModelState.IsValid)
            {
                db.Entry(hospitujacy).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ProtokolyID = new SelectList(db.Protokolies, "ProtokolyID", "Kurs", hospitujacy.ProtokolyID);
            ViewBag.ProwadzacyID = new SelectList(db.Prowadzacies, "ProwadzacyID", "Imie", hospitujacy.ProwadzacyID);
            return View(hospitujacy);
        }

     
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Hospitujacy hospitujacy = db.Hospitujacies.Find(id);
            if (hospitujacy == null)
            {
                return HttpNotFound();
            }
            return View(hospitujacy);
        }

     
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Hospitujacy hospitujacy = db.Hospitujacies.Find(id);
            db.Hospitujacies.Remove(hospitujacy);
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
