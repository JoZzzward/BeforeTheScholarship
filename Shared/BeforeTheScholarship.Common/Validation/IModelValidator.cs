namespace BeforeTheScholarship.Common.Validation
{
    public interface IModelValidator<T> where T : class
    {
        void CheckValidation(T model);
    }
}