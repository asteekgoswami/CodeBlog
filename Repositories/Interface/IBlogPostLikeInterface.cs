using CodeBlog.Models.Domain;

namespace CodeBlog.Repositories.Interface
{
	public interface IBlogPostLikeInterface
	{
		Task<int> GetTotalLikes(Guid blogPostId);

		Task<IEnumerable<BlogPostLike>> GetLikesForBlog(Guid blogPostId);

		Task<BlogPostLike>  AddLikeForBlog(BlogPostLike blogPostLike);

		Task<BlogPostLike?> PostLikeExist(BlogPostLike model);
	}
}
