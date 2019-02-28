using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BusinessLayer;
namespace AdexWeb.Controllers
{
    public class MagazinaController : Controller
    {
        public ActionResult Index()
        {
            if (Session["UserID"] != null)
            {
                MagazinaBusiness mb = new MagazinaBusiness();

                return View(mb.Magazinat.ToList());
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
                MagazinaBusiness mb = new MagazinaBusiness();
                Magazina mag = mb.Magazinat.Single(s => s.id == id);
                return View(mag);

            }
            else
            {
                return RedirectToAction("Index", "Hyrje");
            }
        }

        [HttpPost]
        public ActionResult Edit(Magazina mag)
        {
            if (Session["UserID"] != null)
            {
                MagazinaBusiness mb = new MagazinaBusiness();
                if (ModelState.IsValid)

                {
                    if (mb.Magazinat.Any(ac => ac.emri.Equals(mag.emri) && !ac.id.Equals(mag.id)))
                    {
                        //error
                        ModelState.AddModelError(string.Empty, "Kjo magazine egziston !");
                        return View();
                    }
                    else
                    {
                        mb.modifiko(mag);
                        return RedirectToAction("Index", "Magazina");
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
        public ActionResult Create(Magazina mag)
        {
            if (Session["UserID"] != null)
            {
                MagazinaBusiness mb = new MagazinaBusiness();
                if (ModelState.IsValid)

                {
                    if (mb.Magazinat.Any(ac => ac.emri.Equals(mag.emri)))
                    {
                        //error
                        ModelState.AddModelError(string.Empty, "Egziston nje magazine me kete emer !");
                        return View();
                    }
                    else
                    {
                        mb.shto(mag);
                        return RedirectToAction("Index", "Magazina");
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
                MagazinaBusiness mb = new MagazinaBusiness();

                mb.fshi(Convert.ToInt16( id));
                return RedirectToAction("Index", "Magazina");
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
            KlientiBusiness kb = new KlientiBusiness();
            OrderBusiness ob = new OrderBusiness();
            if (ob.Orders.Any(ac => ac.IdMagazina.Equals(id_klienti))|| kb.Klientet.Any(ac => ac.idMagazina.Equals(id_klienti)))
                return "false";
            else
                return "true";

        }
    }
}