using System.Diagnostics.CodeAnalysis;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ManageMe.Api.Controllers.DTOs.Output;

public class BaseApiResponse
{
    public required string Timestamp { get; set; }

    public required string Message { get; set; }

    public required Dictionary<string, dynamic> Data { get; set; }

    [SetsRequiredMembers]
    public BaseApiResponse(string timestamp, string message, Dictionary<string, dynamic> data)
    {
        Timestamp = timestamp;
        Message = message;
        Data = data;
    }

    public static BaseApiResponse WithData(string message, Dictionary<string, dynamic> data)
    {
        return new BaseApiResponse(
            DateTimeOffset.UtcNow.ToUnixTimeSeconds().ToString(),
            message,
            data
        );
    }

    public static BaseApiResponse OnlyMessage(string message)
    {
        return new BaseApiResponse(
            DateTimeOffset.UtcNow.ToUnixTimeSeconds().ToString(),
            message,
            null
        );
    }
}
