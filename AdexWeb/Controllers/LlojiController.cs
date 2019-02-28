using BusinessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AdexWeb.Controllers
{
    public class LlojiController : Controller
    {
        // GET: Lloji
        public ActionResult Index()
        {
            if (Session["UserID"] != null)
            {
                LlojiBusiness lb = new LlojiBusiness();

                return View(lb.Llojet.ToList());
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
                LlojiBusiness lb = new LlojiBusiness();
                Lloji lloji = lb.Llojet.Single(s => s.id == id);
                return View(lloji);

            }
            else
            {
                return RedirectToAction("Index", "Hyrje");
            }
        }

        [HttpPost]
        public ActionResult Edit(Lloji lloji)
        {
            if (Session["UserID"] != null)
            {
                LlojiBusiness lb = new LlojiBusiness();
                if (ModelState.IsValid)

                {
                    if (lb.Llojet.Any(ac => ac.emri.Equals(lloji.emri) && !ac.id.Equals(lloji.id)))
                    {
                        //error
                        ModelState.AddModelError(string.Empty, "Ky lloj egziston");
                        return View();
                    }
                    else
                    {
                        lb.modifiko(lloji);
                        return RedirectToAction("Index", "Lloji");
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
        public ActionResult Create(Lloji lloji)
        {
            if (Session["UserID"] != null)
            {
                LlojiBusiness lb = new LlojiBusiness();
                if (ModelState.IsValid)

                {
                    if (lb.Llojet.Any(ac => ac.emri.Equals(lloji.emri)))
                    {
                        //error
                        ModelState.AddModelError(string.Empty, "Egziston nje zone me kete emer !");
                        return View();
                    }
                    else
                    {
                        lb.shto(lloji);
                        return RedirectToAction("Index", "Lloji");
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
                LlojiBusiness lb = new LlojiBusiness();

                lb.fshi(Convert.ToInt16( id));
                return RedirectToAction("Index", "Lloji");
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
            if (ob.Orders.Any(ac => ac.IdLloji.Equals(id_klienti)))
                return "false";
            else
                return "true";

        }
    }
}