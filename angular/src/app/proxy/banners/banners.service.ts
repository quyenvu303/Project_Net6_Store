import type { BannerDto, BannerInlistDto, CreateUpdateBannerDto } from './models';
import { RestService } from '@abp/ng.core';
import type { PagedResultDto, PagedResultRequestDto } from '@abp/ng.core';
import { Injectable } from '@angular/core';
import type { BaseListFilterDto } from '../models';

@Injectable({
  providedIn: 'root',
})
export class BannersService {
  apiName = 'Default';
  

  create = (input: CreateUpdateBannerDto) =>
    this.restService.request<any, BannerDto>({
      method: 'POST',
      url: '/api/app/banners',
      body: input,
    },
    { apiName: this.apiName });
  

  delete = (id: string) =>
    this.restService.request<any, void>({
      method: 'DELETE',
      url: `/api/app/banners/${id}`,
    },
    { apiName: this.apiName });
  

  deleteMultile = (ids: string[]) =>
    this.restService.request<any, void>({
      method: 'DELETE',
      url: '/api/app/banners/multile',
      params: { ids },
    },
    { apiName: this.apiName });
  

  get = (id: string) =>
    this.restService.request<any, BannerDto>({
      method: 'GET',
      url: `/api/app/banners/${id}`,
    },
    { apiName: this.apiName });
  

  getImage = (fileName: string) =>
    this.restService.request<any, string>({
      method: 'GET',
      responseType: 'text',
      url: '/api/app/banners/image',
      params: { fileName },
    },
    { apiName: this.apiName });
  

  getList = (input: PagedResultRequestDto) =>
    this.restService.request<any, PagedResultDto<BannerDto>>({
      method: 'GET',
      url: '/api/app/banners',
      params: { skipCount: input.skipCount, maxResultCount: input.maxResultCount },
    },
    { apiName: this.apiName });
  

  getListAll = () =>
    this.restService.request<any, BannerInlistDto[]>({
      method: 'GET',
      url: '/api/app/banners/all',
    },
    { apiName: this.apiName });
  

  getListFilter = (input: BaseListFilterDto) =>
    this.restService.request<any, PagedResultDto<BannerInlistDto>>({
      method: 'GET',
      url: '/api/app/banners/filter',
      params: { keyword: input.keyword, status: input.status, skipCount: input.skipCount, maxResultCount: input.maxResultCount },
    },
    { apiName: this.apiName });
  

  update = (id: string, input: CreateUpdateBannerDto) =>
    this.restService.request<any, BannerDto>({
      method: 'PUT',
      url: `/api/app/banners/${id}`,
      body: input,
    },
    { apiName: this.apiName });

  constructor(private restService: RestService) {}
}
