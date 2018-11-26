using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ClockRestoration.Controllers
{
    public class AdminController : Controller
    {
        
        public ActionResult AdminPanel()
        {
            return View();
        }
    }
}