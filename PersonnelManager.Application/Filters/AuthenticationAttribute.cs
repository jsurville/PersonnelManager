using System;
using System.Web.Mvc;
using PersonnelManager.Models;

namespace PersonnelManager.Filters
{
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class)]
    public class AuthenticationAttribute : ActionFilterAttribute
    {
        public string Type { get; set; } = null;

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var utilisateur = filterContext.HttpContext.Session["Utilisateur"] as Utilisateur;
            if (utilisateur == null || (this.Type != null && utilisateur.Type != this.Type))
            {
                filterContext.Result = new RedirectResult(@"/home/login");
            }
        }
    }
}