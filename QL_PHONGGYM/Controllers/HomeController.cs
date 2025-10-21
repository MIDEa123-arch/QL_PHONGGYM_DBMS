using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using QL_PHONGGYM.Models;
using QL_PHONGGYM.ViewModel;
using QL_PHONGGYM.Repositories;

namespace QL_PHONGGYM.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult ClassMenu()
        {
            ListService listService = new ListService();   
            List<ChuyenMon> list = listService.GetList();

            return PartialView(list);
        }
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}