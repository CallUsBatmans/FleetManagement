using FluentValidation;
using TheFleet.Services.Domain;

namespace TheFleet.Services.Validators
{
    public class UserValidator: AbstractValidator<User>
    {
        public UserValidator()
        {
            RuleFor(x => x.Email)
                .EmailAddress()
                .WithMessage("Email is not valid")
                .NotNull()
                .WithMessage("Email can not be null")
                .NotEmpty()
                .WithMessage("Email can not be empty");
            RuleFor(x => x.Name)
                .NotNull().WithMessage("Name can not be null")
                .NotEmpty().WithMessage("Name can not be empty");
            RuleFor(x => x.Username)
                .NotNull().WithMessage("Username can not be null")
                .NotEmpty().WithMessage("Username can not be empty");
            RuleFor(x => x.Password)
                .Length(8, 255)
                .WithMessage("Minimum length for password is 8 characters")
                .NotNull()
                .WithMessage("Password can not be null")
                .NotEmpty()
                .WithMessage("Password can not be empty");
            RuleFor(x => x.ConfirmPassword)
                .Equal(x => x.Password)
                .WithMessage("Passwords do not match")
                .When(x => !string.IsNullOrWhiteSpace(x.Password))
                .NotNull()
                .WithMessage("Confirm Password can not be null")
                .NotEmpty()
                .WithMessage("Confirm Password can not be empty");
        }
    }
}
