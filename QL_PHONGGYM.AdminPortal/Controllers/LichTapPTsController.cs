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
    public class LichTapPTsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: LichTapPTs
        public ActionResult Index()
        {
            var lichTapPTs = db.LichTapPTs.Include(l => l.DangKyPT);
            return View(lichTapPTs.ToList());
        }

        // GET: LichTapPTs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LichTapPT lichTapPT = db.LichTapPTs.Find(id);
            if (lichTapPT == null)
            {
                return HttpNotFound();
            }
            return View(lichTapPT);
        }

        // GET: LichTapPTs/Create
        public ActionResult Create()
        {
            ViewBag.MaDKPT = new SelectList(db.DangKyPTs, "MaDKPT", "TrangThai");
            return View();
        }

        // POST: LichTapPTs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MaLichPT,MaDKPT,NgayTap,GioBatDau,GioKetThuc,TrangThai")] LichTapPT lichTapPT)
        {
            if (ModelState.IsValid)
            {
                db.LichTapPTs.Add(lichTapPT);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.MaDKPT = new SelectList(db.DangKyPTs, "MaDKPT", "TrangThai", lichTapPT.MaDKPT);
            return View(lichTapPT);
        }

        // GET: LichTapPTs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LichTapPT lichTapPT = db.LichTapPTs.Find(id);
            if (lichTapPT == null)
            {
                return HttpNotFound();
            }
            ViewBag.MaDKPT = new SelectList(db.DangKyPTs, "MaDKPT", "TrangThai", lichTapPT.MaDKPT);
            return View(lichTapPT);
        }

        // POST: LichTapPTs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MaLichPT,MaDKPT,NgayTap,GioBatDau,GioKetThuc,TrangThai")] LichTapPT lichTapPT)
        {
            if (ModelState.IsValid)
            {
                db.Entry(lichTapPT).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MaDKPT = new SelectList(db.DangKyPTs, "MaDKPT", "TrangThai", lichTapPT.MaDKPT);
            return View(lichTapPT);
        }

        // GET: LichTapPTs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LichTapPT lichTapPT = db.LichTapPTs.Find(id);
            if (lichTapPT == null)
            {
                return HttpNotFound();
            }
            return View(lichTapPT);
        }

        // POST: LichTapPTs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            LichTapPT lichTapPT = db.LichTapPTs.Find(id);
            db.LichTapPTs.Remove(lichTapPT);
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
