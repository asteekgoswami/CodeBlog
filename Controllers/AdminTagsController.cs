using CodeBlog.Data;
using CodeBlog.Models.Domain;
using CodeBlog.Models.ViewModels;
using CodeBlog.Repositories.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CodeBlog.Controllers
{
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
        public async Task<IActionResult> List()
        {
            var tags = await  tagRepository.GetAllAsync();
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
    }
}
