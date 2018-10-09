using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using PersonnelManager.Business.Exceptions;
using PersonnelManager.Dal.Data;
using PersonnelManager.Dal.Entites;

namespace PersonnelManager.Business.Services
{
    public class ServiceEmploye
    {
        private readonly IDataEmploye dataEmploye;

        public ServiceEmploye(IDataEmploye dataEmploye)
        {
            this.dataEmploye = dataEmploye;
        }

        public Ouvrier GetOuvrier(string nom)
        {
            return this.dataEmploye.GetListeOuvriers().FirstOrDefault(x => x.Nom == nom);
        }

        public Ouvrier GetOuvrier(int idOuvrier)
        {
            return this.dataEmploye.GetOuvrier(idOuvrier);
        }

        public Cadre GetCadre(int idCadre)
        {
            return this.dataEmploye.GetCadre(idCadre);
        }
        public void EnregistrerCadre(Cadre cadre)
        {
            if(cadre == null)
            {
                throw new InvalidOperationException();
            }
            if (cadre.DateEmbauche.Year <= 1920)
            {
                throw new BusinessException("La date d'embauche doit être > 1920");
            }
            if (cadre.DateEmbauche > DateTime.Now.AddMonths(3))
            {
                throw new BusinessException("La date d'embauche ne peut être dans plus de 3 mois");
            }
            if (cadre.SalaireMensuel <= 0)
            {
                throw new BusinessException("Le salaire mensuel doit être positif");
            }

            Regex special = new Regex(@"[^a-zA-Zéàèïëüêâîôöç\s\-]");
            int BadCharCheckNom = special.Match(cadre.Nom).Length;
            int BadCharCheckPrenom = special.Match(cadre.Prenom).Length;
            if (BadCharCheckPrenom > 0 || BadCharCheckNom > 0)
            {
                throw new BusinessException("Les caractères spéciaux ne sont pas autorisés");
            }

            this.dataEmploye.EnregistrerCadre(cadre);
        }

        public void EnregistrerOuvrier(Ouvrier ouvrier)
        {
            if (ouvrier == null)
            {
                throw new InvalidOperationException();
            }

            
            if (ouvrier.TauxHoraire <= 0)
            {
                throw new BusinessException("Le taux horraire doit être positif");
            }
            if(ouvrier.DateEmbauche.Year <= 1920)
            {
                throw new BusinessException("La date d'embauche doit être > 1920");
            }

            if (ouvrier.DateEmbauche > DateTime.Now.AddMonths(3))
            {
                throw new BusinessException("La date d'embauche ne peut être dans plus de 3 mois");
            }
            Regex special = new Regex(@"[^a-zA-Zéàèïëüêâîôöç\s\-]");
            int BadCharCheckNom = special.Match(ouvrier.Nom).Length;
            int BadCharCheckPrenom = special.Match(ouvrier.Prenom).Length;
            if (BadCharCheckPrenom > 0 || BadCharCheckNom > 0)
            {
                throw new BusinessException("Les caractères spéciaux ne sont pas autorisés");
            }
            if (ouvrier.Nom.Length > 50)
            {
                throw new BusinessException("Le Nom ou Prénom est trop long");
            }

            this.dataEmploye.EnregistrerOuvrier(ouvrier);
        }

        public IEnumerable<Employe> GetListeEmployes()
        {
            var listeEmployes = new List<Employe>();
            listeEmployes.AddRange(this.dataEmploye.GetListeOuvriers());
            listeEmployes.AddRange(this.dataEmploye.GetListeCadres());

            return listeEmployes.OrderBy(x => x.Nom).ThenBy(x => x.Prenom);
        }

        public IEnumerable<SalaireOuvrier> GetSalaireOuvrier(int idOuvrier, DateTime mois)
        {
            return null;
        }

        public IEnumerable<Salaire> GetSalaireCadre(int idCadre, DateTime mois)
        {
            return null;
        }
    }
}
