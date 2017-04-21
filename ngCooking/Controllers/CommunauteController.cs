using BLL.CommunauteBLL;
using BLL.RecetteBLL;
using ngCooking.Models;
using NgCooking.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Web.Mvc;

namespace ngCooking.Controllers
{
    public class CommunauteController : Controller
    {
        public CommunauteController()
        {
            ViewBag.PageActuelle = "Communaute";
            RecetteBLL receteee = new RecetteBLL();
            List<Recette> list = receteee.getRec(null, false, "za");
            var nbr = list.Count;
            ViewBag.NombreDeRecettes = nbr;
        }

        // GET: Communaute
        [AllowAnonymous]
        public ActionResult Index(string OptionCommunaute)
        {
            List<Communaute> list ;
            CommunauteBLL coom = new CommunauteBLL();
            if (string.IsNullOrWhiteSpace(OptionCommunaute))
            {
                list = coom.getCom(null, false, "az");
            }
            else
            {
                list = coom.getCom(null, false, OptionCommunaute);
            }
            return View(list);
        }

        // GET: Communaute/Details/5
        [AllowAnonymous]
        public ActionResult Details(int? id)
        {
            IngredientCategorieRecette rc = new IngredientCategorieRecette();
            CommunauteBLL comm = new CommunauteBLL();
            RecetteBLL rec = new RecetteBLL();
                     
            Expression<Func<Communaute, bool>> myFunc = x => x.idcommunaute == id;
            List<Communaute> communaute = comm.getCom(myFunc, false, "az");
            List<Recette> recette = rec.getRec(null, false, "az");

            rc.Communaute = communaute;
            rc.Recette = recette;
            return View(rc);
        }

        [AllowAnonymous]
        public ActionResult AfficherMore(string OptionRecette, string display)
        {
            CommunauteBLL coom = new CommunauteBLL();
            List<Communaute> list  = coom.getCom(null, false, OptionRecette, int.Parse(display));
            return PartialView("_Communaute", list);
        }
    }
}
