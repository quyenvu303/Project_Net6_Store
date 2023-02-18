import type { EntityDto } from '@abp/ng.core';

export interface CreateUpdateShippingDto {
  shippingName?: string;
  shippingFee?: number;
  description?: string;
  status?: boolean;
}

export interface ShippingDto {
  shippingName?: string;
  shippingFee?: number;
  description?: string;
  status?: boolean;
  id?: string;
}

export interface ShippingInlistDto extends EntityDto<string> {
  shippingName?: string;
  shippingFee?: number;
  description?: string;
  status?: boolean;
}
