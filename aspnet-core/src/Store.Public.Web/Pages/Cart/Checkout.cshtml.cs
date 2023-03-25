using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Polly.Caching;
using Store.Emailing;
using Store.Orders.Events;
using Store.Public.Orders;
using Store.Public.Products;
using Store.Public.Shippings;
using Store.Public.Web.Extensions;
using Store.Public.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text.Json;
using System.Threading.Tasks;
using Volo.Abp.EventBus.Local;
using Volo.Abp.TextTemplating;
using Volo.Abp.Users;
using static System.Collections.Specialized.BitVector32;

namespace Store.Public.Web.Pages.Cart
{
    public class CheckoutModel : PageModel
    {
        private readonly IOrdersAppService _ordersAppService;
        private readonly ILocalEventBus _localEventBus;
        private readonly IProductsAppService _productsAppService;
        private readonly IShippingAppService _shippingAppService;

        public CheckoutModel(IOrdersAppService ordersAppService,
            IProductsAppService productsAppService,
            IShippingAppService shippingAppService,
            ILocalEventBus localEventBus
            )
        {
            _ordersAppService = ordersAppService;
            _localEventBus = localEventBus;
            _shippingAppService = shippingAppService;
            _productsAppService = productsAppService;
        }
        public List<CartItem> CartItems { get; set; }

        public List<ShippingInlistDto> Shipping { get; set; }
        public bool? CreateStatus { set; get; }
        [BindProperty]
        public OrderDto Order { set; get; }
        public ShippingInlistDto ShippingFree { set; get; }

        private List<CartItem> GetCartItems()
        {
            var cart = HttpContext.Session.GetString(StoreConsts.Cart);
            var productCarts = new Dictionary<string, CartItem>();
            if (cart != null)
            {
                productCarts = JsonSerializer.Deserialize<Dictionary<string, CartItem>>(cart);
            }
            return productCarts.Values.ToList();
        }
        public async void OnGet(string id)
        {

            var cart = HttpContext.Session.GetString(StoreConsts.Cart);
            var productCarts = new Dictionary<string, CartItem>();
            if (cart != null)
            {
                productCarts = JsonSerializer.Deserialize<Dictionary<string, CartItem>>(cart);
            }
            var _Cart = productCarts.Values.ToList();



            if (_Cart.Count == 0)
            {
                if (cart != null)
                {
                    productCarts = JsonSerializer.Deserialize<Dictionary<string, CartItem>>(cart);
                }
                var product = await _productsAppService.GetAsync(Guid.Parse(id));
                if (cart == null)
                {
                    productCarts.Add(id, new CartItem()
                    {
                        Product = product,
                        Quantity = 1
                    });
                    HttpContext.Session.SetString(StoreConsts.Cart, JsonSerializer.Serialize(productCarts));
                }
                else
                {
                    productCarts = JsonSerializer.Deserialize<Dictionary<string, CartItem>>(cart);
                    if (productCarts.ContainsKey(id))
                    {
                        productCarts[id].Quantity += 1;
                    }
                    else
                    {
                        productCarts.Add(id, new CartItem()
                        {
                            Product = product,
                            Quantity = 1
                        });
                    }
                    HttpContext.Session.SetString(StoreConsts.Cart, JsonSerializer.Serialize(productCarts));
                }
                CartItems = productCarts.Values.ToList();
                //ShippingFree = await _shippingAppService.GetShipingFreeAsync();
                Shipping = await _shippingAppService.GetListAllAsync();
            }
            else if (_Cart.Count > 0)
            {
                CartItems = productCarts.Values.ToList();
                //ShippingFree = await _shippingAppService.GetShipingFreeAsync();
                Shipping = await _shippingAppService.GetListAllAsync();
            }
            else
            {

            }
        }
        public async Task OnPostAsync()
        {
            if (ModelState.IsValid == false)
            {
            }
            var cartItems = new List<OrderItemDto>();
            foreach (var item in GetCartItems())
            {
                var total = item.Product.Price * item.Quantity;
                cartItems.Add(new OrderItemDto()
                {

                    Price = item.Product.Price,
                    ProductId = item.Product.Id,
                    ProductName = item.Product.ProductName,
                    ProductImage = item.Product.Image,
                    Quantity = item.Quantity,
                    Total = total,
                }); ;
            }
            Guid? currentUserId = User.Identity.IsAuthenticated ? User.GetUserId() : null;
            var order = await _ordersAppService.CreateAsync(new CreateOrderDto()
            {
                Items = cartItems,
                CustomerUserId = currentUserId,
                CustomerName = Order.CustomerName,
                CustomerEmail = Order.CustomerEmail,
                CustomerAddress = Order.CustomerAddress,
                CustomerPhoneNumber = Order.CustomerPhoneNumber,
                Description = Order.Description,

            });
            CartItems = GetCartItems();

            if (order != null)
            {
                //if (User.Identity.IsAuthenticated)
                //{
                //    ////var email = User.GetSpecificClaim(ClaimTypes.Email);
                //    //var email = order.CustomerEmail;
                //    //await _localEventBus.PublishAsync(new NewOrderCreatedEvent()
                //    //{
                //    //    CustomerEmail = email,
                //    //    Message = "Tạo đơn hàng thành công. Vui lòng đợi hệ thống sửa lý"
                //    //});
                //    Message = "Tạo đơn hàng thành công. Vui lòng đợi hệ thống sửa lý"
                //}
                CreateStatus = true;
            }
            else
                CreateStatus = false;
        }

        [HttpPost]
        public ContentResult GetFree(Guid id)
        {
            // Do something here
            var result = new { shippingFee = 0 };
            var jsonResult = JsonSerializer.Serialize(result);
            return Content(jsonResult, "application/json");
        }

    }
}
