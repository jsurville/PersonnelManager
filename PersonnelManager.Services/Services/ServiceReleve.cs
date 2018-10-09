using System.Collections.Generic;
using System.Linq;
using PersonnelManager.Business.Exceptions;
using PersonnelManager.Dal.Data;
using PersonnelManager.Dal.Entites;

namespace PersonnelManager.Business.Services
{
    public class ServiceReleve
    {
        private readonly IDataReleve dataReleve;
        private readonly IDataEmploye dataEmploye;

        public ServiceReleve(IDataReleve dataReleve, IDataEmploye dataEmploye)
        {
            this.dataReleve = dataReleve;
            this.dataEmploye = dataEmploye;
        }

        public IEnumerable<ReleveMensuel> GetListeRelevesMensuels(int idOuvrier)
        {
            return this.dataReleve.GetListeRelevesMensuels(idOuvrier).OrderBy(x => x.Periode.PremierJour);
        }

        public ReleveMensuel GetReleveMensuel(int idReleve)
        {
            return this.dataReleve.GetReleveMensuel(idReleve);
        }

        public void EnregistrerReleveMensuel(ReleveMensuel releveMensuel)
        {
            var ouvrier = this.dataEmploye.GetOuvrier(releveMensuel.IdOuvrier);

            bool check = true;
            while (check)
            {
                foreach (var item in releveMensuel.Jours)
                {
                    if (item.NombreHeures >= 13)
                    {
                        check = false;
                        throw new BusinessException("Vous avez dépassé la limite horraire journalière");
                    }
                   
                }
                break;
            }


            releveMensuel.NombreTotalHeures = releveMensuel.Jours.Sum(x => x.NombreHeures);
            releveMensuel.TauxHoraire = ouvrier.TauxHoraire;
                          

            this.dataReleve.EnregistrerReleveMensuel(releveMensuel);
        }
    }
}
