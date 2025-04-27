namespace SmartJobAssistant.APIS.Errors
{
	public class ApiValdiationErrorResponse:ApiResponse
	{
		public IEnumerable<string> Errors { get; set; }
		public ApiValdiationErrorResponse() : base(400)
		{
			Errors = new List<string>();
		}
	}
}
