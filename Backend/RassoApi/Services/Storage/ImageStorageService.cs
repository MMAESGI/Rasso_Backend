using RassoApi.DTOs.Responses.Storage;
using System.Text.Json;

namespace RassoApi.Services.Storage
{
    public interface IImageStorageService
    {
        Task<ImageUploadResponse> UploadImageAsync(IFormFile file);
        Task<string> GetImageUrlAsync(string customName);
    }

    public class ImageStorageService : IImageStorageService
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseUrl;

        public ImageStorageService(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _baseUrl = configuration["ImageStorage:BaseUrl"];
        }

        public async Task<ImageUploadResponse> UploadImageAsync(IFormFile file)
        {
            try
            {
                using var content = new MultipartFormDataContent();
                using var fileStream = file.OpenReadStream();
                using var streamContent = new StreamContent(fileStream);
                
                content.Add(streamContent, "file", file.FileName);

                Console.WriteLine($"Uploading image: {file.FileName} to {_baseUrl}/upload");

                var response = await _httpClient.PostAsync($"{_baseUrl}/upload", content);
                
                if (!response.IsSuccessStatusCode)
                {
                    throw new HttpRequestException($"Erreur lors de l'upload de l'image: {response.StatusCode}");
                }

                var jsonResponse = await response.Content.ReadAsStringAsync();
                var uploadResponse = JsonSerializer.Deserialize<ImageUploadResponse>(jsonResponse, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

                return uploadResponse ?? throw new InvalidOperationException("Réponse d'upload invalide");
            }
            catch (HttpRequestException ex)
            {
                throw new InvalidOperationException($"Erreur lors de l'upload de l'image {file.FileName}: {ex.Message}", ex);
            }
            catch (TaskCanceledException ex)
            {
                throw new InvalidOperationException($"Timeout lors de l'upload de l'image {file.FileName}: {ex.Message}", ex);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Erreur inattendue lors de l'upload de l'image {file.FileName}: {ex.Message}", ex);
            }
        }

        public async Task<string> GetImageUrlAsync(string customName)
        {
            return $"{_baseUrl}/images/{customName}";
        }
    }
}
