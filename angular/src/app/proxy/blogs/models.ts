import type { EntityDto } from '@abp/ng.core';

export interface BlogDto {
  title?: string;
  image?: string;
  description?: string;
  status?: boolean;
  id?: string;
}

export interface BlogInlistDto extends EntityDto<string> {
  title?: string;
  image?: string;
  description?: string;
  status?: boolean;
}

export interface CreateUpdateBlogDto {
  title?: string;
  image?: string;
  description?: string;
  status?: boolean;
}
