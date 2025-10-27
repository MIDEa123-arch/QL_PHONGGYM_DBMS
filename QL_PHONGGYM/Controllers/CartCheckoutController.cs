using QL_PHONGGYM.Models;
using QL_PHONGGYM.Repositories;
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
            _cartRepo = new CartRepository(new QL_PHONGGYMEntities2());
        }

        public ActionResult ToCheckOut()
        {
            int maKH = 21;
            var cart = _cartRepo.GetCart(maKH);
            
            return View(cart);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddToCart(int maSP, int soLuong)
        {
            int maKH = (int)Session["MaKH"];

            var add = _cartRepo.AddToCart(maKH, maSP, null, null, soLuong);
            return RedirectToAction("ToCheckOut");
        }
    }
}