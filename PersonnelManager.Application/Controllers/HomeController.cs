using System;
using System.Web.Mvc;
using PersonnelManager.Business.Services;
using PersonnelManager.Dal.Data;
using PersonnelManager.Filters;
using PersonnelManager.Models;

namespace PersonnelManager.Controllers
{
    public class HomeController : Controller
    {
        private const string SessionKey_Utilisateur = "Utilisateur";
        private readonly ServiceEmploye serviceEmploye;

        public HomeController()
        {
            this.serviceEmploye = new ServiceEmploye(new DbDataEmploye());
        }

        [Authentication]
        public ActionResult Index()
        {
            var utilisateur = (Utilisateur)this.Session[SessionKey_Utilisateur];
            return this.View();
        }

        public ActionResult Login()
        {
            return this.View(new LoginViewModel());
        }

        [HttpPost]
        public ActionResult Login(LoginViewModel viewModel)
        {
            if (this.ModelState.IsValid)
            {
                if (viewModel.Nom.Equals("Manager", StringComparison.OrdinalIgnoreCase)
                    && viewModel.MotDePasse.Equals("Manager"))
                {
                    this.Session[SessionKey_Utilisateur] = new Utilisateur(TypeUtilisateur.Manager);
                }
                else
                {
                    var ouvrier = this.serviceEmploye.GetOuvrier(viewModel.Nom);
                    if (ouvrier != null)
                    {
                        this.Session[SessionKey_Utilisateur] = new Utilisateur(TypeUtilisateur.Ouvrier)
                        {
                            IdOuvrier = ouvrier.Id
                        };
                    }
                    else
                    {
                        this.ModelState.AddModelError(string.Empty, "Utilisateur invalide");
                        return this.View(viewModel);
                    }
                }

                return this.RedirectToAction("Index", "Home");
            }

            return this.View(viewModel);
        }

        [Authentication]
        public ActionResult Logoff()
        {
            this.Session[SessionKey_Utilisateur] = null;
            return this.RedirectToAction("Index", "Home");
        }
    }
}