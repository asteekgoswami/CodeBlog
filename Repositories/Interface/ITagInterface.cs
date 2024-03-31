using CodeBlog.Models.Domain;

namespace CodeBlog.Repositories.Interface
{
    public interface ITagInterface
    {
        Task<IEnumerable<Tag>> GetAllAsync();

        Task<Tag?> GetAsync(Guid id);

        Task<Tag> AddAsync(Tag tag);

        Task<Tag?> UpdateAsync(Tag tag);

        Task<Tag?> DeleteAsync(Guid id);
    }
}
