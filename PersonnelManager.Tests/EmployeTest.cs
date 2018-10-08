using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PersonnelManager.Business.Exceptions;
using PersonnelManager.Business.Services;
using PersonnelManager.Dal.Data;
using PersonnelManager.Dal.Entites;

namespace PersonnelManager.Tests
{
    [TestClass]
    public class EmployeTest
    {
        [TestMethod]
        public void ValiderStringLength()
        {
            string text = new string('x', 20);
            DateTime dateEmbauche = Convert.ToDateTime("02/02/2018");

            ServiceEmploye serviceEmploye = new ServiceEmploye(new DbDataEmploye());
                var employe = new Ouvrier
                {
                    Nom = text,
                    Prenom = text,
                    DateEmbauche = dateEmbauche,
                    TauxHoraire = 1
                };
            var exception = Assert.ThrowsException<BusinessException>(() =>
            {
                serviceEmploye.EnregistrerOuvrier(employe);
            });
                Assert.AreEqual("Le Nom est trop long (50 caractères max)", exception.Message);
        }
    }
}
