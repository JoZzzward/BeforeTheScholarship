using System.Text.RegularExpressions;

namespace BeforeTheScholarship.Common.Extensions;

public static class StringExtensions
{

    public static string RemoveWhiteSpaces(this string str)
    {
        str = str.Trim();

        return Regex.Replace(str, @"\s+", string.Empty);
    }

    /// <summary>
    /// Split string into <paramref name="parts"/>.
    /// <para>Returns first part of the string</para>
    /// </summary>
    /// <param name="parts">What part of the string need to return</param>

    public static string Divide(this string str, int parts = 2)
    {
        str = str.Substring(0, str.Length / parts);

        return str;
    }
}
