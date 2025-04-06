using System.Globalization;
using System.Web.Mvc;

namespace WebShop.Models
{
    public class DecimalModelBinder : DefaultModelBinder //: IModelBinder
    {
        public override object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            var valueProviderResult = bindingContext.ValueProvider.GetValue(bindingContext.ModelName);

            if (valueProviderResult == null || string.IsNullOrEmpty(valueProviderResult.AttemptedValue))
                return base.BindModel(controllerContext, bindingContext);

            return decimal.TryParse(valueProviderResult.AttemptedValue, NumberStyles.Any, CultureInfo.InvariantCulture, out var result)
                ? (decimal?)result 
                : base.BindModel(controllerContext, bindingContext);
        }
    }
}