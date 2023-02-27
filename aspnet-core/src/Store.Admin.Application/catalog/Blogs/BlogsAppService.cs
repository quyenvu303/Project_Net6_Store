using Microsoft.AspNetCore.Authorization;
using Store.Admin.Blogs;
using Store.Admin.Permissions;
using Store.Blogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.ObjectMapping;

namespace Store.Admin.Blogs
{
    [Authorize(StorePermissions.Blog.Default)]
    public class BlogsAppService : CrudAppService<
        Blog,
        BlogDto,
        Guid,
        PagedResultRequestDto,
        CreateUpdateBlogDto,
        CreateUpdateBlogDto>, IBlogAppService
    {
        public BlogsAppService(IRepository<Blog, Guid> repository) : base(repository)
        {
            GetPolicyName = StorePermissions.Blog.Default;
            GetListPolicyName = StorePermissions.Blog.Default;
            CreatePolicyName = StorePermissions.Blog.Create;
            UpdatePolicyName = StorePermissions.Blog.Update;
            DeletePolicyName = StorePermissions.Blog.Delete;
        }
        [Authorize(StorePermissions.Blog.Delete)]
        public async Task DeleteMultileAsync(IEnumerable<Guid> ids)
        {
            await Repository.DeleteManyAsync(ids);
            await UnitOfWorkManager.Current.SaveChangesAsync();
        }

        [Authorize(StorePermissions.Blog.Default)]
        public async Task<List<BlogInlistDto>> GetListAllAsync()
        {
            var query = await Repository.GetQueryableAsync();
            //query = query.Where(x => x.IsActive == true);
            var data = await AsyncExecuter.ToListAsync(query);
            return ObjectMapper.Map<List<Blog>, List<BlogInlistDto>>(data);
        }

        [Authorize(StorePermissions.Blog.Default)]
        public async Task<PagedResultDto<BlogInlistDto>> GetListFilterAsync(BaseListFilterDto input)
        {
            var query = await Repository.GetQueryableAsync();
            query = query.WhereIf(!string.IsNullOrWhiteSpace(input.Keyword), x => x.Title.Contains(input.Keyword));
            query = query.Where(x => x.Status == input.Status);
            var totalCount = await AsyncExecuter.LongCountAsync(query);
            var data = await AsyncExecuter.ToListAsync(query.Skip(input.SkipCount).Take(input.MaxResultCount));

            return new PagedResultDto<BlogInlistDto>(totalCount, ObjectMapper.Map<List<Blog>, List<BlogInlistDto>>(data));
        }
    }
}
