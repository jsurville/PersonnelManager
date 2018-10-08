using System.Collections.Generic;
using PersonnelManager.Dal.Entites;

namespace PersonnelManager.Dal.Data
{
    public interface IDataPeriode
    {
        void EnregistrerPeriode(Periode periode);

        IEnumerable<Periode> GetListePeriodes();

        Periode GetPeriode(int idPeriode);
    }
}
