using CodeBlog.Models.Domain;

namespace CodeBlog.Repositories.Interface
{
    public interface ITagInterface
    {
        Task<IEnumerable<Tag>> GetAllAsync(string? searchQuery=null, string? shortBy=null, string? sortDirection=null, int pageSize = 10, int pageNumber = 1);

        Task<Tag?> GetAsync(Guid id);

        Task<Tag> AddAsync(Tag tag);

        Task<Tag?> UpdateAsync(Tag tag);

        Task<Tag?> DeleteAsync(Guid id);

        Task<int> CountAsync();
    }
}
