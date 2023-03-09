using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Store.Public.Blogs;
using Store.Public.Products;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Store.Public.Web.Pages.Home.Blogs
{
    public class DetailsModel : PageModel
    {
        private readonly IBlogsAppService _blogsAppService;
        public DetailsModel(IBlogsAppService blogsAppService)
        {
            _blogsAppService = blogsAppService;
        }
        public BlogDto Blog { get; set; }
        public List<BlogInlistDto> TopBlog { get; set; }
        public async Task OnGetAsync(Guid? Id, string Title)
        {
            Blog = await _blogsAppService.GetBlogByIdAsync(Id,Title);
            TopBlog = await _blogsAppService.GetListTopBlogAsync(4);
        }
    }
}
