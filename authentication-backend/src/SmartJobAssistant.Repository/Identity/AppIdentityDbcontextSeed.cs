using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using SmartJobAssistant.Core.Models.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartJobAssistant.Repository.Identity
{
	public static class AppIdentityDbcontextSeed
	{
		public static async Task SeedUserAsync(UserManager<AppUser> userManager)
		{
			if (!userManager.Users.Any())
			{
				var user = new AppUser()
				{
					DisplayNamr = "FaroukKhaled",
					Email = "elghriwfarouk@gmail.com",
					UserName = "Farouk.Elghriw",
					PhoneNumber = "01551273356"
				};
				await userManager.CreateAsync(user, "Pa$$w0rd");
			}
			
		}
	}
}
