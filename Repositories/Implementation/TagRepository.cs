using CodeBlog.Data;
using CodeBlog.Models.Domain;
using CodeBlog.Repositories.Interface;
using Microsoft.EntityFrameworkCore;

namespace CodeBlog.Repositories.Implementation
{
    public class TagRepository : ITagInterface
    {

        private readonly CodeBlogDbContext dbcontext;
        public TagRepository(CodeBlogDbContext dbcontext)
        {
            this.dbcontext = dbcontext;
        }

        public async Task<Tag> AddAsync(Tag tag)
        {
            await dbcontext.Tags.AddAsync(tag);
            await dbcontext.SaveChangesAsync();
            return tag;
        }

        public async Task<Tag?> DeleteAsync(Guid id)
        {
            var existingTag = await dbcontext.Tags.FindAsync(id);
            if(existingTag != null)
            {
                 dbcontext.Tags.Remove(existingTag);
                await dbcontext.SaveChangesAsync();
                return existingTag;
            }
            return null;
        }

        public async Task<IEnumerable<Tag>> GetAllAsync()
        {
           return  await dbcontext.Tags.ToListAsync();
        }

        public async Task<Tag?> GetAsync(Guid id)
        {
            return  await dbcontext.Tags.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Tag?> UpdateAsync(Tag tag)
        {
            var existingTag = await dbcontext.Tags.FindAsync(tag.Id);
            if(existingTag != null)
            {
                existingTag.Name= tag.Name;
                existingTag.DisplayName= tag.DisplayName;
                await dbcontext.SaveChangesAsync();
                return existingTag;
            }
            return null;
        }
    }
}
