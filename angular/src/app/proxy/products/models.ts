import type { BaseListFilterDto } from '../models';
import type { EntityDto } from '@abp/ng.core';

export interface CreateUpdateProductDto {
  productId?: string;
  productName?: string;
  slug?: string;
  categoryId?: string;
  warehouseGuid?: string;
  origin?: string;
  imageName?: string;
  imageContent?: string;
  quantity?: number;
  price?: number;
  priceSale?: number;
  parameter?: string;
  description?: string;
  isActive?: boolean;
  status?: boolean;
}

export interface ProductDto {
  productId?: string;
  productName?: string;
  slug?: string;
  categoryId?: string;
  warehouseGuid?: string;
  origin?: string;
  image?: string;
  quantity?: number;
  price?: number;
  priceSale?: number;
  parameter?: string;
  description?: string;
  isActive?: boolean;
  status?: boolean;
  id?: string;
  categoryName?: string;
  warehouseName?: string;
}

export interface ProductFilter extends BaseListFilterDto {
  categoryId?: string;
  warehouseGuid?: string;
}

export interface ProductInlistDto extends EntityDto<string> {
  productId?: string;
  productName?: string;
  slug?: string;
  categoryId?: string;
  warehouseGuid?: string;
  origin?: string;
  image?: string;
  quantity?: number;
  price?: number;
  priceSale?: number;
  parameter?: string;
  description?: string;
  isActive?: boolean;
  status?: boolean;
  categoryName?: string;
  warehouseName?: string;
}
