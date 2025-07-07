using Identity.Client;
using RassoApi.DTOs;
using RassoApi.Mappers;
using RassoApi.Services.Events.Interfaces;

public class UserProxyService : IUserProxyService
{
    private readonly IdentityClient _identityClient;
    private readonly ILogger<UserProxyService> _logger;
    private readonly IUserMapper _userMapper;

    public UserProxyService(IdentityClient identityClient, ILogger<UserProxyService> logger, IUserMapper userMapper)
    {
        _identityClient = identityClient;
        _logger = logger;
        _userMapper = userMapper;
    }

    public async Task<RassoApi.DTOs.UserDto?> GetUserByIdAsync(Guid userId)
    {
        try
        {
            return _userMapper.UserIdentityToUser(await _identityClient.IdAsync(userId));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Exception when calling identity service for user {UserId}", userId);
            return null;
        }
    }

    public async Task<RassoApi.DTOs.UserDto?> GetUserByEmail(string email)
    {
        try
        {
            return _userMapper.UserIdentityToUser(await _identityClient.EmailAsync(email));

        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Exception when calling identity service for user {email}", email);
            return null;
        }
    }


    // Implémenter GetUsersByIdsAsync si disponible dans IdentityClient
}
