using CodeBlog.Models;
using CodeBlog.Models.ViewModels;
using CodeBlog.Repositories.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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
        public async Task<IActionResult> Index(string ? searchQuery , string ? tag)
        {
            ViewBag.SearchQuery = searchQuery;
            ViewBag.Tag = tag;


            //getting all blogs
            var blogPosts = await blogPostReposiory.GetAllAsync(searchQuery,tag);

            //getting all tags
            var tags = await tagRepository.GetAllAsync();

            var model = new HomeViewModel
            {
                BlogPosts = blogPosts,
                Tags = tags
            };
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> SearchByTag(string ? tag, string? searchQuery)
        {
            ViewBag.SearchQuery = searchQuery;
            ViewBag.Tag = tag;
            //getting all blogs
            var blogPosts = await blogPostReposiory.GetAllByTagPageAsync( tag, searchQuery);

            return View(blogPosts);


        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
