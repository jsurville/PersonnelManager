using System.Collections.Generic;
using PersonnelManager.Dal.Entites;

namespace PersonnelManager.Dal.Data
{
    public interface IDataEmploye
    {
        void EnregistrerOuvrier(Ouvrier ouvrier);

        void EnregistrerCadre(Cadre cadre);

        IEnumerable<Cadre> GetListeCadres();

        IEnumerable<Ouvrier> GetListeOuvriers();

        Ouvrier GetOuvrier(int idOuvrier);

        Cadre GetCadre(int idCadre);
    }
}
