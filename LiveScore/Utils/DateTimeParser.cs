using LiveScore.Exceptions;
using System;
using System.Globalization;

namespace LiveScore.Utils
{
    public class DateTimeParser
    {
        private static string[] allowedFormats =
        {
            "yyyy-MM-ddTHH:mm:ss",
            "yyyy-dd-MMTHH:mm:ss"
        };

        public static DateTime ParseDate(string dateString)
        {
            var result = DateTime.Now;

            if (DateTime.TryParseExact(dateString, allowedFormats, CultureInfo.InvariantCulture, DateTimeStyles.None, out result))
            {
                return result;
            }

            throw new ClientRequestException(string.Format("Invalid date value: {0}", dateString));
        }
    }
}
