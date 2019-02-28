using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BusinessLayer;
namespace AdexWeb.Controllers
{
    public class HyrjeController : Controller
    {
       [HttpGet]
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Index(Users user)
        {
            UserBusiness bl = new UserBusiness();
         
           Users  perd = bl.Perdoruesit.Where(s => s.perdoruesi.Equals ( user.perdoruesi) && s.password.Equals ( user.password)).FirstOrDefault();
            //gjej username dhe password nga database
            if (perd != null)
            {
                Session["userID"] = perd.perdoruesi.ToString();

               return RedirectToAction("Index","Blank");
            }
            else

                return View();
        }
    }
}