namespace BeforeTheScholarship.Web.Extensions
{
    public static class StringsExtensions
    {
        public static string CutBrackets(this string value) 
        {
            value = value.Replace('\"', ' ').Trim();

            return value;
        }
    }
}
