import { RestService } from '@abp/ng.core';
import type { PagedResultDto, PagedResultRequestDto } from '@abp/ng.core';
import { Injectable } from '@angular/core';
import type { OrderItemDto } from '../orders/models';

@Injectable({
  providedIn: 'root',
})
export class OrderItemService {
  apiName = 'Default';
  

  create = (input: OrderItemDto) =>
    this.restService.request<any, OrderItemDto>({
      method: 'POST',
      url: '/api/app/order-item',
      body: input,
    },
    { apiName: this.apiName });
  

  delete = (id: string) =>
    this.restService.request<any, void>({
      method: 'DELETE',
      url: `/api/app/order-item/${id}`,
    },
    { apiName: this.apiName });
  

  get = (id: string) =>
    this.restService.request<any, OrderItemDto>({
      method: 'GET',
      url: `/api/app/order-item/${id}`,
    },
    { apiName: this.apiName });
  

  getImage = (fileName: string) =>
    this.restService.request<any, string>({
      method: 'GET',
      responseType: 'text',
      url: '/api/app/order-item/image',
      params: { fileName },
    },
    { apiName: this.apiName });
  

  getList = (input: PagedResultRequestDto) =>
    this.restService.request<any, PagedResultDto<OrderItemDto>>({
      method: 'GET',
      url: '/api/app/order-item',
      params: { skipCount: input.skipCount, maxResultCount: input.maxResultCount },
    },
    { apiName: this.apiName });
  

  getOrderListItems = (Id: string) =>
    this.restService.request<any, OrderItemDto[]>({
      method: 'GET',
      url: `/api/app/order-item/order-list-items/${Id}`,
    },
    { apiName: this.apiName });
  

  update = (id: string, input: OrderItemDto) =>
    this.restService.request<any, OrderItemDto>({
      method: 'PUT',
      url: `/api/app/order-item/${id}`,
      body: input,
    },
    { apiName: this.apiName });

  constructor(private restService: RestService) {}
}
