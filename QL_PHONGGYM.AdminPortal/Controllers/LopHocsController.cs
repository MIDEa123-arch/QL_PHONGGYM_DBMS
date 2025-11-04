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
    public class LopHocsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: LopHocs
        public ActionResult Index()
        {
            var lopHocs = db.LopHocs.Include(l => l.ChuyenMon);
            return View(lopHocs.ToList());
        }

        // GET: LopHocs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LopHoc lopHoc = db.LopHocs.Find(id);
            if (lopHoc == null)
            {
                return HttpNotFound();
            }
            return View(lopHoc);
        }

        // GET: LopHocs/Create
        public ActionResult Create()
        {
            ViewBag.MaCM = new SelectList(db.ChuyenMons, "MaCM", "TenChuyenMon");
            return View();
        }

        // POST: LopHocs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MaLop,TenLop,MaCM,HocPhi,NgayBatDau,NgayKetThuc,SoBuoi,SiSoToiDa")] LopHoc lopHoc)
        {
            if (ModelState.IsValid)
            {
                db.LopHocs.Add(lopHoc);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.MaCM = new SelectList(db.ChuyenMons, "MaCM", "TenChuyenMon", lopHoc.MaCM);
            return View(lopHoc);
        }

        // GET: LopHocs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LopHoc lopHoc = db.LopHocs.Find(id);
            if (lopHoc == null)
            {
                return HttpNotFound();
            }
            ViewBag.MaCM = new SelectList(db.ChuyenMons, "MaCM", "TenChuyenMon", lopHoc.MaCM);
            return View(lopHoc);
        }

        // POST: LopHocs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MaLop,TenLop,MaCM,HocPhi,NgayBatDau,NgayKetThuc,SoBuoi,SiSoToiDa")] LopHoc lopHoc)
        {
            if (ModelState.IsValid)
            {
                db.Entry(lopHoc).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MaCM = new SelectList(db.ChuyenMons, "MaCM", "TenChuyenMon", lopHoc.MaCM);
            return View(lopHoc);
        }

        // GET: LopHocs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LopHoc lopHoc = db.LopHocs.Find(id);
            if (lopHoc == null)
            {
                return HttpNotFound();
            }
            return View(lopHoc);
        }

        // POST: LopHocs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            LopHoc lopHoc = db.LopHocs.Find(id);
            db.LopHocs.Remove(lopHoc);
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
