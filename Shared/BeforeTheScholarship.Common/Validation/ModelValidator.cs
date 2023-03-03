using FluentValidation;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace BeforeTheScholarship.Common.Validation
{
    public class ModelValidator<T> : IModelValidator<T> where T : class
    {
        private readonly IValidator<T> _validator;

        public ModelValidator(
                IValidator<T> validator
            )
        {
            _validator = validator;
        }

        public void CheckValidation(T model)
        {
            var result = _validator.Validate(model);

            if (!result.IsValid)
                throw new ValidationException(result.Errors);
        }
    }
}
