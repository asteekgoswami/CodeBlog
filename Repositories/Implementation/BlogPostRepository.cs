using CodeBlog.Data;
using CodeBlog.Models.Domain;
using CodeBlog.Repositories.Interface;
using Microsoft.EntityFrameworkCore;

namespace CodeBlog.Repositories.Implementation
{
    public class BlogPostRepository : IBlogPostInterface
    {
        private readonly CodeBlogDbContext dbContext;

        public BlogPostRepository(CodeBlogDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public  async Task<BlogPost> AddAsync(BlogPost blogPost)
        {
               
            await  dbContext.AddAsync(blogPost);
            await dbContext.SaveChangesAsync();
            return blogPost;
        }

        public async Task<BlogPost?> DeleteAsync(Guid id)
        {
            var existingBlog = await dbContext.BlogPosts.FindAsync(id);
            if(existingBlog != null)
            {
                dbContext.BlogPosts.Remove(existingBlog);
                await dbContext.SaveChangesAsync();
                return existingBlog;
            }
            else
            {
                return null;
            }
        }

        public async  Task<IEnumerable<BlogPost>> GetAllAsync()
        {
            return await dbContext.BlogPosts.Include(x=>x.Tags).ToListAsync();
        
        }

        public async Task<BlogPost?> GetAsync(Guid id)
        {
            return await dbContext.BlogPosts.Include(x=>x.Tags).FirstOrDefaultAsync(x=>x.Id==id);
        }

        public async Task<BlogPost?> GetByUrlHanldeAsync(string urlHandle)
        {
            return await dbContext.BlogPosts.Include(x=>x.Tags).FirstOrDefaultAsync(x=>x.UrlHandle==urlHandle);
        }

        public async Task<BlogPost?> UpdateAsync(BlogPost blogPost)
        {
           var existingBlog=  await dbContext.BlogPosts.Include(x=>x.Tags).FirstOrDefaultAsync(x=>x.Id==blogPost.Id);
            if(existingBlog!=null)
            {
                existingBlog.Id=blogPost.Id;
                existingBlog.Heading=blogPost.Heading;
                existingBlog.Author=blogPost.Author;
                existingBlog.Content=blogPost.Content;
                existingBlog.PageTitle=blogPost.PageTitle;
                existingBlog.ShortDescription=blogPost.ShortDescription;
                existingBlog.PublishedDate=blogPost.PublishedDate;
                existingBlog.FeaturedImageUrl=blogPost.FeaturedImageUrl;
                existingBlog.UrlHandle=blogPost.UrlHandle;
                existingBlog.Visible=blogPost.Visible;
                existingBlog.Tags=blogPost.Tags;

                await dbContext.SaveChangesAsync();
                return existingBlog;
            }
            else
            {
                return null;
            }
        }
    }
}
