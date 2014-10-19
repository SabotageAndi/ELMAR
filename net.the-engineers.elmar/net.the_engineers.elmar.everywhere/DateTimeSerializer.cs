using System;
using System.Globalization;

namespace net.the_engineers.elmar.everywhere
{
    public class DateTimeSerializer
    {
        private const string FormatDateTimeString = "yyyy-MM-ddTHH:mm:sszzz";
        private const string FormatDateString = "yyyy-MM-dd";
        private static readonly CultureInfo CultureInfo = CultureInfo.CurrentCulture;

        public static DateTime ParseDateTime(string text)
        {
            DateTime dateTimeOffset;
            if (DateTime.TryParseExact(text, FormatDateTimeString, CultureInfo, DateTimeStyles.None, out dateTimeOffset))
            {
                return dateTimeOffset;
            }

            if (DateTime.TryParseExact(text, FormatDateString, CultureInfo, DateTimeStyles.None, out dateTimeOffset))
            {
                return dateTimeOffset;
            }

            return dateTimeOffset;
        }

        public static DateTime? ParseDateTimeNullable(string text)
        {
            if (String.IsNullOrWhiteSpace(text))
                return null;

            DateTime dateTimeOffset;
            if (DateTime.TryParseExact(text, FormatDateTimeString, CultureInfo, DateTimeStyles.None, out dateTimeOffset))
            {
                return dateTimeOffset;
            }

            if (DateTime.TryParseExact(text, FormatDateString, CultureInfo, DateTimeStyles.None, out dateTimeOffset))
            {
                return dateTimeOffset;
            }

            return null;
        }

        public static DateTimeOffset ParseDateTimeOffset(string text)
        {
            DateTimeOffset dateTimeOffset;
            if (DateTimeOffset.TryParseExact(text, FormatDateTimeString, CultureInfo, DateTimeStyles.None, out dateTimeOffset))
            {
                return dateTimeOffset;
            }

            if (DateTimeOffset.TryParseExact(text, FormatDateString, CultureInfo, DateTimeStyles.None, out dateTimeOffset))
            {
                return dateTimeOffset;
            }

            return dateTimeOffset;
        }

        public static DateTimeOffset? ParseDateTimeOffsetNullable(string text)
        {
            if (String.IsNullOrWhiteSpace(text))
                return null;

            DateTimeOffset dateTimeOffset;
            if (DateTimeOffset.TryParseExact(text, FormatDateTimeString, CultureInfo, DateTimeStyles.None, out dateTimeOffset))
            {
                return dateTimeOffset;
            }

            if (DateTimeOffset.TryParseExact(text, FormatDateString, CultureInfo, DateTimeStyles.None, out dateTimeOffset))
            {
                return dateTimeOffset;
            }

            return null;
        }

        public static string ToString(DateTime dateTime)
        {
            return dateTime.ToString(FormatDateTimeString, CultureInfo);
        }

        public static string ToString(DateTime? dateTime)
        {
            if (dateTime == null)
                return String.Empty;

            return dateTime.Value.ToString(FormatDateTimeString, CultureInfo);
        }

        public static string ToString(DateTimeOffset dateTime)
        {
            return dateTime.ToString(FormatDateTimeString, CultureInfo);
        }

        public static string ToString(DateTimeOffset? dateTime)
        {
            if (dateTime == null)
                return String.Empty;

            return dateTime.Value.ToString(FormatDateTimeString, CultureInfo);
        }
    }
}