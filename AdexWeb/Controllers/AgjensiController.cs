using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BusinessLayer;

namespace AdexWeb.Controllers
{
    public class AgjensiController : Controller
    {
        // GET: Agjensi
       
            public ActionResult Index()
            {
                if (Session["UserID"] != null)
                {
                    KlientiBusiness kb = new KlientiBusiness();

                    return View(kb.Klientet.Where(s=>s.agjensi==true).ToList());
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
                    Utility utility = new Utility();

                    KlientiBusiness kb = new KlientiBusiness();
                    Klienti kl = kb.Klientet.Single(s => s.id == id);
                    ViewBag.IdDispecheri = new SelectList(utility.GetDispecher(), "id", "emri", kl.idDispecheri);
                    ViewBag.IdMagazina = new SelectList(utility.GetMagazina(), "id", "emri", kl.idMagazina);
                    return View(kl);

                }
                else
                {
                    return RedirectToAction("Index", "Hyrje");
                }
            }

            [HttpPost]
            public ActionResult Edit(Klienti klienti)
            {
                if (Session["UserID"] != null)
                {
                    KlientiBusiness kb = new KlientiBusiness();
                    if (ModelState.IsValid)

                    {
                        if (kb.Klientet.Any(ac => ac.emri.Equals(klienti.emri) && !ac.id.Equals(klienti.id)))
                        {
                            //error
                            ModelState.AddModelError(string.Empty, "Ky Klient egziston");
                            return View();
                        }
                        else
                        {
                        klienti.agjensi = true;
                        kb.modifiko(klienti);
                            return RedirectToAction("Index", "Agjensi");
                        }

                    }
                    else
                    {
                        Utility utility = new Utility();
                        ViewBag.IdDispecheri = new SelectList(utility.GetDispecher(), "id", "emri");
                        ViewBag.IdMagazina = new SelectList(utility.GetMagazina(), "id", "emri");
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
                    ViewBag.IdDispecheri = new SelectList(utility.GetDispecher(), "id", "emri");
                    ViewBag.IdMagazina = new SelectList(utility.GetMagazina(), "id", "emri");

                    return View();
                }
                else
                {
                    return RedirectToAction("Index", "Hyrje");
                }

            }
            [HttpPost]
            public ActionResult Create(Klienti klienti)
            {
                if (Session["UserID"] != null)
                {
                    KlientiBusiness kb = new KlientiBusiness();
                    if (ModelState.IsValid)

                    {
                        if (kb.Klientet.Any(ac => ac.emri.Equals(klienti.emri)))
                        {
                            //error
                            ModelState.AddModelError(string.Empty, "Egziston nje klient me kete emer !");
                            return View();
                        }
                        else
                        {
                        klienti.agjensi = true;
                            kb.shto(klienti);
                            return RedirectToAction("Index", "Agjensi");
                        }


                    }
                    else

                    {

                        Utility utility = new Utility();
                        ViewBag.IdDispecheri = new SelectList(utility.GetDispecher(), "id", "emri");
                        ViewBag.IdMagazina = new SelectList(utility.GetMagazina(), "id", "emri");
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
                    KlientiBusiness kb = new KlientiBusiness();

                    kb.fshi(Convert.ToInt16( id));
                   return RedirectToAction("Index", "Agjensi");
                }
                else
                {
                    return RedirectToAction("Index", "Hyrje");
                }
            }

            [HttpGet]
            public ActionResult Detaje(int id)
            {
                if (Session["UserID"] != null)
                {
                    KlientiBusiness kb = new KlientiBusiness();
                    Klienti kl = kb.Klientet.Single(s => s.id == id);
                    return View(kl);

                }
                else
                {
                    return RedirectToAction("Index", "Hyrje");
                }
            }

        public string fshihet(Int64 id_klienti)
        {

            SkontoBusiness sb = new SkontoBusiness();
            ShoferiBusiness shb = new ShoferiBusiness();
            OrderBusiness ob = new OrderBusiness();
            if (sb.Skontot.Any(ac => ac.idKlienti.Equals(id_klienti)) || shb.Shoferet.Any(ac => ac.IdKlienti.Equals(id_klienti))|| ob.Orders.Any(ac => ac.IdKlienti.Equals(id_klienti)))
                return "false";
            else
                return "true";

        }

    }
    }