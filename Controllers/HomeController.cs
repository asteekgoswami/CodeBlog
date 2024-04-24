using Azure;
using CodeBlog.Models;
using CodeBlog.Models.Domain;
using CodeBlog.Models.ViewModels;
using CodeBlog.Repositories.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.Diagnostics;

namespace CodeBlog.Controllers
{
    
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IBlogPostInterface blogPostReposiory;
		private readonly ITagInterface tagRepository;

		public HomeController(ILogger<HomeController> logger , IBlogPostInterface blogPostReposiory,ITagInterface tagRepository)
        {
            _logger = logger;
            this.blogPostReposiory = blogPostReposiory;
			this.tagRepository = tagRepository;
		}


        [HttpGet]
        public async Task<IActionResult> Index(string ? searchQuery , string ? tag , string? selectedDate)
        {
            ViewBag.SearchQuery = searchQuery;
            ViewBag.Tag = tag;
            ViewBag.SelectedDate = selectedDate;



            //getting all blogs
            var blogPosts = await blogPostReposiory.GetLimitedBlogAsync(searchQuery,tag,selectedDate);

            //getting all tags
            var tags = await tagRepository.GetAllAsync();

            var model = new HomeViewModel
            {
                BlogPosts = blogPosts,
                Tags = tags
            };
            return View(model);
        }



        /// <summary>
        /// seperate page for the search by tag
        /// </summary>
        /// <param name="tag"></param>
        /// <param name="searchQuery"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> SearchByTag(string ? tag, string? searchQuery, string? selectedDate = null, int pageSize = 2, int pageNumber = 1)
        {
            ViewBag.Tag = tag;
            //getting all blogs
            var blogPosts = await blogPostReposiory.GetAllByTagPageAsync( tag, searchQuery);

            var totalPosts = blogPosts.Count();

            var totalPages = Math.Ceiling((decimal)totalPosts / pageSize);

            //adding data to view bag
            ViewBag.PageNumber = pageNumber;
            ViewBag.TotalPages = totalPages;
            ViewBag.PageSize = pageSize;
            ViewBag.SearchQuery = searchQuery;
            ViewBag.SelectedDate = selectedDate;

            if (pageNumber > totalPages)
            {
                pageNumber--;
            }

            if (pageNumber < 1)
            {
                pageNumber++;
            }

            var finalBlogPostResult = await blogPostReposiory.GetAllByTagPageAsyncWithPagination(tag,searchQuery, selectedDate,pageSize,pageNumber);

            return View(finalBlogPostResult);

        }



        /// <summary>
        /// Display All the Blogs We have
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> AllBlogs(string? searchQuery = null, string? selectedDate = null, int pageSize=3, int pageNumber=1)
        {
            //getting blogs to calculate the total pages based on this condition
            var allPosts = await blogPostReposiory.GetAllBlogsOfThisConditionAsync(searchQuery, selectedDate);

            var totalPosts = allPosts.Count();

            var totalPages = Math.Ceiling((decimal)totalPosts / pageSize);

            //adding data to view bag
            ViewBag.PageNumber = pageNumber;
            ViewBag.TotalPages = totalPages;
            ViewBag.PageSize = pageSize;
            ViewBag.SearchQuery = searchQuery;
            ViewBag.SelectedDate = selectedDate;
            

            if (pageNumber > totalPages)
            {
                pageNumber--;
            }

            if (pageNumber < 1)
            {
                pageNumber++;
            }

            
            //getting all blogs
            var blogPosts = await blogPostReposiory.GetAllAsync(searchQuery, selectedDate, pageSize, pageNumber);

            //getting all tags
            var tags = await tagRepository.GetAllAsync();

            var model = new HomeViewModel
            {
                BlogPosts = blogPosts,
                Tags = tags
            };
            return View(model);
        }




       

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
