using System.Linq;
using System.Web.Mvc;
using PersonnelManager.Business.Exceptions;
using PersonnelManager.Business.Services;
using PersonnelManager.Dal.Data;
using PersonnelManager.Dal.Entites;
using PersonnelManager.Filters;
using PersonnelManager.Models;

namespace PersonnelManager.Controllers
{
    [Authentication(Type = TypeUtilisateur.Ouvrier)]
    public class RelevesController : Controller
    {
        private readonly ServiceReleve serviceReleve;
        private readonly ServicePeriode servicePeriode;

        public RelevesController()
        {
            this.serviceReleve = new ServiceReleve(new DbDataReleve(), new DbDataEmploye());
            this.servicePeriode = new ServicePeriode(new DbDataPeriode());
        }

        public ActionResult Index()
        {
            var idOuvrier = this.GetIdOuvrier();
            var releves = this.serviceReleve.GetListeRelevesMensuels(idOuvrier);
            var idPeriodes = releves.Select(x => x.IdPeriode).ToArray();
            var periodesOuvertes = this.servicePeriode.GetListePeriodes()
                .Where(x => !idPeriodes.Contains(x.Id) && !x.EstFermee);

            var viewModel = new ListeRelevesViewModel
            {
                IdOuvrier = idOuvrier,
                RelevesMensuels = releves,
                PeriodesOuvertes = periodesOuvertes
            };
            return this.View("ListeReleves", viewModel);
        }

        public ActionResult Nouveau(int idPeriode, int idOuvrier)
        {
            var periode = this.servicePeriode.GetPeriode(idPeriode);

            var viewModel = new EditionReleveMensuelViewModel
            {
                IdPeriode = idPeriode,
                IdOuvrier = idOuvrier,
                Periode = periode
            };

            foreach (var jour in this.servicePeriode.GetListeJoursTravailles(periode.PremierJour, periode.DernierJour))
            {
                viewModel.Jours.Add(new EditionReleveJourViewModel
                {
                    Date = jour,
                    NombreHeures = 7
                });
            }

            return this.View("EditionReleveMensuel", viewModel);
        }

        [HttpPost]
        public ActionResult Nouveau(EditionReleveMensuelViewModel viewModel)
        {
            if (this.ModelState.IsValid)
            {
                var releveMensuel = new ReleveMensuel
                {
                    IdOuvrier = viewModel.IdOuvrier,
                    IdPeriode = viewModel.IdPeriode,
                };

                foreach (var releveJour in viewModel.Jours)
                {
                    releveMensuel.Jours.Add(new ReleveJour
                    {
                        Jour = releveJour.Date,
                        NombreHeures = releveJour.NombreHeures.Value
                    });
                }

                try
                {
                    this.serviceReleve.EnregistrerReleveMensuel(releveMensuel);
                    return this.RedirectToAction("Index", "Releves");
                }
                catch (BusinessException exception)
                {
                    this.ModelState.AddModelError(string.Empty, exception.Message);
                }
            }

            viewModel.Periode = this.servicePeriode.GetPeriode(viewModel.IdPeriode);
            return this.View("EditionReleveMensuel", viewModel);
        }

        public ActionResult Saisie(int idReleve)
        {
            var releve = this.serviceReleve.GetReleveMensuel(idReleve);

            var viewModel = new EditionReleveMensuelViewModel
            {
                Id = releve.Id,
                IdPeriode = releve.IdPeriode,
                IdOuvrier = releve.IdOuvrier,
                Periode = releve.Periode
            };

            foreach (var releveJour in releve.Jours)
            {
                viewModel.Jours.Add(new EditionReleveJourViewModel
                {
                    Id = releveJour.Id,
                    Date = releveJour.Jour,
                    NombreHeures = releveJour.NombreHeures,
                    JourNonTravaille = releveJour.NombreHeures == 0
                });
            }

            return this.View("EditionReleveMensuel", viewModel);
        }

        [HttpPost]
        public ActionResult Saisie(EditionReleveMensuelViewModel viewModel)
        {
            if (this.ModelState.IsValid)
            {
                var releveMensuel = this.serviceReleve.GetReleveMensuel(viewModel.Id);

                foreach (var jour in viewModel.Jours)
                {
                    var releveJour = releveMensuel.Jours.Single(x => x.Jour == jour.Date);
                    releveJour.NombreHeures = jour.NombreHeures.Value;
                }

                try
                {
                    this.serviceReleve.EnregistrerReleveMensuel(releveMensuel);
                    return this.RedirectToAction("Index", "Releves");
                }
                catch (BusinessException exception)
                {
                    this.ModelState.AddModelError(string.Empty, exception.Message);
                }
            }

            viewModel.Periode = this.servicePeriode.GetPeriode(viewModel.IdPeriode);
            return this.View("EditionReleveMensuel", viewModel);
        }

        private int GetIdOuvrier()
        {
            return ((Utilisateur)this.Session["Utilisateur"]).IdOuvrier.Value;
        }
    }
}