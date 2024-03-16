namespace Totten.Solution.Ragstore.Infra.Cross.Errors;
using System;

public class ErrorPayload
{
    public required int ErrorCode { get; init; }
    public required string ErrorMessage { get; init; }

    private ErrorPayload() { }

    public static ErrorPayload New<T>(T exception) where T : Exception
    {
        int errorCode;

        if (exception is BusinessError error)
            errorCode = error.ErrorCode.GetHashCode();
        else
            errorCode = ECodeError.Unhandled.GetHashCode();

        return new ErrorPayload
        {
            ErrorCode = errorCode,
            ErrorMessage = exception.Message,
        };
    }
}