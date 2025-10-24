using QL_PHONGGYM.Models;
using QL_PHONGGYM.Repositories;
using QL_PHONGGYM.ViewModel;
using System;
using System.Linq;
using System.Web.Mvc;

namespace QL_PHONGGYM.Controllers
{
    public class AccountController : Controller
    {
        private readonly AccountRepository _accountRepo;

        public AccountController()
        {
            _accountRepo = new AccountRepository(new QL_PHONGGYMEntities2());
        }

        // GET: /Account/Register
        public ActionResult Register()
        {
            return View();
        }

        // POST: /Account/Register
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(KhachHangRegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    bool isSuccess = _accountRepo.CusRegister(model);

                    if (isSuccess)
                    {
                        TempData["SuccessMessage"] = "Đăng ký thành công! Vui lòng đăng nhập.";
                        return RedirectToAction("Login", "Account");
                    }
                    else
                    {
                        ModelState.AddModelError("", "Đăng ký thất bại. Vui lòng thử lại.");
                    }
                }
                catch (Exception ex)
                {
                    // Lấy thông báo lỗi chi tiết nếu có
                    string errorMsg = ex.InnerException?.Message ?? ex.Message;
                    ModelState.AddModelError("", errorMsg);
                }
            }

            return View(model);
        }

        public ActionResult Login()
        {
            return View();
        }

        // POST: /Account/Login
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(KhachHangLoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = _accountRepo.CusLogin(model.TenDangNhap, model.MatKhau);

                if (user != null)
                {
                    Session["MaKH"] = user.MaKH;
                    string fullName = user.TenKH;                 
                    string firstName = fullName.Split(' ').Last();
                    Session["TenKH"] = firstName;

                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ViewBag.Err =  "Tên đăng nhập hoặc mật khẩu không chính xác.";
                }
            }
            return View(model);
        }


        // GET: /Account/Logout
        public ActionResult Logout()
        {
            Session.Clear();
            return RedirectToAction("Index", "Home");
        }
    }
}
