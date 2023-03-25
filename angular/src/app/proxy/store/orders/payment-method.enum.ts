import { mapEnumToOptions } from '@abp/ng.core';

export enum PaymentMethod {
  COD = 1,
  OnlinePayment = 2,
}

export const paymentMethodOptions = mapEnumToOptions(PaymentMethod);
