import type { OrderStatus } from '../store/orders/order-status.enum';
import type { PaymentMethod } from '../store/orders/payment-method.enum';
import type { EntityDto } from '@abp/ng.core';
import type { ProductDto } from '../products/models';

export interface CreateUpdateOrderDto {
  orderId?: string;
  applyDate?: string;
  status: OrderStatus;
  shippingName?: string;
  shippingFee?: number;
  discount?: number;
  total?: number;
  cusomerUserId?: string;
  customerName?: string;
  customerPhoneNumber?: string;
  customerEmail?: string;
  customerAddress?: string;
  description?: string;
  request?: string;
  paymentId: PaymentMethod;
}

export interface OrderDto {
  orderId?: string;
  applyDate?: string;
  status: OrderStatus;
  shippingName?: string;
  shippingFee: number;
  total: number;
  subtotal: number;
  discount: number;
  grandTotal: number;
  cusomerUserId?: string;
  customerName?: string;
  customerPhoneNumber?: string;
  customerEmail?: string;
  customerAddress?: string;
  description?: string;
  request?: string;
  paymentId: PaymentMethod;
  id?: string;
}

export interface OrderInlistDto extends EntityDto<string> {
  orderId?: string;
  applyDate?: string;
  status: OrderStatus;
  shippingName?: string;
  shippingFee: number;
  total: number;
  subtotal: number;
  discount: number;
  grandTotal: number;
  cusomerUserId?: string;
  customerName?: string;
  customerPhoneNumber?: string;
  customerEmail?: string;
  customerAddress?: string;
  description?: string;
  request?: string;
  paymentId: PaymentMethod;
}

export interface OrderItemDto {
  orderId?: string;
  productId?: string;
  productName?: string;
  productImage?: string;
  sku?: string;
  quantity: number;
  price: number;
  total: number;
  product: ProductDto;
  id?: string;
}
