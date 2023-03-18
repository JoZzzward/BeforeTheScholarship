using BeforeTheScholarship.Common.Helpers;
using BeforeTheScholarship.Entities;

namespace BeforeTheScholarship.Services.DebtService.Helpers;

public static partial class ContentReader
{
    public static string ReadFromFile(string fileName, string studentUsername, Debts data)
    {
        var content = PathReader.ReadContent(
                                        Directory.GetCurrentDirectory() + $"\\EmailPages\\{fileName}",
                                        $"/app/emailpages/{fileName}")
                                        .Replace("DATETIMENOW", $"{DateTimeOffset.Now.DateTime.ToShortDateString()}")
                                        .Replace("STUDENTNAME", $"{studentUsername}")
                                        .Replace("BORROWED", $"{data.Borrowed}")
                                        .Replace("WHENTOPAYBACK", $"{data.WhenToPayback.DateTime.ToShortDateString()}");
        return content;
    }
}
