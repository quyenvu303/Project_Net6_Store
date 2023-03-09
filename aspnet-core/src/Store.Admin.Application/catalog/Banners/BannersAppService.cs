using Microsoft.AspNetCore.Authorization;
using Store.Admin.Banners;
using Store.Admin.Permissions;
using Store.Banners;
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

namespace Store.Admin.Banners
{
    [Authorize(StorePermissions.Banner.Default)]
    public class BannersAppService : CrudAppService<
        Banner,
        BannerDto,
        Guid,
        PagedResultRequestDto,
        CreateUpdateBannerDto,
        CreateUpdateBannerDto>, IBannerAppService
    {
        private readonly BannerManager _bannerManager;
        private readonly IBlobContainer<BannerImageContainer> _fileContainer;
        public BannersAppService(IRepository<Banner, Guid> repository,
            IBlobContainer<BannerImageContainer> fileContainer,
            BannerManager bannerManager) : base(repository)
        {
            _bannerManager = bannerManager;
            _fileContainer = fileContainer;


            GetPolicyName = StorePermissions.Banner.Default;
            GetListPolicyName = StorePermissions.Banner.Default;
            CreatePolicyName = StorePermissions.Banner.Create;
            UpdatePolicyName = StorePermissions.Banner.Update;
            DeletePolicyName = StorePermissions.Banner.Delete;
        }

         [Authorize(StorePermissions.Banner.Create)]
        public override async Task<BannerDto> CreateAsync(CreateUpdateBannerDto input)
        {
            var banner = await _bannerManager.CreateAsync(input.Title,input.Status);
            if (input.ImageContent != null && input.ImageContent.Length > 0)
            {
                await SaveImageAsync(input.ImageName, input.ImageContent);
                banner.Image = input.ImageName;
            }
            var result = await Repository.InsertAsync(banner);
            return ObjectMapper.Map<Banner, BannerDto>(result);
        }
        [Authorize(StorePermissions.Banner.Update)]
        public override async Task<BannerDto> UpdateAsync(Guid id, CreateUpdateBannerDto input)
        {
            var banner = await Repository.GetAsync(id);
            if (banner == null)
            {
                throw new BusinessException(StoreDomainErrorCodes.CategoryIsNotExists);
            }
            banner.Title = input.Title;
            if (input.ImageContent != null && input.ImageContent.Length > 0)
            {
                await SaveImageAsync(input.ImageName, input.ImageContent);
                banner.Image = input.ImageName;
            }
            banner.Status = input.Status;
            await Repository.UpdateAsync(banner);
            return ObjectMapper.Map<Banner, BannerDto>(banner);
        }


        private async Task SaveImageAsync(string fileName, string base64)
        {
            Regex regex = new Regex(@"^[\w/\:.-]+;base64,");
            base64 = regex.Replace(base64, string.Empty);
            byte[] bytes = Convert.FromBase64String(base64);
            await _fileContainer.SaveAsync(fileName, bytes, overrideExisting: true);
        }
        [Authorize(StorePermissions.Banner.Default)]
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
        [Authorize(StorePermissions.Banner.Delete)]
        public async Task DeleteMultileAsync(IEnumerable<Guid> ids)
        {
            await Repository.DeleteManyAsync(ids);
            await UnitOfWorkManager.Current.SaveChangesAsync();
        }
        
        [Authorize(StorePermissions.Banner.Default)]
        public async Task<List<BannerInlistDto>> GetListAllAsync()
        {
            var query = await Repository.GetQueryableAsync();
            var data = await AsyncExecuter.ToListAsync(query);
            return ObjectMapper.Map<List<Banner>, List<BannerInlistDto>>(data);
        }

        [Authorize(StorePermissions.Banner.Default)]
        public async Task<PagedResultDto<BannerInlistDto>> GetListFilterAsync(BaseListFilterDto input)
        {
            var query = await Repository.GetQueryableAsync();
            query = query.WhereIf(!string.IsNullOrWhiteSpace(input.Keyword), x => x.Title.Contains(input.Keyword));
            query = query.Where(x => x.Status == input.Status);
            var totalCount = await AsyncExecuter.LongCountAsync(query);
            var data = await AsyncExecuter.ToListAsync(query.Skip(input.SkipCount).Take(input.MaxResultCount));

            return new PagedResultDto<BannerInlistDto>(totalCount, ObjectMapper.Map<List<Banner>, List<BannerInlistDto>>(data));
        }
    }
}
