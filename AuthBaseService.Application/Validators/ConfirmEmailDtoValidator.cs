using AuthBaseService.Application.DTOs;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthBaseService.Application.Validators
{
    public class ConfirmEmailDtoValidator : AbstractValidator<ConfirmEmailDto>
    {
        public ConfirmEmailDtoValidator()
        {
            RuleFor(x => x.Token)
                .NotEmpty().WithMessage("Token is required.");
        }
    }
}
