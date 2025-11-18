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
    public class LichLopsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: LichLops
        public ActionResult Index()
        {
            var lichLops = db.LichLops.Include(l => l.LopHoc).Include(l => l.NhanVien);
            return View(lichLops.ToList());
        }

        // GET: LichLops/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LichLop lichLop = db.LichLops.Find(id);
            if (lichLop == null)
            {
                return HttpNotFound();
            }
            return View(lichLop);
        }

        // GET: LichLops/Create
        public ActionResult Create()
        {
            ViewBag.MaLop = new SelectList(db.LopHocs, "MaLop", "TenLop");
            ViewBag.MaNV = new SelectList(db.NhanViens, "MaNV", "TenNV");
            return View();
        }

        // POST: LichLops/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MaLichLop,MaLop,MaNV,NgayHoc,GioBatDau,GioKetThuc,TrangThai")] LichLop lichLop)
        {
            if (ModelState.IsValid)
            {
                db.LichLops.Add(lichLop);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.MaLop = new SelectList(db.LopHocs, "MaLop", "TenLop", lichLop.MaLop);
            ViewBag.MaNV = new SelectList(db.NhanViens, "MaNV", "TenNV", lichLop.MaNV);
            return View(lichLop);
        }

        // GET: LichLops/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LichLop lichLop = db.LichLops.Find(id);
            if (lichLop == null)
            {
                return HttpNotFound();
            }
            ViewBag.MaLop = new SelectList(db.LopHocs, "MaLop", "TenLop", lichLop.MaLop);
            ViewBag.MaNV = new SelectList(db.NhanViens, "MaNV", "TenNV", lichLop.MaNV);
            return View(lichLop);
        }
            
        // POST: LichLops/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MaLichLop,MaLop,MaNV,NgayHoc,GioBatDau,GioKetThuc,TrangThai")] LichLop lichLop)
        {
            if (ModelState.IsValid)
            {
                db.Entry(lichLop).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MaLop = new SelectList(db.LopHocs, "MaLop", "TenLop", lichLop.MaLop);
            ViewBag.MaNV = new SelectList(db.NhanViens, "MaNV", "TenNV", lichLop.MaNV);
            return View(lichLop);
        }

        // GET: LichLops/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LichLop lichLop = db.LichLops.Find(id);
            if (lichLop == null)
            {
                return HttpNotFound();
            }
            return View(lichLop);
        }

        // POST: LichLops/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            LichLop lichLop = db.LichLops.Find(id);
            db.LichLops.Remove(lichLop);
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
