﻿using System.ComponentModel.DataAnnotations;

namespace SmartJobAssistant.APIS.DTOS
{
	public class RegisterDTO
	{
		[Required]
        public string DisplayName { get; set; }
		[Required]
		[EmailAddress]
        public string Email { get; set; }
		[Required]
		[Phone]
        public  string PhoneNumber { get; set; }
		[Required]
        public string Password { get; set; }
    }
}
