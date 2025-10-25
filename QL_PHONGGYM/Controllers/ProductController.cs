using QL_PHONGGYM.Models;
using QL_PHONGGYM.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QL_PHONGGYM.Controllers
{
    public class ProductController : Controller
    {
        private readonly ProductRepository _productRepo;

        public ProductController()
        {
            _productRepo = new ProductRepository(new QL_PHONGGYMEntities2());
        }

        public ActionResult ProductDetail(int id)
        {
            var sanpham = _productRepo.GetSanPhams().FirstOrDefault(sp => sp.MaSP == id);
            return View(sanpham);
        }

        public ActionResult Product(string loaisp, string hang, string xuatXu, decimal? maxPrice, decimal? minPrice)
        {
            var list = _productRepo.GetSanPhams();

            if (!string.IsNullOrEmpty(xuatXu))
            {
                list = list.Where(p => p.XuatXu.Contains(xuatXu)).OrderByDescending(sp => sp.SoLuongTon).ToList();
            }
            if (!string.IsNullOrEmpty(loaisp))
            {
                list = list.Where(p => p.LoaiSP.Contains(loaisp)).OrderByDescending(sp => sp.SoLuongTon).ToList();
            }

            if (!string.IsNullOrEmpty(hang))
            {
                list = list.Where(p => p.Hang.Contains(hang)).OrderByDescending(sp => sp.SoLuongTon).ToList();
            }

            if (minPrice.HasValue)
            {
                list = list.Where(p => p.DonGia >= minPrice.Value).OrderByDescending(sp => sp.SoLuongTon).ToList();
            }

            if (maxPrice.HasValue)
            {
                list = list.Where(p => p.DonGia <= maxPrice.Value).OrderByDescending(sp => sp.SoLuongTon).ToList();
            }

            ViewBag.LoaiSP = _productRepo.GetLoaiSanPhams().ToList();
            ViewBag.Hang = list.Where(p => p.Hang != null).Select(p => p.Hang).Distinct().ToList();
            return View(list);
        }
        public ActionResult ClassMenu()
        {
            var list = _productRepo.GetChuyenMons();
            return PartialView(list);
        }
        
        public ActionResult CategoriesMenu()
        {
            var list = _productRepo.GetLoaiSanPhams();
            return PartialView(list);
        }
        public ActionResult BrandMenu()
        {
            var hangs = _productRepo.GetHangsByLoai();
            return PartialView(hangs);
        }

        public ActionResult OriginMenu()
        {
            var xuatsu = _productRepo.GetXuatSu();
            return PartialView(xuatsu);
        }
    }
}
