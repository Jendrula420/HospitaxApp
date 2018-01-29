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
    public class OcenySlownesController : Controller
    {
        private HospitaxDBEntities db = new HospitaxDBEntities();

      
        public ActionResult Index()
        {
            var ocenySlownes = db.OcenySlownes.Include(o => o.Protokoly);
            return View(ocenySlownes.ToList());
        }

    
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OcenySlowne ocenySlowne = db.OcenySlownes.Find(id);
            if (ocenySlowne == null)
            {
                return HttpNotFound();
            }
            return View(ocenySlowne);
        }

    
        public ActionResult Create()
        {
            ViewBag.ProtokolyID = new SelectList(db.Protokolies, "ProtokolyID", "Kurs");
            return View();
        }

    
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "OcenySlowneID,Pytanie,Odpowiedz,ProtokolyID")] OcenySlowne ocenySlowne)
        {
            if (ModelState.IsValid)
            {
                db.OcenySlownes.Add(ocenySlowne);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ProtokolyID = new SelectList(db.Protokolies, "ProtokolyID", "Kurs", ocenySlowne.ProtokolyID);
            return View(ocenySlowne);
        }

   
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OcenySlowne ocenySlowne = db.OcenySlownes.Find(id);
            if (ocenySlowne == null)
            {
                return HttpNotFound();
            }
            ViewBag.ProtokolyID = new SelectList(db.Protokolies, "ProtokolyID", "Kurs", ocenySlowne.ProtokolyID);
            return View(ocenySlowne);
        }

     
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "OcenySlowneID,Pytanie,Odpowiedz,ProtokolyID")] OcenySlowne ocenySlowne)
        {
            if (ModelState.IsValid)
            {
                db.Entry(ocenySlowne).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ProtokolyID = new SelectList(db.Protokolies, "ProtokolyID", "Kurs", ocenySlowne.ProtokolyID);
            return View(ocenySlowne);
        }

     
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OcenySlowne ocenySlowne = db.OcenySlownes.Find(id);
            if (ocenySlowne == null)
            {
                return HttpNotFound();
            }
            return View(ocenySlowne);
        }

  
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            OcenySlowne ocenySlowne = db.OcenySlownes.Find(id);
            db.OcenySlownes.Remove(ocenySlowne);
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
