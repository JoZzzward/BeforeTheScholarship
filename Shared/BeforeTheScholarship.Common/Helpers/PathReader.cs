namespace BeforeTheScholarship.Common.Helpers;

public static class PathReader
{
    /// <summary>
    /// <para>Read file content from spicified route. </para>
    /// <para>Returns content from local path if that exists, otherwise returns content from remote path.</para>
    /// <para>Returns empty string if content was not found.</para>
    /// </summary>
    /// <param name="localPath">Local path from current directory</param>
    /// <param name="productionPath"></param>
    public static string ReadContent(string localPath, string remotePath) =>
        File.Exists(localPath)
            ? File.ReadAllText(localPath)
            : File.Exists(remotePath) 
                ? File.ReadAllText(remotePath) 
                : string.Empty;
}
