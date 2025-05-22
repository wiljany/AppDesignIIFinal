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

		//public byte[] ImageData { get; set; }
		[Key]
		public int Id { get; set; }

		public string ImageMimeType { get; set; }

		public byte[] FileData { get; set; }

		public string MimeType { get; set; }

		public string FileName { get; set; }

		public string FileType { get; set; }

		public DateTime UploadDate { get; set; } = DateTime.Now;

	}
}
