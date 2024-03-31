using CodeBlog.Models.Domain;

namespace CodeBlog.Repositories.Interface
{
    public interface IBlogPostInterface
    {
        Task<IEnumerable<BlogPost>> GetAllAsync();

        Task<BlogPost?> GetAsync(Guid id);

        Task<BlogPost?> GetByUrlHanldeAsync(string urlHandle);

        Task<BlogPost> AddAsync(BlogPost blogPost);

        Task<BlogPost?> UpdateAsync(BlogPost blogPost);

        Task<BlogPost?> DeleteAsync(Guid id);
    }
}
