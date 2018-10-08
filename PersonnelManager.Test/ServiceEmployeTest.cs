using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PersonnelManager.Business.Exceptions;
using PersonnelManager.Business.Services;
using PersonnelManager.Dal.Data;
using PersonnelManager.Dal.Entites;

namespace PersonnelManager.Test
{
    [TestClass]
    public class ServiceEmployeTest
    {
        [TestMethod]
        public void ValiderLongueurNomPrenom()
        {

            ServiceEmploye serviceEmploye = new ServiceEmploye(new fauxDataEmploye());
            string text = new string('x', 60);
            var ouvrier = new Ouvrier
            {
                Nom = text,
                Prenom = text,
                DateEmbauche = new DateTime(2018, 01, 01),
                TauxHoraire = 7
            };

            var exception = Assert.ThrowsException<BusinessException>(() =>
            {
                serviceEmploye.EnregistrerOuvrier(ouvrier);
            });
            Assert.AreEqual("Le Nom ou Prénom est trop long", exception.Message);
        }

        [TestMethod]
        public void ValiderDateEmbauche()
        {
            Assert.Fail();
        }


        [TestMethod]
        public void ValiderNomEtPrenomRequis()
        {
            Assert.Fail();
        }

        [TestMethod]
        public void DateEmbaucheOuvrierPosterieureA1920()
        {
            ServiceEmploye serviceEmploye = new ServiceEmploye(new fauxDataEmploye());
            //var fauxDataEmploye = new Mock<IDataEmploye>();

            //fauxDataEmploye.Setup(x => x.EnregistrerOuvrier(It.IsAny<Ouvrier>()));

           // var serviceEmploye = new ServiceEmploye(fauxDataEmploye.Object);
            var ouvrier = new Ouvrier
            {
                Nom = "Dupont",
                Prenom = "Gérard",
                DateEmbauche = new DateTime(1920, 12, 31),
                TauxHoraire = 12
            };

            var exception = Assert.ThrowsException<BusinessException>(() =>
            {
                serviceEmploye.EnregistrerOuvrier(ouvrier);
            });

            Assert.AreEqual("La date d'embauche doit être > 1920",
                exception.Message);
        }

        [TestMethod]
        public void DateEmbaucheCadrePosterieureA1920()
        {
            // var fauxDataEmploye = new Mock<IDataEmploye>();

            //fauxDataEmploye.Setup(x => x.EnregistrerCadre(It.IsAny<Cadre>()));
            ServiceEmploye serviceEmploye = new ServiceEmploye(new fauxDataEmploye());
            //var serviceEmploye = new ServiceEmploye(fauxDataEmploye.Object);
            var cadre = new Cadre
            {
                Nom = "Dupont",
                Prenom = "Gérard",
                DateEmbauche = new DateTime(1920, 12, 31),
                SalaireMensuel = 1500
            };

            var exception = Assert.ThrowsException<BusinessException>(() =>
            {
                serviceEmploye.EnregistrerCadre(cadre);
            });

            Assert.AreEqual("La date d'embauche doit être > 1920",
                exception.Message);
        }

        [TestMethod]
        public void DateEmbaucheCadreAnterieureAujourdhuiPlus3Mois()
        {
            Assert.Fail();
        }

        [TestMethod]
        public void DateEmbaucheOuvrierAnterieureAujourdhuiPlus3Mois()
        {
            Assert.Fail();
        }

        [TestMethod]
        public void SalaireCadrePositif()
        {
            Assert.Fail();
        }

        [TestMethod]
        public void TauxHoraireOuvrierPositif()
        {
            Assert.Fail();
        }

        [TestMethod]
        public void InterdireCaracteresSpeciauxDansNomEtPrenomCadre()
        {
            Assert.Fail();
        }

        [TestMethod]
        public void InterdireCaracteresSpeciauxDansNomEtPrenomOuvrier()
        {
            Assert.Fail();
        }

        [TestMethod]
        public void OuvrierEstNonNull()
        {
            var fauxDataEmploye = new Mock<IDataEmploye>();
            var serviceEmploye = new ServiceEmploye(fauxDataEmploye.Object);
            Assert.ThrowsException<InvalidOperationException>(
                () => serviceEmploye.EnregistrerOuvrier(null));
        }

        [TestMethod]
        public void CadreEstNonNull()
        {
            var fauxDataEmploye = new Mock<IDataEmploye>();
            var serviceEmploye = new ServiceEmploye(fauxDataEmploye.Object);
            Assert.ThrowsException<InvalidOperationException>(
                () => serviceEmploye.EnregistrerCadre(null));
        }
    }


}
