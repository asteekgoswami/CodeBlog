﻿using CodeBlog.Models.Domain;

namespace CodeBlog.Repositories.Interface
{
    public interface IBlogPostInterface
    {
        Task<IEnumerable<BlogPost>> GetAllAsync(string? searchQuery = null, string ?selectedDate=null,int pageSize=3,int pageNumber=1);

        Task<IEnumerable<BlogPost>> GetAllBlogsOfThisConditionAsync(string? searchQuery = null, string? selectedDate = null);
        
        Task<IEnumerable<BlogPost>> GetLimitedBlogAsync(string? searchQuery = null, string? tag = null, string? selectedDate = null);

        Task<BlogPost?> GetAsync(Guid id);

        Task<BlogPost?> GetByUrlHanldeAsync(string urlHandle);

        Task<BlogPost> AddAsync(BlogPost blogPost);

        Task<BlogPost?> UpdateAsync(BlogPost blogPost);

        Task<BlogPost?> DeleteAsync(Guid id);

        Task<IEnumerable<BlogPost>> GetAllByTagPageAsync(string? tag=null , string? serachQuery=null);

        Task<IEnumerable<BlogPost>> GetAllByTagPageAsyncWithPagination(string? tag, string? searchQuery, string? selectedDate = null, int pageSize = 3, int pageNumber = 1);

        Task<int> CountAllPostAsync();
    }
}
