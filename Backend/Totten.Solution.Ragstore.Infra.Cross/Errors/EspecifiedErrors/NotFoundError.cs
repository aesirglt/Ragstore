namespace Totten.Solution.Ragstore.Infra.Cross.Errors.EspecifiedErrors;
public class NotFoundError : BusinessError
{
    public NotFoundError(string message) : base(errorCode: ECodeError.NotFound, message)
    {

    }
}
