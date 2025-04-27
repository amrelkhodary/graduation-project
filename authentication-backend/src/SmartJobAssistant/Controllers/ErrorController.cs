using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SmartJobAssistant.APIS.Errors;

namespace SmartJobAssistant.APIS.Controllers
{
	[Route("errors/{code}")]
	[ApiExplorerSettings(IgnoreApi = true)]
	[ApiController]
	public class ErrorController : BaseApiController
	{
		public ActionResult Error(int code)
		{
			return NotFound(new ApiResponse(code));
		}
	}
}
