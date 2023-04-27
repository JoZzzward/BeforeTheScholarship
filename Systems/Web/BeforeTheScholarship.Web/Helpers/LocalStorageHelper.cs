namespace BeforeTheScholarship.Web.Helpers
{
    public class LocalStorageHelper
    {
        public static Guid GetStudentId(string studentId)
        {
            studentId = studentId.Replace('\"', ' ').Trim();
            Guid.TryParse(studentId, out var id);

            return id;
        }
    }
}
