using System.ComponentModel.DataAnnotations;

namespace CodeBlog.Models.ViewModels
{
	public class RegisterViewModel
	{
		[Required]
        public string Username { get; set; }

		[Required]
		[EmailAddress]
		public string Email { get; set; }

		[Required]
		[MinLength(6,ErrorMessage ="Password has to be least 6 characters")]
		public string Password { get; set; }
	}
}
