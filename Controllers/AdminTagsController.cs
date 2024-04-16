using CodeBlog.Data;
using CodeBlog.Models.Domain;
using CodeBlog.Models.ViewModels;
using CodeBlog.Repositories.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CodeBlog.Controllers
{
	[Authorize(Roles = "Admin")]
	public class AdminTagsController : Controller
    {
        private  readonly ITagInterface tagRepository;

        public AdminTagsController(ITagInterface tagRepository)
        {
                this.tagRepository = tagRepository;
        }


        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult>  Add(AddTagRequest addTagRequest)
        {

            ValidateAddTagRequest(addTagRequest);
           if(ModelState.IsValid)
            {
                //Mapping AddTagRequest to Tag Domain Model
                var tag = new Tag
                {
                    Name = addTagRequest.Name,
                    DisplayName = addTagRequest.DisplayName,
                };

                var result = await tagRepository.AddAsync(tag);
                return RedirectToAction("List");
            }
            else
            {
                return View("Add",addTagRequest);
            }
        }


        [HttpGet]
        public async Task<IActionResult> List(string? searchQuery, string ? shortBy, string? sortDirection, int pageSize=2, int pageNumber=1)
        {
            var totalRecords = await tagRepository.CountAsync();
            var totalPages = Math.Ceiling((decimal)totalRecords / pageSize);

            if (pageNumber > totalPages)
            {
                pageNumber--;
            }

            if (pageNumber < 1)
            {
                pageNumber++;
            }

            //to remain the searchquery in the search input after searching
            ViewBag.SearchQuery = searchQuery;
            ViewBag.ShortBy = shortBy;
            ViewBag.SortDirection = sortDirection;
            ViewBag.TotalPages = totalPages;
            ViewBag.PageSize = pageSize;
            ViewBag.PageNumber = pageNumber;

            var tags = await  tagRepository.GetAllAsync(searchQuery,shortBy,sortDirection, pageSize, pageNumber);
            return View(tags);
        }


        [HttpGet]
        //name of input parameter id is matched with the route parameter i.e asp-route-id
        public async Task<IActionResult> Edit(Guid id)
        {
            var tag = await tagRepository.GetAsync(id);
            if(tag!=null)
            {
                var editTagRequest = new EditTagRequest
                {
                    Id = id,
                    Name = tag.Name,
                    DisplayName = tag.DisplayName,
                };
                return View(editTagRequest);
            }
            return View(null) ;
        }


        [HttpPost]
        public async Task<IActionResult> Edit(EditTagRequest editTagRequest)
        {
            var tag = new Tag
            {
                Id = editTagRequest.Id,
                Name = editTagRequest.Name,
                DisplayName = editTagRequest.DisplayName,
            };


            var updatedTag = await tagRepository.UpdateAsync(tag);
            if (updatedTag != null)
            {
                //show success notification
                return RedirectToAction("List");
            }
            else
            {
                //show error notification
                return RedirectToAction("Edit", new { id = editTagRequest.Id });
            }
        }

        [HttpPost]
        public async Task<IActionResult> Delete(EditTagRequest editTagRequest)
        {
            var deletedTag = await tagRepository.DeleteAsync(editTagRequest.Id);
            if (deletedTag != null)
            {
                //show success notification
                return RedirectToAction("List");
            }
            else
            {
                //show error notification
                return RedirectToAction("Edit", new { id = editTagRequest.Id });
            }
        }



        /// <summary>
        /// Custom Validation method to validate the name and display name of the tag model 
        /// then add the validation message
        /// </summary>
        /// <param name="addTagRequest"></param>
        private void ValidateAddTagRequest(AddTagRequest addTagRequest)
        {
            if(addTagRequest.Name is not null && addTagRequest.DisplayName is not null)
            {
                if(addTagRequest.Name==addTagRequest.DisplayName)
                {
                    ModelState.AddModelError("DisplayName", "Name and DisplayName cannot be same");
                }
            }
        }

	}
}
