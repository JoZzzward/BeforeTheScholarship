using Duende.IdentityServer.Test;

namespace BeforeTheScholarship.IdentityServer.Configuration.Settings;

public static class AppTestUsers
{
    public static List<TestUser> TestUsers
        = new List<TestUser>()
        {
            new TestUser()
            {
                SubjectId = "1",
                Username = "john@tst.com",
                Password= "password123",
            },
            new TestUser()
            {
                SubjectId = "2",
                Username = "alice@tst.com",
                Password= "password123",
            }
        };
}
