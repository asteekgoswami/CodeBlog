using CodeBlog.Data;
using CodeBlog.Models.Domain;
using CodeBlog.Repositories.Interface;
using Microsoft.EntityFrameworkCore;

namespace CodeBlog.Repositories.Implementation
{
	public class BlogPostCommentRepository : IBlogPostCommentRepository
	{
		private readonly CodeBlogDbContext dbContext;

		public BlogPostCommentRepository(CodeBlogDbContext dbContext)
        {
			this.dbContext = dbContext;
		}
        public async Task<BlogPostComment> AddAsync(BlogPostComment comment)
		{
			await dbContext.BlogPostComment.AddAsync(comment);
			await dbContext.SaveChangesAsync();
			return comment;
		}

		public async Task<IEnumerable<BlogPostComment>> GetCommentsByBlogIdAsync(Guid blogPostId)
		{
			return await dbContext.BlogPostComment.Where(x=>x.BlogPostId==blogPostId).ToListAsync();	
		}
	}
}
