using CodeBlog.Models.ViewModels;
using CodeBlog.Repositories.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CodeBlog.Controllers
{
	[Authorize(Roles ="Admin")]
	public class AdminUsersController : Controller
	{
		private readonly IUserRespository userRespository;

		public AdminUsersController(IUserRespository userRespository)
        {
			this.userRespository = userRespository;
		}
        public async Task<IActionResult> List()
		{
			var users = await userRespository.GetAll();

			var userViewModel = new UserViewModel();
			userViewModel.Users = new List<User>();

			foreach(var user in users)
			{
				userViewModel.Users.Add(new Models.ViewModels.User
				{
					Id = Guid.Parse(user.Id),
					EmailAddress = user.Email,
					Username = user.UserName,
				});
			}


			return View(userViewModel);
		}
	}
}
