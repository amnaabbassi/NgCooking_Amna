using BLL;
using BLL.IngredientBLL;
using BLL.RecetteBLL;
using ngCooking.Models;
using NgCooking.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web.Mvc;

namespace ngCooking.Controllers
{
    public class IngredientsController : Controller
    {
        public IngredientsController()
        {
            ViewBag.PageActuelle = "Ingredient";
            RecetteBLL receteee = new RecetteBLL();
            List<Recette> list = receteee.getRec(null, false, "za");
            var nbr = list.Count();
            ViewBag.NombreDeRecettes = nbr;
        }

        // GET: Ingredients
        [AllowAnonymous]
        public ActionResult Index(string name, string categorie, string calorie1, string calorie2)
        {
            IngredientCategorie listingcateg = new IngredientCategorie();
            IngredientBLL ingbll = new IngredientBLL();
            List<Ingredient> listing = new List<Ingredient>();
            CategorieBLL icatbll = new CategorieBLL();
            List<Categorie> listcat = new List<Categorie>();
            int valeur1 = 0, valeur2 = 0;

            if (string.IsNullOrWhiteSpace(name) && string.IsNullOrWhiteSpace(categorie) && string.IsNullOrWhiteSpace(calorie1) && string.IsNullOrWhiteSpace(calorie2))
            {

                listing = ingbll.getIng(null, false).ToList();
                listcat = icatbll.getCat(null, false).ToList();
                listingcateg.Ingredient = listing;
                listingcateg.Categorie = listcat;
            }
            else if (!string.IsNullOrWhiteSpace(name) && !string.IsNullOrWhiteSpace(categorie) && !string.IsNullOrWhiteSpace(calorie1) && !string.IsNullOrWhiteSpace(calorie2))
            {
                valeur1 = Int32.Parse(calorie1);
                valeur2 = Int32.Parse(calorie2);
                Expression<System.Func<NgCooking.DAL.Entities.Ingredient, bool>> myFunc = x => x.Name.Contains(name) && x.calories > valeur1 && x.calories < valeur2 && x.Categorie.nameToDisplay == categorie;
                listing = ingbll.getIng(myFunc, false).ToList();
                listingcateg.Ingredient = listing;
                listingcateg.Categorie = listcat;
            }
            else
            {

                listing = ingbll.getIng(null, false).ToList();
                listcat = icatbll.getCat(null, false).ToList();
                listingcateg.Ingredient = listing;
                listingcateg.Categorie = listcat;

            }

            return View(listingcateg);
        }
    }
}
