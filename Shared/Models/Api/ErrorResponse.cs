namespace Shared.Models.Api
{
    public class ErrorResponse
    {
        public List<ApiError> Errors { get; set; } = new List<ApiError>();
    }
}
