using AutoMapper.Internal.Mappers;
using Microsoft.AspNetCore.Authorization;
using Store.Banners;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.BlobStoring;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Uow;
using Volo.Abp;
using Volo.Abp.ObjectMapping;

namespace Store.Public.Banners
{
    public class BannersAppService : ReadOnlyAppService<
        Banner,
        BannerDto,
        Guid,
        PagedResultRequestDto>, IBannersAppService
    {
        private readonly BannerManager _bannerManager;
        private readonly IBlobContainer<BannerImageContainer> _fileContainer;
        public BannersAppService(IRepository<Banner, Guid> repository,
            IBlobContainer<BannerImageContainer> fileContainer,
            BannerManager bannerManager) : base(repository)
        {
            _bannerManager = bannerManager;
            _fileContainer = fileContainer;
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
       
        public async Task<List<BannerInlistDto>> GetListAllAsync()
        {
            var query = await Repository.GetQueryableAsync();
            var data = await AsyncExecuter.ToListAsync(query);
            return ObjectMapper.Map<List<Banner>, List<BannerInlistDto>>(data);
        }

        public async Task<PagedResult<BannerInlistDto>> GetListFilterAsync(BaseListFilterDto input)
        {
            var query = await Repository.GetQueryableAsync();
            query = query.WhereIf(!string.IsNullOrWhiteSpace(input.Keyword), x => x.Title.Contains(input.Keyword));
            var totalCount = await AsyncExecuter.LongCountAsync(query);
            var data = await AsyncExecuter
            .ToListAsync(
               query.Skip((input.CurrentPage - 1) * input.PageSize)
            .Take(input.PageSize));

            return new PagedResult<BannerInlistDto>(
                ObjectMapper.Map<List<Banner>, List<BannerInlistDto>>(data),
                totalCount,
                input.CurrentPage,
                input.PageSize
            );
        }

        public async Task<List<BannerInlistDto>> GetListTopBannerAsync(int numberOfRecords)
        {
            var query = await Repository.GetQueryableAsync();
            query = query.Where(x => x.Status == true)
                .Take(numberOfRecords);
            var data = await AsyncExecuter.ToListAsync(query);
            return ObjectMapper.Map<List<Banner>, List<BannerInlistDto>>(data);
        }

    }
}
