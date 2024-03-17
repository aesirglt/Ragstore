namespace Totten.Solution.Ragstore.Infra.Cross.Errors;
using System;

public class ErrorPayload
{
    public required int ErrorCode { get; init; }
    public required string ErrorMessage { get; init; }

    private ErrorPayload() { }

    public static ErrorPayload New<T>(T exception) where T : Exception
        => new()
        {
            ErrorCode = exception is BusinessError error
                        ? error.ErrorCode.GetHashCode()
                        : ECodeError.Unhandled.GetHashCode(),
            ErrorMessage = exception.Message,
        };
}