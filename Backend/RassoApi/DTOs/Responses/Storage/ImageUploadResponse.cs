namespace RassoApi.DTOs.Responses.Storage
{
    public class ImageUploadResponse
    {
        public string custom_name { get; set; } = string.Empty;
        public string filename { get; set; } = string.Empty;
        public string message { get; set; } = string.Empty;
        public string original_name { get; set; } = string.Empty;
    }
}
