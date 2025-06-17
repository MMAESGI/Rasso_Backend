namespace RassoApi.DTOs.Responses.User
{
    public class ModeratorResponse : UserResponse
    {
        public ModeratorResponse()
        {
            UserName ??= "Modérateur";
        }
    }
}
