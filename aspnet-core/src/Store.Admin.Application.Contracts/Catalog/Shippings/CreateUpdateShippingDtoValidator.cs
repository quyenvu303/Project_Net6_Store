using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Store.Admin.Shippings
{
    internal class CreateUpdateShippingDtoValidator : AbstractValidator<CreateUpdateShippingDto>
    {
        public CreateUpdateShippingDtoValidator()
        {
            RuleFor(x => x.ShippingName).NotEmpty().WithMessage("Không được để trống");
            RuleFor(x => x.ShippingFee).NotEmpty().WithMessage("Không được để trống");
        }
    }
}
