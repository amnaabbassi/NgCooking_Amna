using BLL;
using BLL.CommentBLL;
using BLL.CommunauteBLL;
using BLL.IngredientBLL;
using BLL.RecetteBLL;
using BLL.RecetteIngredientBLL;
using ngCooking.Models;
using NgCooking.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Claims;
using System.Threading;
using System.Web.Mvc;

namespace ngCooking.Controllers
{
    public class RecettesController : Controller
    {
        public RecettesController()
        {
            ViewBag.PageActuelle = "Recettes";
            RecetteBLL receteee = new RecetteBLL();
            List<Recette> list = receteee.getRec(null, false, "za");
            var nbr = list.Count();
            ViewBag.NombreDeRecettes = nbr;
        }

        // GET: Recettes
        [AllowAnonymous]
        public ActionResult Index(string NameRecette, string Ingredient, string nbr1, string nbr2, string OptionRecette)
        {
            List<Recette> list = new List<Recette>();
            RecetteBLL receteee = new RecetteBLL();
            int valeur1 = 0, valeur2 = 0, id = 0;

            if (!string.IsNullOrWhiteSpace(NameRecette) && !string.IsNullOrWhiteSpace(Ingredient) && !string.IsNullOrWhiteSpace(nbr1) && !string.IsNullOrWhiteSpace(nbr2))
            {
                valeur1 = Int32.Parse(nbr1);
                valeur2 = Int32.Parse(nbr2);
                IngredientBLL ingbll = new IngredientBLL();
                RecetteIngredientBLL recetingbll = new RecetteIngredientBLL();

                Expression<Func<Recette, bool>> myFunc = x => x.name.Contains(NameRecette) && x.calories > valeur1 && x.calories < valeur2;
                Expression<Func<Ingredient, bool>> myFunc1 = x => x.Name == Ingredient;
                Expression<Func<RecetteIngredient, bool>> myFunc2 = x => x.IdIngredient == id;

                List<Ingredient> ing = ingbll.getIng(myFunc1, false);
                List<RecetteIngredient> recting = recetingbll.getCat(myFunc2, false);

                foreach (var item in ing)
                {
                    id = item.IdIngredient;
                }
                foreach (var item in recting)
                {
                    id = (int)item.IdIngredient;
                }
                if (recting.Count > 0)
                {
                    myFunc = x => x.name.Contains(NameRecette) && x.calories > valeur1 && x.calories < valeur2;
                }
                else
                {
                    ViewBag.Nom = "Une recette avec ce nom existe déjà !";
                }
                if (string.IsNullOrWhiteSpace(OptionRecette))
                {
                    list = receteee.getRec(myFunc, false, "az");
                }

                else if (!string.IsNullOrWhiteSpace(OptionRecette))
                {
                    list = receteee.getRec(myFunc, false, OptionRecette);
                }
            }
            else if (string.IsNullOrWhiteSpace(NameRecette) && string.IsNullOrWhiteSpace(Ingredient) && string.IsNullOrWhiteSpace(nbr1) && string.IsNullOrWhiteSpace(nbr2))
            {
                if (string.IsNullOrWhiteSpace(OptionRecette))
                {
                    list = receteee.getRec(null, false, "az");
                }
                else if (!string.IsNullOrWhiteSpace(OptionRecette))
                {
                    list = receteee.getRec(null, false, OptionRecette);
                }
            }
            return View(list);
        }

        // GET: Recettes/Details/5
        public ActionResult Details(int? idrecette, string comment, string mark, string title, int? idRecette)
        {
            //Get the current claims principal
            var identity = (ClaimsPrincipal)Thread.CurrentPrincipal;
            // Get the claims values
            var idcommunaute = identity.Claims.Where(c => c.Type == ClaimTypes.NameIdentifier)
                               .Select(c => c.Value).SingleOrDefault();
            int idcomm = int.Parse(idcommunaute);

            IngredientCategorieRecette listfinal = new IngredientCategorieRecette();
            CommentBLL commentlist = new CommentBLL();
            RecetteBLL recet = new RecetteBLL();
            CommunauteBLL communauteList = new CommunauteBLL();
            IngredientBLL ingbll = new IngredientBLL();

            Expression<Func<Recette, bool>> myFunc = x => x.idRecette == idrecette;
            Expression<Func<Comment, bool>> myFuncComm = y => y.idRecette == idrecette && y.idcommunaute == idcomm;
            Expression<Func<Communaute, bool>> myFuncCommunaute = z => z.idcommunaute == idcomm;

            List<Comment> listcat = commentlist.getComm(myFuncComm, false);
            List<Recette> recette = recet.getRec(myFunc, false, "za");
            List<Communaute> communaute = communauteList.getCom(myFuncCommunaute, false, "az");
            List<Ingredient> listing = new List<Ingredient>();

            if (!string.IsNullOrWhiteSpace(comment) && !string.IsNullOrWhiteSpace(mark) && !string.IsNullOrWhiteSpace(title))
            {
                Comment newcomment = new Comment();
                newcomment.title = title;
                newcomment.comment1 = comment;
                newcomment.mark = int.Parse(mark);
                newcomment.idcommunaute = int.Parse(idcommunaute);
                newcomment.idRecette = idrecette;
                commentlist.addComm(newcomment);
                return RedirectToAction("Details", "Recettes", new { id = recette.FirstOrDefault().idRecette });
            }

            listing = ingbll.getIng(null, false);
            listfinal.Recette = recette;
            listfinal.Comment = listcat;
            listfinal.Communaute = communaute;
            listfinal.Ingredient = listing;

            return View(listfinal);
        }

