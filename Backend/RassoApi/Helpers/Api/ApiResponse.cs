namespace RassoApi.Helpers.Api
{
    /// <summary>
    /// Réponse de l'Api formatté
    /// </summary>
    /// <typeparam name="T">Type de donnée</typeparam>
    public class ApiResponse<T>
    {
        public bool Success { get; set; }

        public string? Message { get; set; }

        public T? Data { get; set; }

        public List<ValidationError>? Errors { get; set; }


        public static ApiResponse<T> SuccessResponse(T data, string? message = null)
            => new ApiResponse<T> { Success = true, Data = data, Message = message };


        public static ApiResponse<T> FailureResponse(List<ValidationError> errors, string? message = null)
            => new ApiResponse<T> { Success = false, Errors = errors, Message = message };


        public static ApiResponse<T> FailureResponse(string message)
            => new ApiResponse<T> { Success = false, Message = message };
    }
}
