using CodeBlog.Models.Domain;

namespace CodeBlog.Repositories.Interface
{
	public interface IBlogPostCommentRepository
	{
		Task<BlogPostComment> AddAsync(BlogPostComment comment);

		Task<IEnumerable<BlogPostComment>> GetCommentsByBlogIdAsync(Guid blogPostId);
	}
}
