using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using SmartJobAssistant.Core.Models.Identity;
using SmartJobAssistant.Core.Services;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace SmartJobAssistant.Services
{
	public class TokenService : ITokenService
	{
		private readonly IConfiguration _configuration;

		public TokenService(IConfiguration configuration)
		{
			_configuration = configuration;
		}
		public async Task<string> CreateTokenAsync(AppUser user, UserManager<AppUser> userManager)
		{
			// payloads=>{RegisterDeclaimed, PrivateClaims}
			// private Claims
			var authClaims = new List<Claim>()
			{
				new Claim(ClaimTypes.GivenName, user.UserName),
				new Claim(ClaimTypes.Email, user.Email)
			};
			var userRoles = await userManager.GetRolesAsync(user);
			foreach (var role in userRoles)
				authClaims.Add(new Claim(ClaimTypes.Role, role));
			// Key => AppSetting 
			var authKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
			// RegisterDeclaimd in appseeting 
			var Token = new JwtSecurityToken(
				issuer: _configuration["Jwt:VaildIssuer"],
				audience: _configuration["Jwt:VaildAudience"],
				expires: DateTime.Now.AddDays(double.Parse(_configuration["Jwt:DurationDays"])),
				claims: authClaims,
			  signingCredentials: new SigningCredentials(authKey, SecurityAlgorithms.HmacSha256Signature)
				);
			return new JwtSecurityTokenHandler().WriteToken(Token);

		}
	}
}
