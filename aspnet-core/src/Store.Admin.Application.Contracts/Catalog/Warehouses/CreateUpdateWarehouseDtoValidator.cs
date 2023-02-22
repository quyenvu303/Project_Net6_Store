using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Store.Admin.Warehouses
{
    internal class CreateUpdateWarehouseDtoValidator : AbstractValidator<CreateUpdateWarehouseDto>
    {
        public CreateUpdateWarehouseDtoValidator()
        {
            RuleFor(x => x.WarehouseId).NotEmpty().WithMessage("Không được để trống");
            RuleFor(x => x.Title).NotEmpty().WithMessage("Không được để trống");
        }
    }
}
