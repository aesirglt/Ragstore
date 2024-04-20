namespace Totten.Solution.Ragstore.Infra.Cross.Errors;
using System;

public class BusinessError : Exception
{
    public ECodeError ErrorCode { get; }

    public BusinessError(ECodeError errorCode, string message) : base(message)
    {
        ErrorCode = errorCode;
    }

    public BusinessError(ECodeError errorCode, string message, Exception inner) : base(message, inner)
    {
        ErrorCode = errorCode;
    }
}
