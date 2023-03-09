using Store.Blogs;
using Store.Public.Banners;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.BlobStoring;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.ObjectMapping;

namespace Store.Public.Blogs
{
    public class BlogsAppService : ReadOnlyAppService<
        Blog,
        BlogDto,
        Guid,
        PagedResultRequestDto>, IBlogsAppService
    {
        private readonly BlogManager _blogManager;
        private readonly IBlobContainer<BlogImageContainer> _fileContainer;
        private readonly IRepository<Blog, Guid> _blogRepository;
        public BlogsAppService(IReadOnlyRepository<Blog, Guid> repository,
            BlogManager blogManager,
            IRepository<Blog, Guid> blogRepository,
            IBlobContainer<BlogImageContainer> fileContainer) : base(repository)
        {
            _blogManager = blogManager;
            _fileContainer = fileContainer;
            _blogRepository= blogRepository;
        }

        public async Task<BlogDto> GetBlogByIdAsync(Guid? Id, string Title)
        {
            var blog = await _blogRepository.GetAsync(x => x.Id == Id && x.Title == Title);
            return ObjectMapper.Map<Blog, BlogDto>(blog);
        }

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

        public async Task<List<BlogInlistDto>> GetListAllAsync()
        {
            var query = await Repository.GetQueryableAsync();
            var data = await AsyncExecuter.ToListAsync(query);
            return ObjectMapper.Map<List<Blog>, List<BlogInlistDto>>(data);
        }

        public async Task<PagedResult<BlogInlistDto>> GetListFilterAsync(BaseListFilterDto input)
        {
            var query = await Repository.GetQueryableAsync();
            query = query.WhereIf(!string.IsNullOrWhiteSpace(input.Keyword), x => x.Title.Contains(input.Keyword));
            var totalCount = await AsyncExecuter.LongCountAsync(query);
            var data = await AsyncExecuter
            .ToListAsync(
               query.Skip((input.CurrentPage - 1) * input.PageSize)
            .Take(input.PageSize));

            return new PagedResult<BlogInlistDto>(
                ObjectMapper.Map<List<Blog>, List<BlogInlistDto>>(data),
                totalCount,
                input.CurrentPage,
                input.PageSize
            );
        }

        public async Task<List<BlogInlistDto>> GetListTopBlogAsync(int numberOfRecords)
        {
            var query = await Repository.GetQueryableAsync();
            query = query.Where(x => x.Status == true)
                .Take(numberOfRecords);
            var data = await AsyncExecuter.ToListAsync(query);
            return ObjectMapper.Map<List<Blog>, List<BlogInlistDto>>(data);
        }
    }
}
