using System.Text.RegularExpressions;

namespace BeforeTheScholarship.Common.Extensions;

public static class StringExtensions
{

    public static string RemoveWhiteSpaces(this string str)
    {
        str = str.Trim();

        return Regex.Replace(str, @"\s+", string.Empty);
    }
}
