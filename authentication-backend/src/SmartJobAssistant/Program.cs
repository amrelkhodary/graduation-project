
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SmartJobAssistant.APIS.Extensions;
using SmartJobAssistant.APIS.Middlewares;
using SmartJobAssistant.Core.Models.Identity;
using SmartJobAssistant.Repository.Identity;

namespace SmartJobAssistant
{
	public class Program
	{
		public static async Task Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);

			// Add services to the container.

			builder.Services.AddControllers();
			// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
			builder.Services.AddEndpointsApiExplorer();
			builder.Services.AddSwaggerGen();
			builder.Services.AddAppServices();
			#region IdentityServices

			builder.Services.AddDbContext<StoreIdentityDbContext>(options =>
			{
				options.UseSqlServer(builder.Configuration.GetConnectionString("identityConnection"));
			});
			builder.Services.AddIdentityServices(builder.Configuration);
			
			#endregion
			

			var app = builder.Build();

			#region Update DataBase
			using var Scope = app.Services.CreateScope();
			var Services = Scope.ServiceProvider;
			var LoggerFactory = Services.GetRequiredService<ILoggerFactory>();
			try
			{
				
				var IdentityContext = Services.GetRequiredService<StoreIdentityDbContext>();
				await IdentityContext.Database.MigrateAsync();
				var userManger = Services.GetRequiredService<UserManager<AppUser>>();
				await AppIdentityDbcontextSeed.SeedUserAsync(userManger);
				

			}
			catch (Exception ex)
			{
				var logger = LoggerFactory.CreateLogger<Program>();
				logger.LogError(ex, "an Error Occerd when we add Migrations");

			}

			#endregion

			// Configure the HTTP request pipeline.
			app.UseMiddleware<ExceptionMiddleware>();
			if (app.Environment.IsDevelopment())
			{
				app.UseSwagger();
				app.UseSwaggerUI();
			}
			app.UseStatusCodePagesWithReExecute("/errors/{0}");

			app.UseHttpsRedirection();

			app.UseAuthorization();
			app.UseAuthentication();


			app.MapControllers();

			app.Run();
		}
	}
}
