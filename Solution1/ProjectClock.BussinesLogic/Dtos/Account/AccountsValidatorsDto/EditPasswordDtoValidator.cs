using FluentValidation;
using ProjectClock.BusinessLogic.Dtos.AccountDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectClock.BusinessLogic.Dtos.AccountsValidatorsDto
{
    public class EditPasswordDtoValidator : AbstractValidator<EditPasswordDto>
    {
        public EditPasswordDtoValidator()
        {
            RuleFor(c => c.CurrentPassword)
                      .NotEmpty()
                      .MinimumLength(8).WithMessage("Password must have more than 8 characters");
            RuleFor(c => c.NewPassword)
                       .NotEmpty()
                       .MinimumLength(8).WithMessage("Password must have more than 8 characters");
            RuleFor(c => c.ConfirmNewPassword)
                        .NotEmpty()
                        .Equal(e => e.NewPassword)
                        .WithMessage("You passwords aren't equal");
        }
    }
}
