namespace BeforeTheScholarship.EmailWorker.EmailTask;

public interface ITaskEmailSender
{
    Task Start(bool isDevelopment);
}