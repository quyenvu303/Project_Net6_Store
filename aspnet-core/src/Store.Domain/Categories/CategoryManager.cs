using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Domain.Services;

namespace Store.Categories
{
    public class CategoryManager : DomainService
    {
        private readonly IRepository<Category, Guid> _CategoryRepository;
        public CategoryManager(IRepository<Category, Guid> CategoryRepository)
        {
            _CategoryRepository = CategoryRepository;
        }
        public async Task<Category> CreateAsync(
            string categoryId,
            string categoryName,
            string slug,
            int? sortOrder,
            string description, 
            //string icon, 
            Guid? parentId,
            bool? isActive)
        {
            if (await _CategoryRepository.AnyAsync(x => x.CategoryId == categoryId))
                throw new UserFriendlyException("Mã đã tồn tại", StoreDomainErrorCodes.CategoryIdAlreadyExists);
            if (await _CategoryRepository.AnyAsync(x => x.CategoryName == categoryName))
                throw new UserFriendlyException("Tên đã tồn tại", StoreDomainErrorCodes.CategoryNameAlreadyExists);

            return new Category(Guid.NewGuid(), categoryId,categoryName, slug,sortOrder, description,null,parentId,isActive);
        }
    }
}
