using System.Text;
using System.Web.Mvc;

namespace PersonnelManager.Extensions
{
    public static class Validator
    {
        public static MvcHtmlString MyValidationSummary(this HtmlHelper helper)
        {
            if (helper.ViewData.ModelState.IsValid)
                return null;

            const string template = 
                @"<div class='alert alert-danger'>
                    <div class='alert-items'>
                        <div class='alert-item static'>
                            <div class='alert-icon-wrapper'>
                                <clr-icon class='alert-icon' shape='exclamation-circle'></clr-icon>
                            </div>
                            <span class='alert-text'>
                                {0}
                            </span>
                        </div>
                    </div>
                </div>";

            var errorBuilder = new StringBuilder();
            foreach (var key in helper.ViewData.ModelState.Keys)
            {
                if (string.IsNullOrEmpty(key))
                {
                    foreach (var err in helper.ViewData.ModelState[key].Errors)
                        errorBuilder.Append($"{helper.Encode(err.ErrorMessage)}<br/>");
                }
            }

            return MvcHtmlString.Create(string.Format(template, errorBuilder.ToString()));
        }
    }
}