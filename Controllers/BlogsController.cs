using CodeBlog.Repositories.Interface;
using Microsoft.AspNetCore.Mvc;

namespace CodeBlog.Controllers
{
    public class BlogsController : Controller
    {
        private readonly IBlogPostInterface blogPostRepository;

        public BlogsController(IBlogPostInterface blogPostRepository)
        {
            this.blogPostRepository = blogPostRepository;
        }

        [HttpGet]
        public async Task<IActionResult> Index(string urlHandle)
        {

            var blogPost = await blogPostRepository.GetByUrlHanldeAsync(urlHandle); 
            return View(blogPost);
        }
    }
}
