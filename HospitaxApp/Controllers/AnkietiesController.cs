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
    public class AnkietiesController : Controller
    {
        private HospitaxDBEntities db = new HospitaxDBEntities();

      
        public ActionResult Index()
        {
            var ankieties = db.Ankieties.Include(a => a.Protokoly);
            return View(ankieties.ToList());
        }

       
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ankiety ankiety = db.Ankieties.Find(id);
            if (ankiety == null)
            {
                return HttpNotFound();
            }
            return View(ankiety);
        }

     
        public ActionResult Create()
        {
            ViewBag.ProtokolyID = new SelectList(db.Protokolies, "ProtokolyID", "Kurs");
            return View();
        }

      
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "AnkietyID,Pytanie,Ocena,ProtokolyID")] Ankiety ankiety)
        {
            if (ModelState.IsValid)
            {
                db.Ankieties.Add(ankiety);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ProtokolyID = new SelectList(db.Protokolies, "ProtokolyID", "Kurs", ankiety.ProtokolyID);
            return View(ankiety);
        }

      
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ankiety ankiety = db.Ankieties.Find(id);
            if (ankiety == null)
            {
                return HttpNotFound();
            }
            ViewBag.ProtokolyID = new SelectList(db.Protokolies, "ProtokolyID", "Kurs", ankiety.ProtokolyID);
            return View(ankiety);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "AnkietyID,Pytanie,Ocena,ProtokolyID")] Ankiety ankiety)
        {
            if (ModelState.IsValid)
            {
                db.Entry(ankiety).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ProtokolyID = new SelectList(db.Protokolies, "ProtokolyID", "Kurs", ankiety.ProtokolyID);
            return View(ankiety);
        }

      
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ankiety ankiety = db.Ankieties.Find(id);
            if (ankiety == null)
            {
                return HttpNotFound();
            }
            return View(ankiety);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Ankiety ankiety = db.Ankieties.Find(id);
            db.Ankieties.Remove(ankiety);
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
