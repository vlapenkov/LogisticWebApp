using System;
using System.Collections.Generic;
using System.Text;

namespace Logistic.Utils
{
    public static class DateTimeHelper
    {
        public static string ToShortDateFormat(this DateTime? helper)
        {
            return helper.HasValue ? ((DateTime)helper).ToShortDateString() : String.Empty;
        }

        public static string ValueForPaginator(this DateTime? element)
        {
            if (element.HasValue) return ((DateTime)element).ToString("dd.MM.yyyy");
            return String.Empty;
        }

        /// <summary>
        /// Проверка текущий день выходной
        /// </summary>
        /// <param name="startingDate"></param>
        /// <returns></returns>
        private static bool IsDayOff(DateTime startingDate)
        {
            return (startingDate.DayOfWeek == DayOfWeek.Saturday || startingDate.DayOfWeek == DayOfWeek.Sunday);
        }

    }
}
