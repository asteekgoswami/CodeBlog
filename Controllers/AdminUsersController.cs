using CodeBlog.Models.ViewModels;
using CodeBlog.Repositories.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CodeBlog.Controllers
{
	[Authorize(Roles ="Admin")]
	public class AdminUsersController : Controller
	{
		private readonly IUserRespository userRespository;
		private readonly UserManager<IdentityUser> userManager;

		public AdminUsersController(IUserRespository userRespository,UserManager<IdentityUser> userManager)
        {
			this.userRespository = userRespository;
			this.userManager = userManager;
		}

		[HttpGet]
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


		[HttpPost]
		public async Task<IActionResult> List(UserViewModel request)
		{
			var identityUser = new IdentityUser
			{
				UserName = request.Username,
				Email = request.Email,

			};

			var identityResult = await userManager.CreateAsync(identityUser,request.Password);

			if(identityResult is not null)
			{
				if(identityResult.Succeeded)
				{
					//assign roles to this user
					var roles = new List<string> { "User" };

					if (request.AdminRoleCheckbox)
					{
						roles.Add("Admin");
					}

					 identityResult = await userManager.AddToRolesAsync(identityUser, roles);


					if(identityResult is not null && identityResult.Succeeded)
					{
						return RedirectToAction("List", "AdminUsers");
					}
				}
			}
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> Delete(Guid id)
		{
			var user = await userManager.FindByIdAsync(id.ToString());
			if(user is not null)
			{
				var identityResult = await userManager.DeleteAsync(user);
				if(identityResult is not null)
				{
					return RedirectToAction("List", "AdminUsers");
				}
			}
			return View();
		}

	}
}
