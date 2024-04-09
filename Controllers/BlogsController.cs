using CodeBlog.Models.ViewModels;
using CodeBlog.Repositories.Interface;
using Microsoft.AspNetCore.Mvc;

namespace CodeBlog.Controllers
{
    public class BlogsController : Controller
    {
        private readonly IBlogPostInterface blogPostRepository;
		private readonly IBlogPostLikeInterface blogPostLikeRepository;

		public BlogsController(IBlogPostInterface blogPostRepository,IBlogPostLikeInterface blogPostLikeRepository)
        {
            this.blogPostRepository = blogPostRepository;
			this.blogPostLikeRepository = blogPostLikeRepository;
		}

        [HttpGet]
        public async Task<IActionResult> Index(string urlHandle)
        {

            var blogPost = await blogPostRepository.GetByUrlHanldeAsync(urlHandle);

			var blogPostLikeViewModel = new BlogDetailsViewModel();

			if (blogPost != null) 
            {
                var totalLike = await blogPostLikeRepository.GetTotalLikes(blogPost.Id);
				 blogPostLikeViewModel = new BlogDetailsViewModel
				{
					Id = blogPost.Id,
					Content = blogPost.Content,
					PageTitle = blogPost.PageTitle,
					Author = blogPost.Author,
					FeaturedImageUrl = blogPost.FeaturedImageUrl,
					Heading = blogPost.Heading,
					PublishedDate = blogPost.PublishedDate,
					ShortDescription = blogPost.ShortDescription,
					UrlHandle = blogPost.UrlHandle,
					Visible = blogPost.Visible,
					Tags = blogPost.Tags,
					TotalLikes = totalLike,

				};
				
            }
            return View(blogPostLikeViewModel);
        }
    }
}
