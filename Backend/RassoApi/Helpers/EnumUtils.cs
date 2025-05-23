using RassoApi.Enums;

namespace RassoApi.Helpers
{
    public class EnumUtils
    {
        public static string ToStatusString(StatusEnum? status)
        {
            return status?.ToString() ?? "UNKNOWN";
        }
    }
}
