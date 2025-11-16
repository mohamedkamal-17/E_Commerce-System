namespace E_commerceManagementSystem.BLL.DTOs.GeneralResponseDto
{
    public class GeneralRespons
    {
        public bool Success { get; set; } = false;
        public string Message { get; set; }
        public List<string>? Errors { get; set; }
        public object? Model { get; set; }
        public int StatusCode { get; set; }


    }
}
