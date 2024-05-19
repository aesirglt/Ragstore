namespace Totten.Solution.Ragstore.ApplicationService.Interfaces;

using Totten.Solution.Ragstore.Infra.Cross.Functionals;

public interface IMessageService<SendableClass>
    : ISendable<SendableClass> where SendableClass : ISendable<SendableClass>
{
    Task<Result<Exception, Unit>> Send(SendableClass sendableClass);
}
