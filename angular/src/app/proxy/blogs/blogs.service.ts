import type { BlogDto, BlogInlistDto, CreateUpdateBlogDto } from './models';
import { RestService } from '@abp/ng.core';
import type { PagedResultDto, PagedResultRequestDto } from '@abp/ng.core';
import { Injectable } from '@angular/core';
import type { BaseListFilterDto } from '../models';

@Injectable({
  providedIn: 'root',
})
export class BlogsService {
  apiName = 'Default';
  

  create = (input: CreateUpdateBlogDto) =>
    this.restService.request<any, BlogDto>({
      method: 'POST',
      url: '/api/app/blogs',
      body: input,
    },
    { apiName: this.apiName });
  

  delete = (id: string) =>
    this.restService.request<any, void>({
      method: 'DELETE',
      url: `/api/app/blogs/${id}`,
    },
    { apiName: this.apiName });
  

  deleteMultile = (ids: string[]) =>
    this.restService.request<any, void>({
      method: 'DELETE',
      url: '/api/app/blogs/multile',
      params: { ids },
    },
    { apiName: this.apiName });
  

  get = (id: string) =>
    this.restService.request<any, BlogDto>({
      method: 'GET',
      url: `/api/app/blogs/${id}`,
    },
    { apiName: this.apiName });
  

  getList = (input: PagedResultRequestDto) =>
    this.restService.request<any, PagedResultDto<BlogDto>>({
      method: 'GET',
      url: '/api/app/blogs',
      params: { skipCount: input.skipCount, maxResultCount: input.maxResultCount },
    },
    { apiName: this.apiName });
  

  getListAll = () =>
    this.restService.request<any, BlogInlistDto[]>({
      method: 'GET',
      url: '/api/app/blogs/all',
    },
    { apiName: this.apiName });
  

  getListFilter = (input: BaseListFilterDto) =>
    this.restService.request<any, PagedResultDto<BlogInlistDto>>({
      method: 'GET',
      url: '/api/app/blogs/filter',
      params: { keyword: input.keyword, status: input.status, skipCount: input.skipCount, maxResultCount: input.maxResultCount },
    },
    { apiName: this.apiName });
  

  update = (id: string, input: CreateUpdateBlogDto) =>
    this.restService.request<any, BlogDto>({
      method: 'PUT',
      url: `/api/app/blogs/${id}`,
      body: input,
    },
    { apiName: this.apiName });

  constructor(private restService: RestService) {}
}
