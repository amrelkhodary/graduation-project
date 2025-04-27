using Microsoft.AspNetCore.Mvc;
using SmartJobAssistant.APIS.Errors;

namespace SmartJobAssistant.APIS.Extensions
{
	public static class AddApplicationServices
	{
		public static IServiceCollection AddAppServices(this IServiceCollection services)
		{
			services.Configure<ApiBehaviorOptions>(options =>
			{
				options.InvalidModelStateResponseFactory = (actioncontext) =>
				{
					var errors = actioncontext.ModelState.Where(P => P.Value.Errors.Count() > 0)// check if we have a error
														 .SelectMany(P => P.Value.Errors)  // array of arrays
														 .Select(P => P.ErrorMessage).ToArray(); // array of errormesgae
					var response = new ApiValdiationErrorResponse()
					{
						Errors = errors
					};
					return new BadRequestObjectResult(response);

				};

			});
			return services;
		}
	}
}
