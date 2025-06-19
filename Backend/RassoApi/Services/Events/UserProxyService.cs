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


        // TODO
        public async Task<UserDto?> GetUserByIdAsync(Guid userId)
        {
            try
            {
                HttpResponseMessage response = await _httpClient.GetAsync($"/api/users/{userId}");
                if (!response.IsSuccessStatusCode)
                {
                    _logger.LogWarning("Failed to fetch user {UserId} from Identity. Status: {StatusCode}", userId, response.StatusCode);
                    return null;
                }

                ApiResponse<UserDto>? apiResponse = await response.Content.ReadFromJsonAsync<ApiResponse<UserDto>>();
                return apiResponse?.Data;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exception when calling identity service for user {UserId}", userId);
                return null;
            }
        }


        // TODO
        public async Task<UserDto?> GetUserByEmail(string email)
        {
            try
            {
                HttpResponseMessage response = await _httpClient.GetAsync($"/api/users/{email}");
                if (!response.IsSuccessStatusCode)
                {
                    _logger.LogWarning("Failed to fetch user {email} from Identity. Status: {StatusCode}", email, response.StatusCode);
                    return null;
                }

                ApiResponse<UserDto>? apiResponse = await response.Content.ReadFromJsonAsync<ApiResponse<UserDto>>();
                return apiResponse?.Data;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exception when calling identity service for user {UserId}", email);
                return null;
            }
        }

        public Task<List<UserDto?>> GetUsersByIdsAsync(List<Guid> userIds)
        {
            // TODO récupérer l'ensemble des utilisateurs via leurs IDs
            throw new NotImplementedException();
        }
    }

}
