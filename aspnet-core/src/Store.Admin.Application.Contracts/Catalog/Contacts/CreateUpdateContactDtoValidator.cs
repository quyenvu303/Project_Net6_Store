using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Store.Admin.Contacts
{
    public class CreateUpdateContactDtoValidator : AbstractValidator<CreateUpdateContactDto>
    {
        public CreateUpdateContactDtoValidator()
        {
            RuleFor(x => x.Title).NotEmpty().WithMessage("Không được để trống");
        }
    }
}
