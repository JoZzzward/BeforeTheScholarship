namespace BeforeTheScholarship.Common.Validation
{
    public interface IModelValidator<T> where T : class
    {
        Task<bool> CheckValidation(T model);
    }
}