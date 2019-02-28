using BusinessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AdexWeb.Controllers
{
    public class ZonaController : Controller
    {
        // GET: Zona
        public ActionResult Index()
        {
            if (Session["UserID"] != null)
            {
                ZonaBusiness zb = new ZonaBusiness();

                return View(zb.Zonat.ToList());
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
                ZonaBusiness zb = new ZonaBusiness();
                Zona zona = zb.Zonat.Single(s => s.id == id);
                return View(zona);

            }
            else
            {
                return RedirectToAction("Index", "Hyrje");
            }
        }

        [HttpPost]
        public ActionResult Edit(Zona zona)
        {
            if (Session["UserID"] != null)
            {
                ZonaBusiness zb = new ZonaBusiness();
                if (ModelState.IsValid)

                {
                    if (zb.Zonat.Any(ac => ac.emri.Equals(zona.emri) && !ac.id.Equals(zona.id)))
                    {
                        //error
                        ModelState.AddModelError(string.Empty, "Kjo zone egziston !");
                        return View();
                    }
                    else
                    {
                        zb.modifiko(zona);
                        return RedirectToAction("Index", "Zona");
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
                //UserBusiness kb = new UserBusiness();

                return View();
            }
            else
            {
                return RedirectToAction("Index", "Hyrje");
            }

        }
        [HttpPost]
        public ActionResult Create(Zona zona)
        {
            if (Session["UserID"] != null)
            {
                ZonaBusiness zb = new ZonaBusiness();
                if (ModelState.IsValid)

                {
                    if (zb.Zonat.Any(ac => ac.emri.Equals(zona.emri)))
                    {
                        //error
                        ModelState.AddModelError(string.Empty, "Egziston nje zone me kete emer !");
                        return View();
                    }
                    else
                    {
                        zb.shto(zona);
                        return RedirectToAction("Index", "Zona");
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
                ZonaBusiness zb = new ZonaBusiness();

                zb.fshi(Convert.ToInt16( id));
                return RedirectToAction("Index", "Zona");
            }
            else
            {
                return RedirectToAction("Index", "Hyrje");
            }
        }

        public string fshihet(Int64 id_klienti)
        {

            //SkontoBusiness sb = new SkontoBusiness();
            //ShoferiBusiness shb = new ShoferiBusiness();
            OrderBusiness ob = new OrderBusiness();
            if (ob.Orders.Any(ac => ac.IdZona.Equals(id_klienti)))
                return "false";
            else
                return "true";

        }
    }
}