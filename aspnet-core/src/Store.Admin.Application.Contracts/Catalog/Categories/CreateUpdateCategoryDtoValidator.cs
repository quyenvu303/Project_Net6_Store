using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Store.Admin.Categories
{
    internal class CreateUpdateCategoryDtoValidator : AbstractValidator<CreateUpdateCategoryDto>
    {
        public CreateUpdateCategoryDtoValidator()
        {
            RuleFor(x => x.CategoryId).NotEmpty().WithMessage("Không được để trống");
            RuleFor(x => x.CategoryName).NotEmpty().WithMessage("Không được để trống");
        }
    }
}
