using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace AppDomain.Entities
{
	public class UploadFile
	{
		[HiddenInput(DisplayValue = false)]
		public int Id { get; set; }

		[Required(ErrorMessage = "Please enter a first name")]
		public string fName { get; set; }

		[Required(ErrorMessage = "Please enter a last name")]
		public string lName { get; set; }

		[Required(ErrorMessage = "Please enter an email")]
		public string Email { get; set; }

		[Required(ErrorMessage = "Please enter a phone number")]
		public string Phone { get; set; }

		//public byte[] ImageData { get; set; }

		//public string ImageMimeType { get; set; }

		public byte[] FileData { get; set; }

		public string MimeType { get; set; }

		public string FileName { get; set; }

		public string FileType { get; set; }

		public DateTime UploadDate { get; set; } = DateTime.Now;

		public string UploadedByUserId { get; set; }
	}
}
