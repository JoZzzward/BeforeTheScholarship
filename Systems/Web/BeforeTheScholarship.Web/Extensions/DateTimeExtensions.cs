namespace BeforeTheScholarship.Web.Extensions;

public static class DateTimeExtensions
{
    /// <summary>
    /// Converts DateTime to string with format "dd/MM/yy"
    /// </summary>
    public static string ToShortStringFormat(this DateTime dateTime)
    {
        var formattedDateTime = dateTime.ToLocalTime().ToString("dd.MM.yy");

        return formattedDateTime;
    }

    /// <summary>
    /// Converts DateTimeOffset to string with format "dd/MM/yy"
    /// </summary>
    public static string ToShortStringFormat(this DateTimeOffset dateTime)
    {
        var formattedDateTime = dateTime.DateTime.ToShortStringFormat();

        return formattedDateTime;
    }
}
