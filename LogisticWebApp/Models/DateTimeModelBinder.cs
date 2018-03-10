using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace Logistic.Web.Models
{
    public class DateTimeModelBinder : IModelBinder
    {
        private readonly IModelBinder baseBinder = new SimpleTypeModelBinder(typeof(DateTime?));

        public Task BindModelAsync(ModelBindingContext bindingContext)
        {
            var valueProviderResult = bindingContext.ValueProvider.GetValue(bindingContext.ModelName);
            if (valueProviderResult != ValueProviderResult.None)
            {
                bindingContext.ModelState.SetModelValue(bindingContext.ModelName, valueProviderResult);

                var valueAsString = valueProviderResult.FirstValue;


                DateTime dateTime;
                string format = "dd.MM.yyyy";

                if (valueAsString.Length >= 12 && valueAsString.Contains(":")) format = "dd.MM.yyyy HH:mm";

                if (DateTime.TryParseExact(valueAsString, format, CultureInfo.InvariantCulture, DateTimeStyles.None, out dateTime))
                
                bindingContext.Result = ModelBindingResult.Success(dateTime);
                else
                bindingContext.Result = ModelBindingResult.Success(null);

                return Task.CompletedTask;
            }

            return baseBinder.BindModelAsync(bindingContext);
        }
    }
}
