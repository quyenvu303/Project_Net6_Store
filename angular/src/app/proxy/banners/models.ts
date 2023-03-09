import type { EntityDto } from '@abp/ng.core';

export interface BannerDto {
  title?: string;
  image?: string;
  status?: boolean;
  id?: string;
}

export interface BannerInlistDto extends EntityDto<string> {
  title?: string;
  image?: string;
  status?: boolean;
}

export interface CreateUpdateBannerDto {
  title?: string;
  imageName?: string;
  imageContent?: string;
  status?: boolean;
}
