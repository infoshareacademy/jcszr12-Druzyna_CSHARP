using FluentValidation;
using ProjectClock.BusinessLogic.Dtos.AccountDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectClock.BusinessLogic.Dtos.Validators
{
    public class RegisterDtoValidator  : AbstractValidator<RegisterDto>
    {
        public RegisterDtoValidator()
        {
            RuleFor(c => c.FirstName)
                .NotEmpty()
                .MinimumLength(2);

            RuleFor(c => c.LastName)
                .NotEmpty()
                .MinimumLength(2);

            RuleFor(c => c.Email)
                .NotEmpty()
                .EmailAddress()
                .WithMessage("Insert email");

            RuleFor(c => c.Password)
                        .NotEmpty()
                        .MinimumLength(8)
                        .WithMessage("Password must have more than 8 characters");

            RuleFor(c => c.ConfirmPassword)
                        .NotEmpty()
                        .Equal(e => e.Password)
                        .WithMessage("You passwords aren't equal");
                                            
        }
       
    }
}
