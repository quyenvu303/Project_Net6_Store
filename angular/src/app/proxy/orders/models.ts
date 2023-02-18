import type { OrderStatus } from '../store/orders/order-status.enum';
import type { PaymentMethod } from '../store/orders/payment-method.enum';
import type { EntityDto } from '@abp/ng.core';

export interface CreateUpdateOrderDto {
  orderId?: string;
  appDate?: string;
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
  paymentId: PaymentMethod;
}

export interface OrderDto {
  orderId?: string;
  appDate?: string;
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
  paymentId: PaymentMethod;
  id?: string;
}

export interface OrderInlistDto extends EntityDto<string> {
  orderId?: string;
  appDate?: string;
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
  paymentId: PaymentMethod;
}
