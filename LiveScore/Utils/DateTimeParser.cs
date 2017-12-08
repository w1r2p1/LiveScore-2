using LiveScore.Exceptions;
using System;
using System.Globalization;

namespace LiveScore.Utils
{
    /// <summary>
    /// Date time utility class that parses string (ISO 8601 and variant) into <see cref="DateTime"/> object.
    /// </summary>
    public class DateTimeParser
    {
        private static string[] allowedFormats =
        {
            "yyyy-MM-ddTHH:mm:ss",
            "yyyy-dd-MMTHH:mm:ss"
        };

        /// <summary>
        /// This method parses ISO 8601 date string into <see cref="DateTime"/> object.
        /// </summary>
        /// <param name="dateString">Date string</param>
        /// <returns><see cref="DateTime"/></returns>
        public static DateTime ParseDate(string dateString)
        {
            var result = DateTime.Now;

            if (DateTime.TryParseExact(dateString, allowedFormats, CultureInfo.InvariantCulture, DateTimeStyles.None, out result))
            {
                return result;
            }

            throw new ClientRequestException(string.Format("Invalid date value: {0}", dateString));
        }

        /// <summary>
        /// This method prints <see cref="DateTime"/> object to ISO 8601 formated string.
        /// </summary>
        /// <param name="date">Input date time</param>
        /// <returns>ISO 8601 formated date string</returns>
        public static string PrintDate(DateTime date)
        {
            return date.ToString(allowedFormats[0]);
        }
    }
}
