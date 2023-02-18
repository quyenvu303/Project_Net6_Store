using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Store.Admin.Products
{
    public class CreateUpdateProductDtoValidator : AbstractValidator<CreateUpdateProductDto>
    {
        public CreateUpdateProductDtoValidator()
        {
            RuleFor(x => x.ProductId).NotEmpty().WithMessage("Không được để trống");
            RuleFor(x => x.ProductName).NotEmpty().WithMessage("Không được để trống");
            RuleFor(x => x.CategoryId).NotEmpty().WithMessage("Không được để trống");
            RuleFor(x => x.WarehouseGuid).NotEmpty().WithMessage("Không được để trống");
            RuleFor(x => x.Origin).NotEmpty().WithMessage("Không được để trống");
            RuleFor(x => x.Price).NotEmpty().WithMessage("Không được để trống");
            RuleFor(x => x.Parameter).NotEmpty().WithMessage("Không được để trống");
        }
    }
}
