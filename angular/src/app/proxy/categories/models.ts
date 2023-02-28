import type { EntityDto } from '@abp/ng.core';

export interface CategoryDto {
  categoryId?: string;
  categoryName?: string;
  sortOrder?: number;
  description?: string;
  icon?: string;
  parentId?: string;
  isActive?: boolean;
  id?: string;
}

export interface CategoryInlistDto extends EntityDto<string> {
  categoryId?: string;
  categoryName?: string;
  sortOrder?: number;
  description?: string;
  icon?: string;
  parentId?: string;
  isActive?: boolean;
}

export interface CreateUpdateCategoryDto {
  categoryId?: string;
  categoryName?: string;
  sortOrder?: number;
  description?: string;
  iconName?: string;
  iconContent?: string;
  parentId?: string;
  isActive?: boolean;
}
