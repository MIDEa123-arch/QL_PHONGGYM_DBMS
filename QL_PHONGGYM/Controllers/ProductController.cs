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
