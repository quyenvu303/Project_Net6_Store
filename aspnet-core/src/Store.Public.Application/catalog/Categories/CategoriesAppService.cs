﻿using AutoMapper.Internal.Mappers;
using Microsoft.AspNetCore.Authorization;
using Store.Categories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.ObjectMapping;
using Volo.Abp.Uow;

namespace Store.Public.Categories
{
    public class CategoriesAppService : ReadOnlyAppService<
         Category,
         CategoryDto,
         Guid,
         PagedResultRequestDto>, ICategoriesAppService
    {
        public CategoriesAppService(IRepository<Category, Guid> repository, CategoryManager categoryManager)
            : base(repository)
        {
        }
        public async Task<List<CategoryInlistDto>> GetListAllAsync()
        {
            var query = await Repository.GetQueryableAsync();
            query = query.Where(x => x.IsActive == true);
            var data = await AsyncExecuter.ToListAsync(query);
            return ObjectMapper.Map<List<Category>, List<CategoryInlistDto>>(data);
        }

        public async Task<PagedResult<CategoryInlistDto>> GetListFilterAsync(BaseListFilterDto input)
        {
            var query = await Repository.GetQueryableAsync();
            query = query.WhereIf(!string.IsNullOrWhiteSpace(input.Keyword), x => x.CategoryName.Contains(input.Keyword));

            var totalCount = await AsyncExecuter.LongCountAsync(query);
            var data = await AsyncExecuter
            .ToListAsync(
               query.Skip((input.CurrentPage - 1) * input.PageSize)
            .Take(input.PageSize));

            return new PagedResult<CategoryInlistDto>(
                ObjectMapper.Map<List<Category>, List<CategoryInlistDto>>(data),
                totalCount,
                input.CurrentPage,
                input.PageSize
            );
        }


    }
}