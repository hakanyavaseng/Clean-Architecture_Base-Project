using BaseProject.Domain.Entities;
using FluentValidation;
using FluentValidation.Results;

namespace BaseProject.Application.Validators
{
    public class AppUserValidator : AbstractValidator<AppUser>
    {
        public AppUserValidator()
        {
            RuleFor(p=> p.UserName).NotEmpty();
            RuleFor(p=> p.Email).NotEmpty().EmailAddress();

        }

    }
}
