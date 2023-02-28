import type { CategoryDto, CategoryInlistDto, CreateUpdateCategoryDto } from './models';
import { RestService } from '@abp/ng.core';
import type { PagedResultDto, PagedResultRequestDto } from '@abp/ng.core';
import { Injectable } from '@angular/core';
import type { BaseListFilterDto } from '../models';

@Injectable({
  providedIn: 'root',
})
export class CategoriesService {
  apiName = 'Default';
  

  create = (input: CreateUpdateCategoryDto) =>
    this.restService.request<any, CategoryDto>({
      method: 'POST',
      url: '/api/app/categories',
      body: input,
    },
    { apiName: this.apiName });
  

  delete = (id: string) =>
    this.restService.request<any, void>({
      method: 'DELETE',
      url: `/api/app/categories/${id}`,
    },
    { apiName: this.apiName });
  

  deleteMultile = (ids: string[]) =>
    this.restService.request<any, void>({
      method: 'DELETE',
      url: '/api/app/categories/multile',
      params: { ids },
    },
    { apiName: this.apiName });
  

  get = (id: string) =>
    this.restService.request<any, CategoryDto>({
      method: 'GET',
      url: `/api/app/categories/${id}`,
    },
    { apiName: this.apiName });
  

  getImage = (fileName: string) =>
    this.restService.request<any, string>({
      method: 'GET',
      responseType: 'text',
      url: '/api/app/categories/image',
      params: { fileName },
    },
    { apiName: this.apiName });
  

  getList = (input: PagedResultRequestDto) =>
    this.restService.request<any, PagedResultDto<CategoryDto>>({
      method: 'GET',
      url: '/api/app/categories',
      params: { skipCount: input.skipCount, maxResultCount: input.maxResultCount },
    },
    { apiName: this.apiName });
  

  getListAll = () =>
    this.restService.request<any, CategoryInlistDto[]>({
      method: 'GET',
      url: '/api/app/categories/all',
    },
    { apiName: this.apiName });
  

  getListFilter = (input: BaseListFilterDto) =>
    this.restService.request<any, PagedResultDto<CategoryInlistDto>>({
      method: 'GET',
      url: '/api/app/categories/filter',
      params: { keyword: input.keyword, status: input.status, skipCount: input.skipCount, maxResultCount: input.maxResultCount },
    },
    { apiName: this.apiName });
  

  update = (id: string, input: CreateUpdateCategoryDto) =>
    this.restService.request<any, CategoryDto>({
      method: 'PUT',
      url: `/api/app/categories/${id}`,
      body: input,
    },
    { apiName: this.apiName });

  constructor(private restService: RestService) {}
}
