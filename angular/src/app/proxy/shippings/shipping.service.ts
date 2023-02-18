import type { CreateUpdateShippingDto, ShippingDto, ShippingInlistDto } from './models';
import { RestService } from '@abp/ng.core';
import type { PagedResultDto, PagedResultRequestDto } from '@abp/ng.core';
import { Injectable } from '@angular/core';
import type { BaseListFilterDto } from '../models';

@Injectable({
  providedIn: 'root',
})
export class ShippingService {
  apiName = 'Default';
  

  create = (input: CreateUpdateShippingDto) =>
    this.restService.request<any, ShippingDto>({
      method: 'POST',
      url: '/api/app/shipping',
      body: input,
    },
    { apiName: this.apiName });
  

  delete = (id: string) =>
    this.restService.request<any, void>({
      method: 'DELETE',
      url: `/api/app/shipping/${id}`,
    },
    { apiName: this.apiName });
  

  deleteMultile = (ids: string[]) =>
    this.restService.request<any, void>({
      method: 'DELETE',
      url: '/api/app/shipping/multile',
      params: { ids },
    },
    { apiName: this.apiName });
  

  get = (id: string) =>
    this.restService.request<any, ShippingDto>({
      method: 'GET',
      url: `/api/app/shipping/${id}`,
    },
    { apiName: this.apiName });
  

  getList = (input: PagedResultRequestDto) =>
    this.restService.request<any, PagedResultDto<ShippingDto>>({
      method: 'GET',
      url: '/api/app/shipping',
      params: { skipCount: input.skipCount, maxResultCount: input.maxResultCount },
    },
    { apiName: this.apiName });
  

  getListAll = () =>
    this.restService.request<any, ShippingInlistDto[]>({
      method: 'GET',
      url: '/api/app/shipping/all',
    },
    { apiName: this.apiName });
  

  getListFilter = (input: BaseListFilterDto) =>
    this.restService.request<any, PagedResultDto<ShippingInlistDto>>({
      method: 'GET',
      url: '/api/app/shipping/filter',
      params: { keyword: input.keyword, status: input.status, skipCount: input.skipCount, maxResultCount: input.maxResultCount },
    },
    { apiName: this.apiName });
  

  update = (id: string, input: CreateUpdateShippingDto) =>
    this.restService.request<any, ShippingDto>({
      method: 'PUT',
      url: `/api/app/shipping/${id}`,
      body: input,
    },
    { apiName: this.apiName });

  constructor(private restService: RestService) {}
}
