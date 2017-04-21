using BLL.CommunauteBLL;
using BLL.RecetteBLL;
using Microsoft.AspNet.Identity;
using NgCooking.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Security.Claims;
using System.Threading;
using System.Web;
using System.Web.Mvc;


namespace ngCooking.Controllers
{
    public class HomeController : Controller
    {
        public HomeController()
        {
            ViewBag.PageActuelle = "Home";

            RecetteBLL receteee = new RecetteBLL();
            List<Recette> list = receteee.getRec(null, false, "za");
            var nbr = list.Count;
            ViewBag.NombreDeRecettes = nbr;
        }

        [AllowAnonymous]
        public ActionResult Index()
        {
            RecetteBLL recbll = new RecetteBLL();
            List<Recette> rec = recbll.getRec(null, false, "za");
            return View(rec);
        }

        [AllowAnonymous]
        public ActionResult Login(string email, string password)
        {
            if (string.IsNullOrWhiteSpace(email) && string.IsNullOrWhiteSpace(password))
            {
                return View();
            }
            Communaute communaute = ValidateUser(email, password);
            if (communaute.firstname != null)
            {
                var userClaims = new List<Claim>();
                var name = communaute.firstname + " " + communaute.surname;
                string idComm = communaute.idcommunaute.ToString();
                userClaims.Add(new Claim(ClaimTypes.Name, name));
                userClaims.Add(new Claim(ClaimTypes.NameIdentifier, idComm));

                var claimsIdentity = new ClaimsIdentity(userClaims, DefaultAuthenticationTypes.ApplicationCookie);
                var claimPrincipal = new ClaimsPrincipal(claimsIdentity);
                var ctx = Request.GetOwinContext();
                var authenticationManager = ctx.Authentication;
                authenticationManager.SignIn(claimsIdentity);

                Thread.CurrentPrincipal = claimPrincipal;
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        private Communaute ValidateUser(string login, string password)
        {
            CommunauteBLL commBLL = new CommunauteBLL();
            Expression<Func<Communaute, bool>> myFunc = x => x.email == login && x.password == password;
            List<Communaute> comm = commBLL.getCom(myFunc, false, "az");
            if (comm.Count > 0)
            {
                return comm[0];
            }
            return comm[0];
        }

        [HttpGet]
        public ActionResult Logout()
        {
            var ctx = Request.GetOwinContext();
            var authenticationManager = ctx.Authentication;
            authenticationManager.SignOut();
            return RedirectToAction("Index", "Home");
        }

    }
}