using System.ComponentModel.DataAnnotations;

namespace SmartJobAssistant.APIS.DTOS
{
	public class LoginDTO
	{
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
