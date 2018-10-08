using System;
using System.Web.Mvc;
using PersonnelManager.Business.Exceptions;
using PersonnelManager.Business.Services;
using PersonnelManager.Dal.Data;
using PersonnelManager.Filters;
using PersonnelManager.Models;

namespace PersonnelManager.Controllers
{
    [Authentication(Type = TypeUtilisateur.Manager)]
    public class PeriodesController : Controller
    {
        private readonly ServicePeriode servicePeriode;

        public PeriodesController()
        {
            this.servicePeriode = new ServicePeriode(new DbDataPeriode());
        }

        public ActionResult Index()
        {
            var periodes = this.servicePeriode.GetListePeriodes();
            return this.View("ListePeriodes", periodes);
        }

        public PartialViewResult GetNouvellePeriode()
        {
            return this.PartialView("NouvellePeriode");
        }

        [HttpPost]
        public ActionResult NouvellePeriode(DateTime date)
        {
            var resultat = new ResultatOperation();

            try
            {
                this.servicePeriode.OuvrirPeriode(date);
            }
            catch (BusinessException exception)
            {
                resultat.EstReussi = false;
                resultat.MessageErreur = exception.Message;
            }

            return this.Json(resultat);
        }

        [HttpPost]
        public ActionResult FermerPeriode(int idPeriode)
        {
            this.servicePeriode.FermerPeriode(idPeriode);

            return this.Json(true);
        }
    }

    public class ResultatOperation
    {
        public bool EstReussi { get; set; } = true;

        public string MessageErreur { get; set; }
    }
}