import type { CreateUpdateProductDto, ProductDto, ProductFilter, ProductInlistDto } from './models';
import { RestService } from '@abp/ng.core';
import type { PagedResultDto, PagedResultRequestDto } from '@abp/ng.core';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root',
})
export class ProductsService {
  apiName = 'Default';
  

  create = (input: CreateUpdateProductDto) =>
    this.restService.request<any, ProductDto>({
      method: 'POST',
      url: '/api/app/products',
      body: input,
    },
    { apiName: this.apiName });
  

  delete = (id: string) =>
    this.restService.request<any, void>({
      method: 'DELETE',
      url: `/api/app/products/${id}`,
    },
    { apiName: this.apiName });
  

  deleteMultile = (ids: string[]) =>
    this.restService.request<any, void>({
      method: 'DELETE',
      url: '/api/app/products/multile',
      params: { ids },
    },
    { apiName: this.apiName });
  

  get = (id: string) =>
    this.restService.request<any, ProductDto>({
      method: 'GET',
      url: `/api/app/products/${id}`,
    },
    { apiName: this.apiName });
  

  getList = (input: PagedResultRequestDto) =>
    this.restService.request<any, PagedResultDto<ProductDto>>({
      method: 'GET',
      url: '/api/app/products',
      params: { skipCount: input.skipCount, maxResultCount: input.maxResultCount },
    },
    { apiName: this.apiName });
  

  getListAll = () =>
    this.restService.request<any, ProductInlistDto[]>({
      method: 'GET',
      url: '/api/app/products/all',
    },
    { apiName: this.apiName });
  

  getListFilter = (input: ProductFilter) =>
    this.restService.request<any, PagedResultDto<ProductInlistDto>>({
      method: 'GET',
      url: '/api/app/products/filter',
      params: { categoryId: input.categoryId, warehouseGuid: input.warehouseGuid, keyword: input.keyword, status: input.status, skipCount: input.skipCount, maxResultCount: input.maxResultCount },
    },
    { apiName: this.apiName });
  

  update = (id: string, input: CreateUpdateProductDto) =>
    this.restService.request<any, ProductDto>({
      method: 'PUT',
      url: `/api/app/products/${id}`,
      body: input,
    },
    { apiName: this.apiName });

  constructor(private restService: RestService) {}
}
