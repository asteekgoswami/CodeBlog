using CodeBlog.Data;
using CodeBlog.Models.Domain;
using CodeBlog.Repositories.Interface;
using Microsoft.EntityFrameworkCore;

namespace CodeBlog.Repositories.Implementation
{
	public class BlogPostLikeRepository : IBlogPostLikeInterface
	{
		private readonly CodeBlogDbContext codeBlogDbContext;

		public BlogPostLikeRepository(CodeBlogDbContext codeBlogDbContext )
		{
			this.codeBlogDbContext = codeBlogDbContext;
		}

		public async Task<BlogPostLike> AddLikeForBlog(BlogPostLike blogPostLike)
		{
			await codeBlogDbContext.BlogPostLike.AddAsync( blogPostLike );
			await codeBlogDbContext.SaveChangesAsync();
			return blogPostLike;
		}

		public  async Task<IEnumerable<BlogPostLike>> GetLikesForBlog(Guid blogPostId)
		{
			return  await codeBlogDbContext.BlogPostLike.Where(x=>x.BlogPostId==blogPostId).ToListAsync();

		}

		public async Task<int> GetTotalLikes(Guid blogPostId)
		{
			var likes = await codeBlogDbContext.BlogPostLike.CountAsync(x=>x.BlogPostId==blogPostId);
			return likes;
		}

        public Task<BlogPostLike?> PostLikeExist(BlogPostLike model)
        {
			var likeExist = codeBlogDbContext.BlogPostLike.FirstOrDefaultAsync(x=>x.BlogPostId==model.BlogPostId && x.UserId==model.UserId);
			return likeExist;
        }
    }
}
