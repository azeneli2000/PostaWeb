using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BusinessLayer;

namespace AdexWeb.Controllers
{
    public class DispecherController : Controller
    {
        // GET: Dispecher
        [HttpGet]
        public ActionResult Index()
        {

            if (Session["UserID"] != null)
            {
                DispecherBusiness db = new DispecherBusiness();

                return View(db.Dispecherat.ToList());
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
                DispecherBusiness db = new DispecherBusiness();
                Dispecher dis = db.Dispecherat.Single(s => s.id == id);
                return View(dis);
            }
            else
            {
                return RedirectToAction("Index", "Hyrje");
            }

        }

        [HttpPost]
        public ActionResult Edit(Dispecher dis)
        {
            if (Session["UserID"] != null)
            {
                DispecherBusiness db = new DispecherBusiness();
                if (ModelState.IsValid)

                {
                    if (db.Dispecherat.Any(ac => ac.perdoruesi.Equals(dis.perdoruesi) && !ac.id.Equals(dis.id)))
                    {
                        //error
                        ModelState.AddModelError(string.Empty, "Ky dispecher egziston");
                        return View();
                    }
                    else
                    {
                        db.modifiko(dis);
                        return RedirectToAction("Index", "Dispecher");
                    }

                }
                return View();
            }
            else
            {
                return RedirectToAction("Index", "Hyrje");
            }
        }

        [HttpGet]
        public ActionResult Create()
        {
            if (Session["UserID"] != null)
            {
                DispecherBusiness db = new DispecherBusiness();

                return View();
            }
            else
            {
                return RedirectToAction("Index", "Hyrje");
            }

        }
        [HttpPost]
        public ActionResult Create(Dispecher dis)
        {
            if (Session["UserID"] != null)
            {
                DispecherBusiness db = new DispecherBusiness();
                if (ModelState.IsValid)

                {
                    if (db.Dispecherat.Any(ac => ac.perdoruesi.Equals(dis.perdoruesi)))
                    {
                        //error
                        ModelState.AddModelError(string.Empty, "Egziston nje dispecher me kete perdorues !");
                        return View();
                    }
                    else
                    {
                        db.shto(dis);
                        return RedirectToAction("Index", "Dispecher");
                    }


                }
                else
                    return View();
            }
            else
            {
                return RedirectToAction("Index", "Hyrje");
            }
        }

       
        public ActionResult Fshi(string id)
        {
            if (Session["UserID"] != null)
            {
                DispecherBusiness db = new DispecherBusiness();

                db.fshi(Convert.ToInt16(id));
                return RedirectToAction("Index", "Dispecher");
            }
            else
            {
                return RedirectToAction("Index", "Hyrje");
            }
        }

        public string fshihet (Int64 id_dispecheri)
        {

            KlientiBusiness kb = new KlientiBusiness();
            ShoferiBusiness sb = new ShoferiBusiness();
            if (kb.Klientet.Any(ac => ac.idDispecheri.Equals(id_dispecheri)) || sb.Shoferet.Any(ac => ac.IdDispecher.Equals(id_dispecheri)))
                return "false";
            else
                return "true";

        }
    }
}