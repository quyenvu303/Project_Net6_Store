using Microsoft.AspNetCore.Authorization;
using Store.Admin.Blogs;
using Store.Admin.Permissions;
using Store.Blogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.BlobStoring;
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
        private readonly BlogManager _blogManager;
        private readonly IBlobContainer<BlogImageContainer> _fileContainer;
        public BlogsAppService(IRepository<Blog, Guid> repository,
            BlogManager blogManager,
            IBlobContainer<BlogImageContainer> fileContainer) : base(repository)
        {
            _blogManager = blogManager;
            _fileContainer = fileContainer;

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

        [Authorize(StorePermissions.Blog.Create)]
        public override async Task<BlogDto> CreateAsync(CreateUpdateBlogDto input)
        {
            var blog = await _blogManager.CreateAsync(
                input.Title,
                input.Description,
                input.Status);
            if (input.ImageContent != null && input.ImageContent.Length > 0)
            {
                await SaveImageAsync(input.ImageName, input.ImageContent);
                blog.Image = input.ImageName;
            }
            var result = await Repository.InsertAsync(blog);
            return ObjectMapper.Map<Blog, BlogDto>(result);
        }
        [Authorize(StorePermissions.Blog.Update)]
        public override async Task<BlogDto> UpdateAsync(Guid id, CreateUpdateBlogDto input)
        {
            var blog = await Repository.GetAsync(id);
            if (blog == null)
            {
                throw new BusinessException(StoreDomainErrorCodes.CategoryIsNotExists);
            }
            blog.Title = blog.Title;
            blog.Description = blog.Description;
            if (input.ImageContent != null && input.ImageContent.Length > 0)
            {
                await SaveImageAsync(input.ImageName, input.ImageContent);
                blog.Image = input.ImageName;
            }
            blog.Status = blog.Status;
            await Repository.UpdateAsync(blog);
            return ObjectMapper.Map<Blog, BlogDto>(blog);
        }

        private async Task SaveImageAsync(string fileName, string base64)
        {
            Regex regex = new Regex(@"^[\w/\:.-]+;base64,");
            base64 = regex.Replace(base64, string.Empty);
            byte[] bytes = Convert.FromBase64String(base64);
            await _fileContainer.SaveAsync(fileName, bytes, overrideExisting: true);
        }
        [Authorize(StorePermissions.Blog.Default)]
        public async Task<string> GetImageAsync(string fileName)
        {
            if (string.IsNullOrEmpty(fileName))
            {
                return null;
            }
            var thumbnailContent = await _fileContainer.GetAllBytesOrNullAsync(fileName);

            if (thumbnailContent is null)
            {
                return null;
            }
            var result = Convert.ToBase64String(thumbnailContent);
            return result;
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
