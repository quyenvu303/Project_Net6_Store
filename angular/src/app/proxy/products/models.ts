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
  bestSellers?: boolean;
  trending?: boolean;
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
  bestSellers?: boolean;
  trending?: boolean;
  id?: string;
  categoryName?: string;
  warehouseName?: string;
  categorySlug?: string;
  categoryParentId?: string;
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
  bestSellers?: boolean;
  trending?: boolean;
  categoryName?: string;
  warehouseName?: string;
  categorySlug?: string;
  categoryParentId?: string;
}
