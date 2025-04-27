namespace SmartJobAssistant.APIS.Errors
{
	public class ApiResponse
	{
		public int StatusCode { get; set; }
		public string Message { get; set; }
		public ApiResponse(int statusCode, string? message = null)
		{
			StatusCode = statusCode;
			Message = message ?? GetDefulaultMessage(statusCode);
		}

		private string? GetDefulaultMessage(int statusCode)
		{
			return statusCode switch
			{
				400 => "A Bad Request You have make",
				401 => "Authoried ,you are  not ",
				404 => "Resource Not Found",
				500 => "Errors are the path to the dark side .Error let to angur",
				_ => null
			};
		}
	}
}
