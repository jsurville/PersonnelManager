using System.ComponentModel.DataAnnotations;

namespace PersonnelManager.Models
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Champ requis")]
        public string Nom { get; set; }

        [Required(ErrorMessage = "Champ requis")]
        public string MotDePasse { get; set; }
    }
}