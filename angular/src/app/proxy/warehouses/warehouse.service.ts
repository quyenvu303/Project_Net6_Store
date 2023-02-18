import type { CreateUpdateWarehouseDto, WarehouseDto, WarehouseInlistDto } from './models';
import { RestService } from '@abp/ng.core';
import type { PagedResultDto, PagedResultRequestDto } from '@abp/ng.core';
import { Injectable } from '@angular/core';
import type { BaseListFilterDto } from '../models';

@Injectable({
  providedIn: 'root',
})
export class WarehouseService {
  apiName = 'Default';
  

  create = (input: CreateUpdateWarehouseDto) =>
    this.restService.request<any, WarehouseDto>({
      method: 'POST',
      url: '/api/app/warehouse',
      body: input,
    },
    { apiName: this.apiName });
  

  delete = (id: string) =>
    this.restService.request<any, void>({
      method: 'DELETE',
      url: `/api/app/warehouse/${id}`,
    },
    { apiName: this.apiName });
  

  deleteMultile = (ids: string[]) =>
    this.restService.request<any, void>({
      method: 'DELETE',
      url: '/api/app/warehouse/multile',
      params: { ids },
    },
    { apiName: this.apiName });
  

  get = (id: string) =>
    this.restService.request<any, WarehouseDto>({
      method: 'GET',
      url: `/api/app/warehouse/${id}`,
    },
    { apiName: this.apiName });
  

  getList = (input: PagedResultRequestDto) =>
    this.restService.request<any, PagedResultDto<WarehouseDto>>({
      method: 'GET',
      url: '/api/app/warehouse',
      params: { skipCount: input.skipCount, maxResultCount: input.maxResultCount },
    },
    { apiName: this.apiName });
  

  getListAll = () =>
    this.restService.request<any, WarehouseInlistDto[]>({
      method: 'GET',
      url: '/api/app/warehouse/all',
    },
    { apiName: this.apiName });
  

  getListFilter = (input: BaseListFilterDto) =>
    this.restService.request<any, PagedResultDto<WarehouseInlistDto>>({
      method: 'GET',
      url: '/api/app/warehouse/filter',
      params: { keyword: input.keyword, status: input.status, skipCount: input.skipCount, maxResultCount: input.maxResultCount },
    },
    { apiName: this.apiName });
  

  update = (id: string, input: CreateUpdateWarehouseDto) =>
    this.restService.request<any, WarehouseDto>({
      method: 'PUT',
      url: `/api/app/warehouse/${id}`,
      body: input,
    },
    { apiName: this.apiName });

  constructor(private restService: RestService) {}
}
