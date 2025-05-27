using RassoApi.DTOs;
using RassoApi.Helpers.Api;
using RassoApi.Services.Events.Interfaces;

namespace RassoApi.Services.Events
{
    public class UserProxyService : IUserProxyService
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<UserProxyService> _logger;

        public UserProxyService(HttpClient httpClient, ILogger<UserProxyService> logger)
        {
            _httpClient = httpClient;
            _logger = logger;
        }

        public async Task<UserDto?> GetUserByIdAsync(Guid userId)
        {
            try
            {
                var response = await _httpClient.GetAsync($"/api/users/{userId}");
                if (!response.IsSuccessStatusCode)
                {
                    _logger.LogWarning("Failed to fetch user {UserId} from Identity. Status: {StatusCode}", userId, response.StatusCode);
                    return null;
                }

                var apiResponse = await response.Content.ReadFromJsonAsync<ApiResponse<UserDto>>();
                return apiResponse?.Data;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exception when calling identity service for user {UserId}", userId);
                return null;
            }
        }
    }

}
