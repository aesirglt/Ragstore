namespace Totten.Solution.Ragstore.Infra.Cross.Errors.EspecifiedErrors;
public class InvalidObjectError : BusinessError
{
    public InvalidObjectError(string message) : base(errorCode: ECodeError.InvalidObject, message)
    {

    }
}
