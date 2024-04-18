using CodeBlog.Repositories.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace CodeBlog.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImagesController : ControllerBase
    {
        private readonly IImageInterface iImageRepositor;

        public ImagesController(IImageInterface iImageRepositor)
        {
            this.iImageRepositor = iImageRepositor;
        }

        [HttpPost]
        public async Task<IActionResult> UploadAsync(IFormFile file)
        {
            // call a repository
            var imageyUrl = await iImageRepositor.UploadAsync(file);
            if(imageyUrl == null)
            {
                return Problem("Image Upload Not Successfull", null, (int)HttpStatusCode.InternalServerError);
            }
            return new JsonResult(new {link =imageyUrl});

        }

    }
} 
