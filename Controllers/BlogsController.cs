using CodeBlog.Models.Domain;
using CodeBlog.Models.ViewModels;
using CodeBlog.Repositories.Interface;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CodeBlog.Controllers
{
    public class BlogsController : Controller
    {
        private readonly IBlogPostInterface blogPostRepository;
		private readonly IBlogPostLikeInterface blogPostLikeRepository;
		private readonly SignInManager<IdentityUser> signInManager;
		private readonly UserManager<IdentityUser> userManager;
		private readonly IBlogPostCommentRepository blogPostCommentRepository;

		public BlogsController(IBlogPostInterface blogPostRepository,IBlogPostLikeInterface blogPostLikeRepository,SignInManager<IdentityUser> signInManager, UserManager<IdentityUser> userManager, IBlogPostCommentRepository blogPostCommentRepository)
        {
            this.blogPostRepository = blogPostRepository;
			this.blogPostLikeRepository = blogPostLikeRepository;
			this.signInManager = signInManager;
			this.userManager = userManager;
			this.blogPostCommentRepository = blogPostCommentRepository;
		}

        [HttpGet]
        public async Task<IActionResult> Index(string urlHandle)
        {
			var liked = false;

            var blogPost = await blogPostRepository.GetByUrlHanldeAsync(urlHandle);

			var blogPostLikeViewModel = new BlogDetailsViewModel();

			if (blogPost != null) 
            {
                var totalLike = await blogPostLikeRepository.GetTotalLikes(blogPost.Id);

				if (signInManager.IsSignedIn(User))
				{
					//get like for this blog for this user
					var likesForBlog =	await blogPostLikeRepository.GetLikesForBlog(blogPost.Id);

					var userId = userManager.GetUserId(User);

					if (userId != null)
					{
						var likeFromUser= likesForBlog.FirstOrDefault(x=> x.UserId == Guid.Parse(userId));

						liked = likeFromUser!=null;

					}

				}

				//get the comments for this blog
				var blogCommentsDomainModel = await blogPostCommentRepository.GetCommentsByBlogIdAsync(blogPost.Id);


				var blogCommentsForView = new List<BlogComment>();
				foreach(var blogComment in blogCommentsDomainModel)
				{
					blogCommentsForView.Add(new BlogComment
					{
						Description = blogComment.Description,
						DateAdded = blogComment.DateAdded,
						Username = (await userManager.FindByIdAsync(blogComment.UserId.ToString())).UserName
					});
				}


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
					Liked= liked,
					Comments= blogCommentsForView,
				};
				
            }
            return View(blogPostLikeViewModel);
        }



		[HttpPost]
		public async Task<IActionResult> Index(BlogDetailsViewModel blogDetailsViewModel)
		{
			if(signInManager.IsSignedIn(User))
			{
				var domainModel = new BlogPostComment
				{
					BlogPostId = blogDetailsViewModel.Id,
					Description = blogDetailsViewModel.CommentDescription,
					UserId = Guid.Parse(userManager.GetUserId(User)),
					DateAdded=DateTime.Now,
				};
				await blogPostCommentRepository.AddAsync(domainModel);
				return RedirectToAction("Index", "Blogs", new {urlHandle = blogDetailsViewModel.UrlHandle});
	 		}

			//if user is not login
			return View();
		}



    }
}
