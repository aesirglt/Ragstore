namespace Totten.Solution.Ragstore.Infra.Cross.Errors.EspecifiedErrors;
using System;

public class InternalError : BusinessError
{
    public InternalError(string message) : base(errorCode: ECodeError.Unhandled, message)
    {

    }
    public InternalError(string message, Exception inner) : base(errorCode: ECodeError.Unhandled, message, inner)
    {

    }
}
