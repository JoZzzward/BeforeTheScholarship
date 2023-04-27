using BeforeTheScholarship.Common.Extensions;
using BeforeTheScholarship.Common.Helpers;

namespace BeforeTheScholarship.Services.UserAccountService.Helpers;

public static class ContentReader
{
    public static string ReadFromFile(string fileName, string userEmail, string token)
    {
        var content = PathReader.ReadContent(
                                Path.Combine(Directory.GetCurrentDirectory(), $"\\EmailPages\\{fileName}"),
                                $"/app/emailpages/{fileName}");

        content = content.Replace("QUERYEMAIL", userEmail)
                         .Replace("QUERYTOKEN", token)
                         .Replace("DATENOW", DateTimeOffset.UtcNow.ToShortStringFormat().ToString());

        return content;
    }
}
