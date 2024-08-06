using BaseProject.Domain.DTOs.AppUserDtos;
using FluentValidation;

namespace BaseProject.Application.Validators
{
    public class CreateUserDtoValidator : AbstractValidator<Create_User_Dto>
    {
        public CreateUserDtoValidator()
        {
            RuleFor(x => x.Email).NotEmpty().EmailAddress();
            RuleFor(x => x.Password).NotEmpty().MinimumLength(6);
        }
    }
}
