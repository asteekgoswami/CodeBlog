using CodeBlog.Models.Domain;
using CodeBlog.Models.ViewModels;
using CodeBlog.Repositories.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CodeBlog.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class BlogPostLikeController : ControllerBase
	{
		private readonly IBlogPostLikeInterface blogPostLikeRepository;

		public BlogPostLikeController(IBlogPostLikeInterface blogPostLikeRepository)
        {
			this.blogPostLikeRepository = blogPostLikeRepository;
		}

        [HttpPost]
		[Route("Add")]
		public async Task<IActionResult> AddLike([FromBody] AddLikeRequest addLikeRequest)
		{


			var model = new BlogPostLike {
				BlogPostId = addLikeRequest.BlogPostId,
				UserId = addLikeRequest.UserId,
			};

			var exist = await blogPostLikeRepository.PostLikeExist(model);
			if(exist==null)
			{
                await blogPostLikeRepository.AddLikeForBlog(model);
                return Ok();
            }
			else
			{
				return Ok("Like Exist");
			}
			
			
			
			
		}
	}
}
