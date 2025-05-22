using System.ComponentModel.DataAnnotations;

namespace AppDesignFinal.Models
{
	public class Users
	{
		[Key]
		public int Id { get; set; }

		public string fName { get; set; }
		public string lName { get; set; }

		[Required]
		public string Email { get; set; }

		[Required]
		public string Password { get; set; }

		public string Phone { get; set; }
		public string fileInput { get; set; }
	}
}
