using BusinessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AdexWeb.Controllers
{
    public class ShoferiController : Controller
    {
        // GET: Shoferi
        [HttpGet]
        public ActionResult Index()
        {

            if (Session["UserID"] != null)
            {
                ShoferiBusiness shb = new ShoferiBusiness();

                return View(shb.Shoferet.ToList());
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
                ShoferiBusiness shb = new ShoferiBusiness();
                Shoferi shof = shb.Shoferet.Single(s => s.Id == id);
                Utility utility = new Utility();
                ViewBag.IdDispecher = new SelectList(utility.GetDispecher(), "id", "emri",shof.IdDispecher);
                ViewBag.IdKlienti = new SelectList(utility.GetKlient(), "id", "emri",shof.IdKlienti);
                return View(shof);
            }
            else
            {
                return RedirectToAction("Index", "Hyrje");
            }

        }

        [HttpPost]
        public ActionResult Edit(Shoferi shof)
        {
            if (Session["UserID"] != null)
            {
                ShoferiBusiness shb = new ShoferiBusiness();
                if (ModelState.IsValid)

                {
                    if (shb.Shoferet.Any(ac => ac.perdoruesi.Equals(shof.perdoruesi) && !ac.Id.Equals(shof.Id)))
                    {
                        //error
                        ModelState.AddModelError(string.Empty, "Ky shofer egziston");


                        return View();
                    }
                    else
                    {
                        shb.modifiko(shof);
                        return RedirectToAction("Index", "Shoferi");
                    }

                }
                else
                {
                    Utility utility = new Utility();
                    ViewBag.IdDispecher = new SelectList(utility.GetDispecher(), "id", "emri");
                    ViewBag.IdKlienti = new SelectList(utility.GetKlient(), "id", "emri");
                    return View();

                }
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
                Utility utility = new Utility();
                ViewBag.IdDispecher = new SelectList(utility.GetDispecher(), "id", "emri");
                ViewBag.IdKlienti = new SelectList(utility.GetKlient(), "id", "emri");

                ShoferiBusiness shb = new ShoferiBusiness();

                return View();
            }
            else
            {
                return RedirectToAction("Index", "Hyrje");
            }

        }
        [HttpPost]
        public ActionResult Create(Shoferi shof)
        {
            if (Session["UserID"] != null)
            {
                ShoferiBusiness shb = new ShoferiBusiness();
                if (ModelState.IsValid)

                {
                    if (shb.Shoferet.Any(ac => ac.perdoruesi.Equals(shof.perdoruesi)))
                    {
                        //error
                        ModelState.AddModelError(string.Empty, "Egziston nje shofer me kete emer !");
                        return View();
                    }
                    else
                    {
                        shb.shto(shof);
                        return RedirectToAction("Index", "Shoferi");
                    }


                }
                else
                {
                    Utility utility = new Utility();
                    ViewBag.IdDispecher = new SelectList(utility.GetDispecher(), "id", "emri");
                    ViewBag.IdKlienti = new SelectList(utility.GetKlient(), "id", "emri");
                    return View();
                }
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
                ShoferiBusiness mb = new ShoferiBusiness();

                mb.fshi(Convert.ToInt16(id));
                return RedirectToAction("Index", "Shoferi");
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
            //KlientiBusiness kb = new KlientiBusiness();
            OrderBusiness ob = new OrderBusiness();
            if (ob.Orders.Any(ac => ac.DriverIdDorezimi.Equals(id_klienti)) || ob.Orders.Any(ac => ac.DriverIdPickUp.Equals(id_klienti))|| ob.Orders.Any(ac => ac.DriverIdKthyerMag.Equals(id_klienti)))
                return "false";
            else
                return "true";

        }
    }
}