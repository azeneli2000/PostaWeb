using BusinessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AdexWeb.Controllers
{
    public class SkontoController : Controller
    {
        // GET: Skonto


        public ActionResult Index()
        {
            if (Session["UserID"] != null)
            {
                SkontoBusiness sb = new SkontoBusiness();

                return View(sb.Skontot.ToList());
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
                SkontoBusiness sb = new SkontoBusiness();
                Skonto sk = sb.Skontot.Single(s => s.id == id);
                return View(sk);

            }
            else
            {
                return RedirectToAction("Index", "Hyrje");
            }
        }

        [HttpPost]
        public ActionResult Edit(Skonto sk)
        {
            if (Session["UserID"] != null)
            {
                SkontoBusiness sb = new SkontoBusiness();
                if (ModelState.IsValid)

                {
                    sb.modifiko(sk);
                    return RedirectToAction("Index", "Skonto");
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