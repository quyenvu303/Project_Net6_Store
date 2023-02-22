using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Store.Admin.Blogs
{
    public class CreateUpdateBlogDtoValidator : AbstractValidator<CreateUpdateBlogDto>
    {
        public CreateUpdateBlogDtoValidator()
        {
            RuleFor(x => x.Title).NotEmpty().WithMessage("Không được để trống");
        }
    }
}
