namespace AuthandAuthrizations.Models.DTO
{
    public class ApiResponse
    {
        public int StatusCode { get; set; }
        public bool Status { get; set; } = false;
        public object Message { get; set; }
        public string?Errors { get; set; }
    }
}
