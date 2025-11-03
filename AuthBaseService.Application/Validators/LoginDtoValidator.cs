using AuthBaseService.Application.DTOs;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthBaseService.Application.Validators
{
    public  class LoginDtoValidator :AbstractValidator<LoginDto>
    {
        public LoginDtoValidator()
        {
            RuleFor(x => x.Email)
          .NotEmpty().WithMessage("ایمیل الزامی است.")
          .EmailAddress().WithMessage("فرمت ایمیل معتبر نیست.");

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("رمز عبور الزامی است.");
        }
    }
}
