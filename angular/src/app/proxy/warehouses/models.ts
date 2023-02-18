import type { EntityDto } from '@abp/ng.core';

export interface CreateUpdateWarehouseDto {
  warehouseId?: string;
  title?: string;
  status: boolean;
}

export interface WarehouseDto {
  warehouseId?: string;
  title?: string;
  status: boolean;
  id?: string;
}

export interface WarehouseInlistDto extends EntityDto<string> {
  warehouseId?: string;
  title?: string;
  status: boolean;
}
