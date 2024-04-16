using CodeBlog.Data;
using CodeBlog.Models.Domain;
using CodeBlog.Repositories.Interface;
using Microsoft.EntityFrameworkCore;
using System.Linq;

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

        public async Task<int> CountAsync()
        {
            return await dbcontext.Tags.CountAsync();
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

        public async Task<IEnumerable<Tag>> GetAllAsync(string? searchQuery, string? shortBy, string? sortDirection, int pageSize = 10, int pageNumber = 1)
        {
            var query = dbcontext.Tags.AsQueryable();

            //filtering
            if (string.IsNullOrWhiteSpace(searchQuery)==false)
            {
                query = query.Where(x => x.Name.Contains(searchQuery) || x.DisplayName.Contains(searchQuery));
            }


            //sorting
            if(string.IsNullOrWhiteSpace(shortBy) == false)
            {
                var isDesc= string.Equals(sortDirection, "Desc", StringComparison.OrdinalIgnoreCase);

                if(string.Equals(shortBy,"Name", StringComparison.OrdinalIgnoreCase))
                {
                    query = isDesc ? query.OrderByDescending(x => x.Name):query.OrderBy(x => x.Name);
                }

                if (string.Equals(shortBy, "DisplayName", StringComparison.OrdinalIgnoreCase))
                {
                    query = isDesc ? query.OrderByDescending(x => x.DisplayName) : query.OrderBy(x => x.DisplayName);
                }


            }



            //pagination
            var skipResults = (pageNumber - 1) * pageSize;
            query=query.Skip(skipResults).Take(pageSize);

            return await query.ToListAsync();

           //return  await dbcontext.Tags.ToListAsync();
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
