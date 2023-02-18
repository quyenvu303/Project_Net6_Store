import type { ContactDto, ContactInlistDto, CreateUpdateContactDto } from './models';
import { RestService } from '@abp/ng.core';
import type { PagedResultDto, PagedResultRequestDto } from '@abp/ng.core';
import { Injectable } from '@angular/core';
import type { BaseListFilterDto } from '../models';

@Injectable({
  providedIn: 'root',
})
export class ContactsService {
  apiName = 'Default';
  

  create = (input: CreateUpdateContactDto) =>
    this.restService.request<any, ContactDto>({
      method: 'POST',
      url: '/api/app/contacts',
      body: input,
    },
    { apiName: this.apiName });
  

  delete = (id: string) =>
    this.restService.request<any, void>({
      method: 'DELETE',
      url: `/api/app/contacts/${id}`,
    },
    { apiName: this.apiName });
  

  deleteMultile = (ids: string[]) =>
    this.restService.request<any, void>({
      method: 'DELETE',
      url: '/api/app/contacts/multile',
      params: { ids },
    },
    { apiName: this.apiName });
  

  get = (id: string) =>
    this.restService.request<any, ContactDto>({
      method: 'GET',
      url: `/api/app/contacts/${id}`,
    },
    { apiName: this.apiName });
  

  getList = (input: PagedResultRequestDto) =>
    this.restService.request<any, PagedResultDto<ContactDto>>({
      method: 'GET',
      url: '/api/app/contacts',
      params: { skipCount: input.skipCount, maxResultCount: input.maxResultCount },
    },
    { apiName: this.apiName });
  

  getListAll = () =>
    this.restService.request<any, ContactInlistDto[]>({
      method: 'GET',
      url: '/api/app/contacts/all',
    },
    { apiName: this.apiName });
  

  getListFilter = (input: BaseListFilterDto) =>
    this.restService.request<any, PagedResultDto<ContactInlistDto>>({
      method: 'GET',
      url: '/api/app/contacts/filter',
      params: { keyword: input.keyword, status: input.status, skipCount: input.skipCount, maxResultCount: input.maxResultCount },
    },
    { apiName: this.apiName });
  

  update = (id: string, input: CreateUpdateContactDto) =>
    this.restService.request<any, ContactDto>({
      method: 'PUT',
      url: `/api/app/contacts/${id}`,
      body: input,
    },
    { apiName: this.apiName });

  constructor(private restService: RestService) {}
}
