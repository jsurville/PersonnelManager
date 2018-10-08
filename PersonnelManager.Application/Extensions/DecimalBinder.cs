using System;
using System.Globalization;
using System.Web.Mvc;

namespace PersonnelManager.Extensions
{
    public class DecimalModelBinder : DefaultModelBinder
    {
        public override object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            var valueProviderResult = bindingContext.ValueProvider.GetValue(bindingContext.ModelName);
            return valueProviderResult == null 
                ? base.BindModel(controllerContext, bindingContext) 
                : Convert.ToDecimal(valueProviderResult.AttemptedValue
                    .Replace(",", CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator)
                    .Replace(".", CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator));
        }
    }
}