using SmartJobAssistant.APIS.Errors;
using System.Net;
using System.Text.Json;

namespace SmartJobAssistant.APIS.Middlewares
{
	public class ExceptionMiddleware
	{
		private readonly RequestDelegate _Next;
		private readonly ILogger<ExceptionMiddleware> _Logger;
		private readonly IHostEnvironment _Env;

		public ExceptionMiddleware(RequestDelegate Next, ILogger<ExceptionMiddleware> logger, IHostEnvironment env)
		{
			_Next = Next;
			_Logger = logger;
			_Env = env;
		}
		public async Task Invoke(HttpContext context)
		{

			try
			{
				await _Next.Invoke(context);
			}
			catch (Exception ex)
			{
				_Logger.LogError(ex, ex.Message);
				context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
				context.Response.ContentType = "application/json";
				//body
				var response = _Env.IsDevelopment() ?
					new ApiExceptionResponse((int)HttpStatusCode.InternalServerError, ex.Message, ex.StackTrace.ToString()) :
					new ApiExceptionResponse((int)HttpStatusCode.InternalServerError, ex.Message);
				var options = new JsonSerializerOptions() { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };


				var json = JsonSerializer.Serialize(response, options);
				await context.Response.WriteAsync(json);
			}
		}
	}
}
