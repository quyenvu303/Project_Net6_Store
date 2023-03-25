using Azure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Store.Public.Orders;
using Store.Public.Products;
using Store.Public.Web.Extensions;
using Store.Public.Web.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Caching;

namespace Store.Public.Web.Pages.Profile
{
    public class ViewOrdersModel : PageModel
    {
        public List<OrderItemDto> OrderItems { get; set; }
        public OrderDto OrderData { set; get; }
        public bool? CreateStatus { set; get; }

        private readonly IOrdersAppService _ordersAppService;

        public ViewOrdersModel(IOrdersAppService ordersAppService)
        {
            _ordersAppService = ordersAppService;
        }
        public async Task OnGetAsync(string Id)
        {
            Guid? currentUserId = User.Identity.IsAuthenticated ? User.GetUserId() : null;
           
            OrderItems = await _ordersAppService.GetOrderItemByIdAsync(Id);

            OrderData = await _ordersAppService.GetOrderByIdAsync(Id);
        }
        public async Task OnPostCancelItemAsync(Guid Id)
        {
            try
            {
                var order = await _ordersAppService.UpdateCancelItemAsync(Id);
                if (order != null)
                {
                    CreateStatus = true;
                    await OnGetAsync(Id.ToString());
                }
            }
            catch (Exception ex)
            {
                CreateStatus = false;
                throw;
            }
        }

        public async Task OnPostRequestCancelAsync(Guid Id)
        {
            try
            {
                var order = await _ordersAppService.UpdateRequestCancelAsync(Id);
                if (order != null)
                {
                    CreateStatus = true;
                    await OnGetAsync(Id.ToString());
                }
            }
            catch (Exception ex)
            {
                CreateStatus = false;
                throw;
            }
        }
    }
}
