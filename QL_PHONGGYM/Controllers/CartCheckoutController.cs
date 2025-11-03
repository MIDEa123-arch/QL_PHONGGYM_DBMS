using QL_PHONGGYM.Models;
using QL_PHONGGYM.Repositories;
using QL_PHONGGYM.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QL_PHONGGYM.Controllers
{
    public class CartCheckoutController : Controller
    {
        private readonly CartRepository _cartRepo;

        public CartCheckoutController()
        {            
            _cartRepo = new CartRepository(new QL_PHONGGYMEntities());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult XoaVaThanhToan(FormCollection form, string actionType)
        {
            int maKH = (int)Session["MaKH"];

            if (actionType == "delete")
            {
                _cartRepo.XoaDaChon(form, maKH);
                Session["cart"] = _cartRepo.GetCart(maKH);
                return RedirectToAction("ToCheckOut");
            }
            else if (actionType == "checkout")
            {

                var list = _cartRepo.ChonSanPham(form, maKH, (List<GioHangViewModel>)Session["cart"]);
                Session["cart"] = list;
                return RedirectToAction("ThanhToan");
            }

            return RedirectToAction("ToCheckOut");            
        }

        public ActionResult ThanhToan()
        {
            var list = (List<GioHangViewModel>)Session["cart"];

            return View(list);
        }
        public ActionResult ToCheckOut()
        {
            int maKH = (int)Session["MaKH"];
            var cart = _cartRepo.GetCart(maKH);
            
            return View(cart);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddToCart(int maSP, int soLuong)
        {
            int maKH = (int)Session["MaKH"];

            var add = _cartRepo.AddToCart(maKH, maSP, null, null, soLuong);
            Session["cart"] = _cartRepo.GetCart(maKH);
            return RedirectToAction("ToCheckOut");
        }
    }
}