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
    // 1. ĐÃ THÊM [Authorize]
    // Nó sẽ hoạt động 100% vì chúng ta đã sửa Global.asax.cs
    
    public class NhanVienChuyenMonsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: NhanVienChuyenMons
        public ActionResult Index()
        {
            var nhanVienChuyenMons = db.NhanVienChuyenMons.Include(n => n.ChuyenMon).Include(n => n.NhanVien);
            return View(nhanVienChuyenMons.ToList());
        }

        // GET: NhanVienChuyenMons/Details/5
        // =============================================
        // == ĐÃ SỬA: Nhận vào 2 tham số (MaNV, MaCM) ==
        // =============================================
        public ActionResult Details(int? MaNV, int? MaCM)
        {
            if (MaNV == null || MaCM == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            // Tìm bằng 2 khóa
            NhanVienChuyenMon nhanVienChuyenMon = db.NhanVienChuyenMons.Find(MaNV, MaCM);
            if (nhanVienChuyenMon == null)
            {
                return HttpNotFound();
            }
            return View(nhanVienChuyenMon);
        }

        // GET: NhanVienChuyenMons/Create
        public ActionResult Create()
        {
            ViewBag.MaCM = new SelectList(db.ChuyenMons, "MaCM", "TenChuyenMon");
            ViewBag.MaNV = new SelectList(db.NhanViens, "MaNV", "TenNV");
            return View();
        }

        // POST: NhanVienChuyenMons/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MaNV,MaCM")] NhanVienChuyenMon nhanVienChuyenMon)
        {
            if (ModelState.IsValid)
            {
                db.NhanVienChuyenMons.Add(nhanVienChuyenMon);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.MaCM = new SelectList(db.ChuyenMons, "MaCM", "TenChuyenMon", nhanVienChuyenMon.MaCM);
            ViewBag.MaNV = new SelectList(db.NhanViens, "MaNV", "TenNV", nhanVienChuyenMon.MaNV);
            return View(nhanVienChuyenMon);
        }

        // GET: NhanVienChuyenMons/Edit/5
        // =============================================
        // == ĐÃ SỬA: Nhận vào 2 tham số (MaNV, MaCM) ==
        // =============================================
        public ActionResult Edit(int? MaNV, int? MaCM)
        {
            if (MaNV == null || MaCM == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NhanVienChuyenMon nhanVienChuyenMon = db.NhanVienChuyenMons.Find(MaNV, MaCM);
            if (nhanVienChuyenMon == null)
            {
                return HttpNotFound();
            }
            ViewBag.MaCM = new SelectList(db.ChuyenMons, "MaCM", "TenChuyenMon", nhanVienChuyenMon.MaCM);
            ViewBag.MaNV = new SelectList(db.NhanViens, "MaNV", "TenNV", nhanVienChuyenMon.MaNV);
            return View(nhanVienChuyenMon);
        }

        // POST: NhanVienChuyenMons/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MaNV,MaCM")] NhanVienChuyenMon nhanVienChuyenMon)
        {
            if (ModelState.IsValid)
            {
                db.Entry(nhanVienChuyenMon).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MaCM = new SelectList(db.ChuyenMons, "MaCM", "TenChuyenMon", nhanVienChuyenMon.MaCM);
            ViewBag.MaNV = new SelectList(db.NhanViens, "MaNV", "TenNV", nhanVienChuyenMon.MaNV);
            return View(nhanVienChuyenMon);
        }

        // GET: NhanVienChuyenMons/Delete/5
        // =============================================
        // == ĐÃ SỬA: Nhận vào 2 tham số (MaNV, MaCM) ==
        // =============================================
        public ActionResult Delete(int? MaNV, int? MaCM)
        {
            if (MaNV == null || MaCM == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NhanVienChuyenMon nhanVienChuyenMon = db.NhanVienChuyenMons.Find(MaNV, MaCM);
            if (nhanVienChuyenMon == null)
            {
                return HttpNotFound();
            }
            return View(nhanVienChuyenMon);
        }

        // POST: NhanVienChuyenMons/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        // =============================================
        // == ĐÃ SỬA: Nhận vào 2 tham số (MaNV, MaCM) ==
        // =============================================
        public ActionResult DeleteConfirmed(int MaNV, int MaCM)
        {
            NhanVienChuyenMon nhanVienChuyenMon = db.NhanVienChuyenMons.Find(MaNV, MaCM);
            db.NhanVienChuyenMons.Remove(nhanVienChuyenMon);
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