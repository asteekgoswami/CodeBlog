using CodeBlog.Models.Domain;
using CodeBlog.Models.ViewModels;
using CodeBlog.Repositories.Implementation;
using CodeBlog.Repositories.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;


namespace CodeBlog.Controllers
{
    [Authorize(Roles ="Admin")]
    public class AdminBlogPostsController : Controller
    {
        private readonly ITagInterface tagRepository;
        private readonly IBlogPostInterface blogPostRepository;

        public AdminBlogPostsController(ITagInterface tagRepository,IBlogPostInterface blogPostRepository)
        {
            this.tagRepository = tagRepository;
            this.blogPostRepository = blogPostRepository;
        }


        [HttpGet]
        public async  Task<IActionResult> Add()
        {
            //get tags from repository
            var tags = await tagRepository.GetAllAsync();
            var model = new AddBlogPostRequest
            {
                Tags = tags.Select(x => new SelectListItem { Text = x.Name, Value = x.Id.ToString() })
            };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddBlogPostRequest addBlogPostRequest)
        {
            
                //map to view model to domain model
                var blogPost = new BlogPost
                {
                    Heading = addBlogPostRequest.Heading,
                    PageTitle = addBlogPostRequest.PageTitle,
                    Content = addBlogPostRequest.Content,
                    ShortDescription = addBlogPostRequest.ShortDescription,
                    FeaturedImageUrl = addBlogPostRequest.FeaturedImageUrl,
                    UrlHandle = addBlogPostRequest.UrlHandle,
                    PublishedDate = addBlogPostRequest.PublishedDate,
                    Author = addBlogPostRequest.Author,
                    Visible = addBlogPostRequest.Visible,

                };

                // map tags from the selected tags
                var selectedTags = new List<Tag>();
                foreach (var selectedTagsId in addBlogPostRequest.SelectedTags)
                {
                    var selectedTagAsGuid = Guid.Parse(selectedTagsId);
                    var existingTag = await tagRepository.GetAsync(selectedTagAsGuid);
                    if (existingTag != null)
                    {
                        selectedTags.Add(existingTag);
                    }
                }

                //maping tags back to domain model
                blogPost.Tags = selectedTags;

                await blogPostRepository.AddAsync(blogPost);
                return RedirectToAction("List");
            
        }


        [HttpGet]
        public async Task<IActionResult> List(string? searchQuery = null, string? selectedDate = null, int pageSize = 3, int pageNumber = 1)
        {
			//getting blogs to calculate the total pages based on this condition
			var allPosts = await blogPostRepository.GetAllBlogsOfThisConditionAsync(searchQuery, selectedDate);

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
			var blogPosts = await blogPostRepository.GetAllAsync(searchQuery, selectedDate, pageSize, pageNumber);
            return View(blogPosts);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(Guid Id)
        {
            //retrieve result from the repo
            var blogPost = await blogPostRepository.GetAsync(Id);

            var tagsDomainModel = await tagRepository.GetAllAsync();

           if(blogPost != null)
            {
                //map the domain model to view model
                var model = new EditBlogPostRequest
                {
                    Id = blogPost.Id,
                    Heading = blogPost.Heading,
                    PageTitle = blogPost.PageTitle,
                    Content = blogPost.Content,
                    Author = blogPost.Author,
                    FeaturedImageUrl = blogPost.FeaturedImageUrl,
                    UrlHandle = blogPost.UrlHandle,
                    ShortDescription = blogPost.ShortDescription,
                    PublishedDate = blogPost.PublishedDate,
                    Visible = blogPost.Visible,
                    Tags = tagsDomainModel.Select(x => new SelectListItem { Text = x.Name, Value = x.Id.ToString() }),
                    SelectedTags = blogPost.Tags.Select(x => x.Id.ToString()).ToArray(),
                };

                //pass data to view
                return View(model);
            }
            else
            {
                return View(null);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EditBlogPostRequest editBlogPostRequest)
        {
            //map view model back to domain model
            var blogPostDomainModel = new BlogPost
            {
                Id = editBlogPostRequest.Id,
                Heading = editBlogPostRequest.Heading,
                PageTitle = editBlogPostRequest.PageTitle,
                Content = editBlogPostRequest.Content,
                Author = editBlogPostRequest.Author,
                ShortDescription = editBlogPostRequest.ShortDescription,
                FeaturedImageUrl = editBlogPostRequest.FeaturedImageUrl,
                PublishedDate = editBlogPostRequest.PublishedDate,
                UrlHandle = editBlogPostRequest.UrlHandle,
                Visible = editBlogPostRequest.Visible,
            };
            //map tags into domain model
            var selectedTags = new List<Tag>();
            foreach(var selectedTag in  editBlogPostRequest.SelectedTags)
            {
                if(Guid.TryParse(selectedTag, out var tag))
                {
                    var found = await tagRepository.GetAsync(tag);
                    if (found != null)
                    {
                        selectedTags.Add(found);
                    }
                }
            }
            blogPostDomainModel.Tags = selectedTags;

            //submit info to repo to update
            var updatedBlog= await blogPostRepository.UpdateAsync(blogPostDomainModel);
            
            if(updatedBlog != null)
            {

                //redirect to get method
                //show success notification
                return RedirectToAction("Edit");
            }
            else
            {
                //show failure notification
                return RedirectToAction("Edit");
            }

        }

        [HttpPost]
        public async Task<IActionResult> Delete (EditBlogPostRequest editBlogPostRequest)
        {
            //talk to repo to delete this blog and tags
            var deletedBlogPost = await blogPostRepository.DeleteAsync(editBlogPostRequest.Id);
            if(deletedBlogPost!=null)
            {
                //show success notification 
                //return to list page
                return RedirectToAction("List");

                //display the reposne
            }
            else
            {
                //show error notification
                return RedirectToAction("Edit", new { id = editBlogPostRequest.Id });
            }

        }
    }
}
