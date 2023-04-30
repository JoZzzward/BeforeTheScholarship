namespace BeforeTheScholarship.Tests.Integration.Base.Data
{
    public static class StudentConsts
    {
        public static Guid Id = Guid.NewGuid();
        public const string Email = "testuseremail1@test.com";
        public const string UserName = "testusername1";
        public static string Password = "aaaa1111";
        public static string NewPassword = "aaaa2222";

        public static Guid SecondId = Guid.NewGuid();
        public const string SecondEmail = "testuseremail2@test.com";
        public const string SecondUserName = "testusername2";
        public static string SecondPassword = "aaaa1111";
        public static string SecondNewPassword = "aaaa2222";
    }
}
