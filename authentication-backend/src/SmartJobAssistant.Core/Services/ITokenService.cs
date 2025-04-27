using Microsoft.AspNetCore.Identity;
using SmartJobAssistant.Core.Models.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartJobAssistant.Core.Services
{
	public interface ITokenService
	{
		Task<string> CreateTokenAsync(AppUser user, UserManager<AppUser> userManager);
	}
}
