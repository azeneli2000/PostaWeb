using BusinessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AdexWeb.Controllers
{
    public class PerdoruesiController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {

            if (Session["UserID"] != null)
            {
                UserBusiness ub = new UserBusiness();

                return View(ub.Perdoruesit.ToList());
            }
            else
            {
                return RedirectToAction("Index","Hyrje");
                }
        }
        [HttpGet]
        public ActionResult Edit(int id)
        {
            if (Session["UserID"] != null)
            {
                UserBusiness ub = new UserBusiness();
                Users perd = ub.Perdoruesit.Single(s => s.Id == id);
                return View(perd);
            }
            else
            {
                return RedirectToAction("Index", "Hyrje");
            }
         
        }

        [HttpPost]
        public ActionResult Edit(Users perd)
        {
            if (Session["UserID"] != null)
            {
                UserBusiness ub = new UserBusiness();
                if (ModelState.IsValid)

                {
                    if (ub.Perdoruesit.Any(ac => ac.perdoruesi.Equals(perd.perdoruesi) && !ac.Id.Equals(perd.Id)))
                    {
                        //error
                        ModelState.AddModelError(string.Empty, "Ky perdorues egziston");
                        return View();
                    }
                    else
                    {
                        ub.modifiko(perd);
                        return RedirectToAction("Index", "Perdoruesi");
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
                UserBusiness ub = new UserBusiness();

                return View();
            }
            else
            {
                return RedirectToAction("Index", "Hyrje");
            }

        }
        [HttpPost]
        public ActionResult Create(Users user)
        {
            if (Session["UserID"] != null)
            {
                UserBusiness ub = new UserBusiness();
                if (ModelState.IsValid)

                {
                    if (ub.Perdoruesit.Any(ac => ac.perdoruesi.Equals(user.perdoruesi)))
                    {
                        //error
                        ModelState.AddModelError(string.Empty, "Egziston nje perdorues me kete emer !");
                        return View();
                    }
                    else
                    {
                        ub.shto(user);
                        return RedirectToAction("Index", "Perdoruesi");
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
       
        [HttpPost]
        public ActionResult Fshi(int id)
        {
            if (Session["UserID"] != null)
            {
                UserBusiness ub = new UserBusiness();
              
                    ub.fshi(id);
                return RedirectToAction("Index", "Perdoruesi");
            }
            else
            {
                return RedirectToAction("Index", "Hyrje");
            }
        }

        }
    }