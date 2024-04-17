using CodeBlog.Data;
using CodeBlog.Models.Domain;
using CodeBlog.Repositories.Interface;
using Microsoft.EntityFrameworkCore;
using System.Linq;

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

        public async Task<int> CountAllPostAsync()
        {
            return await dbContext.BlogPosts.CountAsync();
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

        public async Task<IEnumerable<BlogPost>> GetAllAsync(string? searchQuery,string? selectedDate,int pageSize=3,int pageNumber=1)
        {
            var query = dbContext.BlogPosts.Include(x => x.Tags).AsQueryable();

            // Filtering
            if (!string.IsNullOrWhiteSpace(searchQuery))
            {
                query = query.Where(x => x.PageTitle.Contains(searchQuery) ||
                                         x.Heading.Contains(searchQuery) ||
                                         x.Content.Contains(searchQuery) ||
                                         x.Author.Contains(searchQuery) ||
                                         x.ShortDescription.Contains(searchQuery) ||
                                         x.UrlHandle.Contains(searchQuery));
            }

            if (!string.IsNullOrWhiteSpace(selectedDate))
            {
                query = query.Where(x => x.PublishedDate == selectedDate);
            }

            /*// For tag
            if (!string.IsNullOrWhiteSpace(tag))
            {
                query = query.Where(x => x.Tags.Any(t => t.Name == tag));
            }*/



            //pagination
            var skipResults = (pageNumber - 1) * pageSize;
            query= query.Skip(skipResults).Take(pageSize);

            query = query.OrderByDescending(x => x.PublishedDate);
            return await query.ToListAsync();
        }

        public async  Task<IEnumerable<BlogPost>> GetAllBlogsOfThisConditionAsync(string? searchQuery = null, string? selectedDate = null)
        {
            var query = dbContext.BlogPosts.Include(x => x.Tags).AsQueryable();

            // Filtering
            if (!string.IsNullOrWhiteSpace(searchQuery))
            {
                query = query.Where(x => x.PageTitle.Contains(searchQuery) ||
                                         x.Heading.Contains(searchQuery) ||
                                         x.Content.Contains(searchQuery) ||
                                         x.Author.Contains(searchQuery) ||
                                         x.ShortDescription.Contains(searchQuery) ||
                                         x.UrlHandle.Contains(searchQuery));
            }

            if (!string.IsNullOrWhiteSpace(selectedDate))
            {
                query = query.Where(x => x.PublishedDate == selectedDate);
            }


            query = query.OrderByDescending(x => x.PublishedDate);
            return await query.ToListAsync();
        }



        /// <summary>
        /// for the seperate page which coes after search by tag
        /// </summary>
        /// <param name="tag"></param>
        /// <param name="searchQuery"></param>
        /// <returns></returns>
        public async Task<IEnumerable<BlogPost>> GetAllByTagPageAsync(string? tag, string? searchQuery)
        {
            var query = dbContext.BlogPosts.Include(x => x.Tags).AsQueryable();

            if (!string.IsNullOrWhiteSpace(tag))
            {
                query = query.Where(x => x.Tags.Any(t => t.Name == tag));
            }

            //filtering
            if (string.IsNullOrWhiteSpace(searchQuery) == false)
            {
                query = query.Where(x => x.PageTitle.Contains(searchQuery) || x.Heading.Contains(searchQuery) || x.Content.Contains(searchQuery) || x.Author.Contains(searchQuery) || x.ShortDescription.Contains(searchQuery) || x.UrlHandle.Contains(searchQuery));

            }

            //reverse the list
            query = query.OrderByDescending(x => x.PublishedDate);
            return await query.ToListAsync();
        }



        public async Task<BlogPost?> GetAsync(Guid id)
        {
            return await dbContext.BlogPosts.Include(x=>x.Tags).FirstOrDefaultAsync(x=>x.Id==id);
        }

        public async Task<BlogPost?> GetByUrlHanldeAsync(string urlHandle)
        {
            return await dbContext.BlogPosts.Include(x=>x.Tags).FirstOrDefaultAsync(x=>x.UrlHandle==urlHandle);
        }

        public  async Task<IEnumerable<BlogPost>> GetLimitedBlogAsync(string? searchQuery = null, string? tag = null, string? selectedDate = null)
        {
            var query = dbContext.BlogPosts.Include(x => x.Tags).AsQueryable();

            // Filtering
            if (!string.IsNullOrWhiteSpace(searchQuery))
            {
                query = query.Where(x => x.PageTitle.Contains(searchQuery) ||
                                         x.Heading.Contains(searchQuery) ||
                                         x.Content.Contains(searchQuery) ||
                                         x.Author.Contains(searchQuery) ||
                                         x.ShortDescription.Contains(searchQuery) ||
                                         x.UrlHandle.Contains(searchQuery));
            }

            if (!string.IsNullOrWhiteSpace(selectedDate))
            {
                query = query.Where(x => x.PublishedDate == selectedDate);
            }

            // For tag
            if (!string.IsNullOrWhiteSpace(tag))
            {
                query = query.Where(x => x.Tags.Any(t => t.Name == tag));
            }

            query = query.OrderByDescending(x => x.PublishedDate).Take(6);
            return await query.ToListAsync();
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
