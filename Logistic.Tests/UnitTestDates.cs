using Logistic.Web.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.Primitives;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Threading.Tasks;
using Xunit;

namespace Logistic.Tests
{
    public class UnitTestDates
    {
        private static DefaultModelBindingContext GetBindingContext(IValueProvider valueProvider, Type modelType)
        {
            var metadataProvider = new EmptyModelMetadataProvider();
            var bindingContext = new DefaultModelBindingContext
            {
                ModelMetadata = metadataProvider.GetMetadataForType(modelType),
                //    ModelName = modelType.Name,
                ModelName = "startDate",
                ModelState = new ModelStateDictionary(),
                ValueProvider = valueProvider,
            };
            return bindingContext;
        }

        [Fact]
        public void TestOnlyDate()
        { DateTime result;
            string valueAsString = "24.12.2018";


            DateTime.TryParseExact(valueAsString, "dd.MM.yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out result);



            Assert.Equal(24, result.Day);
        }

        [Fact]
        public void TestDateTime()
        {
            DateTime result;
            //string valueAsString = "24.12.2018 16:12";
            //string valueAsString = "24.12.2018 16:12";
            string valueAsString = "24.12.2018 01:03";

            string format = "dd.MM.yyyy";

            if (valueAsString.Length >= 12 && valueAsString.Contains(":")) format = "dd.MM.yyyy HH:mm";

            DateTime.TryParseExact(valueAsString, format, CultureInfo.InvariantCulture, DateTimeStyles.None, out result);

            Assert.Equal(24, result.Day);
            Assert.Equal(1, result.Hour);
            Assert.Equal(3, result.Minute);
        }

        [Fact]
        public async Task TestModelBinderDateTime()

        {

            
            var binder = new DateTimeModelBinder();

            var formCollection = new FormCollection(
                new Dictionary<string, StringValues>()
                {
            { "startDate", new StringValues("14.01.2008") },
            
                });
            var vp = new FormValueProvider(BindingSource.Form, formCollection, CultureInfo.CurrentCulture);

            var context = GetBindingContext(vp, typeof(DateTime?));

            await binder.BindModelAsync(context);

            var resultModel = context.Result.Model as DateTime?;

            Assert.NotNull(resultModel);
            Assert.True(((DateTime)resultModel).Day==14);
            //TODO asserts
        }


    }
    
}
