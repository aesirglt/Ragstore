namespace Totten.Solution.RagnaComercio.ApplicationService.Interfaces;
using FunctionalConcepts;
using FunctionalConcepts.Results;

public interface IMessageService<SendableClass>
    : ISendable<SendableClass> where SendableClass : ISendable<SendableClass>
{
    Task<Result<Success>> Send(SendableClass sendableClass);
}
