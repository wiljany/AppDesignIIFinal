﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace AppDesignFinal.Helpers
{
	public static class PasswordHelper
	{
		public static string HashPassword(string password)
		{
			using (var sha = SHA256.Create())
			{
				var bytes = Encoding.UTF8.GetBytes(password);
				var hash = sha.ComputeHash(bytes);
				return Convert.ToBase64String(hash);
			}
		}
	}
}