        // Post: Recettes/Create
        public ActionResult Create(string NomRecetteAdd, string categorieOption, string PreparationRecette, string totalCalories, string recetteImage, string IngredientName, string categorie)
        {

            //Get the current claims principal
            var identity = (ClaimsPrincipal)Thread.CurrentPrincipal;
            //Get the claims values
            var idcommunaute = identity.Claims.Where(c => c.Type == ClaimTypes.NameIdentifier).Select(c => c.Value).SingleOrDefault();
            int idcomm = int.Parse(idcommunaute);
            int? cal = 0, x = 0;

            CategorieBLL catelist = new CategorieBLL();
            IngredientBLL inglist = new IngredientBLL();
            IngredientCategorieRecette listing = new IngredientCategorieRecette();
            RecetteBLL recet = new RecetteBLL();

            Recette newrecette = new Recette();
            RecetteIngredient ri = new RecetteIngredient();
            RecetteIngredientBLL ribll = new RecetteIngredientBLL();
            CommunauteBLL commbll = new CommunauteBLL();

            Expression<Func<Communaute, bool>> myFuncCommunaute = z => z.idcommunaute == idcomm;

            List<Categorie> listcat;
            List<Ingredient> listingred;
            List<Communaute> commu = commbll.getCom(myFuncCommunaute, false, "za");
            List<Recette> listrecette;

            if (!string.IsNullOrWhiteSpace(NomRecetteAdd) && !string.IsNullOrWhiteSpace(categorieOption) && !string.IsNullOrWhiteSpace(PreparationRecette) && !string.IsNullOrWhiteSpace(recetteImage))
            {
                int iding = int.Parse(IngredientName);
                Expression<Func<Ingredient, bool>> myFunc = z => z.IdIngredient == iding;
                listingred = inglist.getIng(myFunc, false);
                cal = listingred.First().calories;
                ViewBag.Calorie = cal;
                int idingre = int.Parse(IngredientName);
                newrecette.name = NomRecetteAdd;
                newrecette.isAvailable = true;
                newrecette.preparation = PreparationRecette;
                newrecette.picture = recetteImage;
                newrecette.calories = (cal * 100) / 400;
                newrecette.idcommunaute = int.Parse(idcommunaute);
                recet.addRec(newrecette);
                listrecette = recet.getRec(null, false, "za");
                x = listrecette.OrderBy(z => z.idRecette).Last().idRecette;
                ri.idRecette = x;
                ri.IdIngredient = idingre;
                ribll.addCat(ri);

                ViewData["Nom1"] = "Votre recette a bien été ajoutée !";
                listcat = catelist.getCat(null, false);
                listingred = inglist.getIng(null, false);
                listing.Categorie = listcat;
                listing.Ingredient = listingred;
                listing.Communaute = commu;

            }
            else
            {
                ViewData["Nom"] = "Woops ! Votre recette est super foireuse";
                listcat = catelist.getCat(null, false);
                listingred = inglist.getIng(null, false);
                listing.Categorie = listcat;
                listing.Ingredient = listingred;
                listing.Communaute = commu;


            }
            return View(listing);
        }

        [Authorize]
        public ActionResult AfficherIngredients(string categorie)
        {
            IngredientCategorieRecette listing = new IngredientCategorieRecette();

            int CategorieId = 0;
            CategorieBLL categorieBLL = new CategorieBLL();
            Expression<Func<Categorie, bool>> myFuncCategorie = z => z.nameToDisplay == categorie;
            List<Categorie> listecategorie = categorieBLL.getCat(myFuncCategorie, false);

            foreach (var item in listecategorie)
            {
                CategorieId = item.IdCategorie;
            }

            Expression<Func<Ingredient, bool>> myFuncingredient = z => z.IdCategorie == CategorieId;
            IngredientBLL ingredienBLL = new IngredientBLL();
            List<Ingredient> listeIngredient = ingredienBLL.getIng(myFuncingredient, false);

            listing.Ingredient = listeIngredient;
            listing.Categorie = listecategorie;
            return PartialView("_Affiche_Ingredient", listing);
        }

        [Authorize]
        public ActionResult AjouterIngredientListe(string ingredient)
        {
            IngredientBLL ingredienBLL = new IngredientBLL();
            int IngredientId = int.Parse(ingredient);
            Expression<Func<Ingredient, bool>> myFuncIngredient = z => z.IdIngredient == IngredientId;
            List<Ingredient> listeIngredient = ingredienBLL.getIng(myFuncIngredient, false);
            IngredientCategorieRecette listing = new IngredientCategorieRecette();
            listing.Ingredient = listeIngredient;
            return PartialView("_AfficherListeIngredientsRecette", listing);
        }

    }
}
