using BusinessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AdexWeb.Controllers
{
    public class CmimetController : Controller
    {
        
        public ActionResult Index()
        {
            if (Session["UserID"] != null)
            {
               CmimetBusiness cb = new CmimetBusiness();

                return View(cb.Cmimet.ToList());
            }
            else
            {
                return RedirectToAction("Index", "Hyrje");
            }
        }
        [HttpGet]
        public ActionResult Edit(int id)
        {
            if (Session["UserID"] != null)
            {
                CmimetBusiness cb = new CmimetBusiness();
                Cmimet cm = cb.Cmimet.Single(s => s.id == id);
                return View(cm);

            }
            else
            {
                return RedirectToAction("Index", "Hyrje");
            }
        }

        [HttpPost]
        public ActionResult Edit(Cmimet cmimet)
        {
            if (Session["UserID"] != null)
            {
                CmimetBusiness cb = new CmimetBusiness();
                if (ModelState.IsValid)

                {
                    cb.modifiko(cmimet);
                    return RedirectToAction("Index", "Cmimet");
                }
                else
                    return View();
            }
                     
            {
                return RedirectToAction("Index", "Hyrje");
            }
        }

    }
}