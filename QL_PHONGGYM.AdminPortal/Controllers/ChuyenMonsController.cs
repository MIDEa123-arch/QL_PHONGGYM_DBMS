using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using QL_PHONGGYM.AdminPortal.Data;
using QL_PHONGGYM.AdminPortal.Models;

namespace QL_PHONGGYM.AdminPortal.Controllers
{
    public class ChuyenMonsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: ChuyenMons
        public ActionResult Index()
        {
            return View(db.ChuyenMons.ToList());
        }

        // GET: ChuyenMons/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ChuyenMon chuyenMon = db.ChuyenMons.Find(id);
            if (chuyenMon == null)
            {
                return HttpNotFound();
            }
            return View(chuyenMon);
        }

        // GET: ChuyenMons/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ChuyenMons/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MaCM,TenChuyenMon,MoTa")] ChuyenMon chuyenMon)
        {
            if (ModelState.IsValid)
            {
                db.ChuyenMons.Add(chuyenMon);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(chuyenMon);
        }

        // GET: ChuyenMons/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ChuyenMon chuyenMon = db.ChuyenMons.Find(id);
            if (chuyenMon == null)
            {
                return HttpNotFound();
            }
            return View(chuyenMon);
        }

        // POST: ChuyenMons/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MaCM,TenChuyenMon,MoTa")] ChuyenMon chuyenMon)
        {
            if (ModelState.IsValid)
            {
                db.Entry(chuyenMon).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(chuyenMon);
        }

        // GET: ChuyenMons/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ChuyenMon chuyenMon = db.ChuyenMons.Find(id);
            if (chuyenMon == null)
            {
                return HttpNotFound();
            }
            return View(chuyenMon);
        }

        // POST: ChuyenMons/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ChuyenMon chuyenMon = db.ChuyenMons.Find(id);
            db.ChuyenMons.Remove(chuyenMon);
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
