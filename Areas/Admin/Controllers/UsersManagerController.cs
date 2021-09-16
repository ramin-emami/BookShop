using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace BookShop.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class UsersManagerController : Controller
    {
        public IActionResult Index(string Msg)
        {
            if(Msg=="Success")
            {
                ViewBag.Alert = "عضویت با موفقیت انجام شد.";
            }
            return View();
        }
    }
}