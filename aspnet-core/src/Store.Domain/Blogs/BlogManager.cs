using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Domain.Services;

namespace Store.Blogs
{
    public class BlogManager : DomainService
    {
        private readonly IRepository<Blog, Guid> _BlogRepository;
        public BlogManager(IRepository<Blog, Guid> BlogRepository)
        {
            _BlogRepository = BlogRepository;
        }
        public async Task<Blog> CreateAsync(string title, string description, bool? status) {
            return new Blog(Guid.NewGuid(), title, null,description,status);
        }
    }
}
