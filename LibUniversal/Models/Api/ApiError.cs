namespace LibUniversal.Models.Api
{
    public class ApiError
    {
        public string? ErrorCode { get; set; }

        public string? Details { get; set; }

        public Dictionary<string, List<string>>? Conflicts { get; set; }
    }
}
