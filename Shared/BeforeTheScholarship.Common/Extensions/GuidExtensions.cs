namespace BeforeTheScholarship.Common.Extensions
{
    public static class GuidExtensions
    {
        public static string Shrink(this Guid guid)
            => guid.ToString().Replace("-", "");
    }
}
