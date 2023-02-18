import type { EntityDto } from '@abp/ng.core';

export interface ContactDto {
  title?: string;
  description?: string;
  status?: boolean;
  id?: string;
}

export interface ContactInlistDto extends EntityDto<string> {
  title?: string;
  description?: string;
  status?: boolean;
}

export interface CreateUpdateContactDto {
  title?: string;
  description?: string;
  status?: boolean;
}
