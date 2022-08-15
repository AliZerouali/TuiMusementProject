namespace Weather.Forecast.Application.Common.FormattingAndManipulation
{
    public static class Conversions
    {
        public static string ConvertDateTimeToString(DateTime date, int addingDates = 0)
        {
            return addingDates == 0 ? date.ToString("yyyy-MM-dd") :
                date.AddDays(addingDates).ToString("yyyy-MM-dd");
        }
    }
}
