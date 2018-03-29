using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Logistic.Web.Controllers
{
    public static class ControllerExtensions
    {
        /// <summary>
        /// Возврат ошибок валидации модели как Json
        /// </summary>
        /// <param name="controller"></param>
        /// <returns></returns>
        public static  IActionResult ReturnModelErrorsAsJson(this Controller controller)
        {
            var errorList = controller.ModelState
           .Where(x => x.Value.Errors.Count > 0)
           .ToDictionary(
               kvp => kvp.Key,
               kvp => kvp.Value.Errors.Select(e => e.ErrorMessage).ToArray()
           );
            return controller.Json(
           new
           { Success = false, Element = errorList.First().Key, Text = errorList.First().Value[0] });
        }
    }
}
