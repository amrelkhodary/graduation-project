using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using SmartJobAssistant.Core.Models.Identity;
using SmartJobAssistant.Core.Services;
using SmartJobAssistant.Repository.Identity;
using SmartJobAssistant.Services;
using System.Text;

namespace SmartJobAssistant.APIS.Extensions
{
	public static class IdentityServicesExtension
	{
		public static IServiceCollection AddIdentityServices(this IServiceCollection services, IConfiguration configuration)
		{
			services.AddScoped(typeof(ITokenService), typeof(TokenService));
			services.AddIdentity<AppUser, IdentityRole>(options =>
			{
				options.Password.RequireDigit = true;
				options.Password.RequireUppercase = true;
				options.Password.RequireNonAlphanumeric = true;
				options.Password.RequireLowercase = true;
			})
				.AddEntityFrameworkStores<StoreIdentityDbContext>();

			services.AddAuthentication( //options =>
										//{
										//    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
										//    options.DefaultChallengeScheme= JwtBearerDefaults.AuthenticationScheme;
										//}
		  JwtBearerDefaults.AuthenticationScheme)
			  .AddJwtBearer(options =>
			  {
				  options.TokenValidationParameters = new TokenValidationParameters()
				  {
					  ValidateIssuer = true,
					  ValidIssuer = configuration["Jwt:VaildIssuer"],
					  ValidateAudience = true,
					  ValidAudience = configuration["Jwt:VaildAudience"],
					  ValidateLifetime = true,
					  ValidateIssuerSigningKey = true,
					  IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"])),

				  };

			  });

			return services;

		}
	}
}
