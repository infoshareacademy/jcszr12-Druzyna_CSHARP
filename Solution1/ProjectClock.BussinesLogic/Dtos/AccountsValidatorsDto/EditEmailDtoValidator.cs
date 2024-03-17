using FluentValidation;
using ProjectClock.BusinessLogic.Dtos.AccountDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectClock.BusinessLogic.Dtos.AccountsValidatorsDto
{
    public class EditEmailDtoValidator : AbstractValidator<EditEmailDto>
    {
        public EditEmailDtoValidator()
        {
            RuleFor(c => c.CurrentEmail)
                .NotEmpty()
                .EmailAddress()
                .WithMessage("Insert email");
            RuleFor(c => c.NewEmail)
                .NotEmpty()
                .EmailAddress()
                .WithMessage("Insert email");
            RuleFor(c => c.NewEmailRepeat)
                .NotEmpty()
                .EmailAddress()
                .WithMessage("Insert email");
        }
    }
}
