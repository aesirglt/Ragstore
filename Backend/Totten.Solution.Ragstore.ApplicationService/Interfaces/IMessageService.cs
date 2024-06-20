using FunctionalConcepts;
using FunctionalConcepts.Results;
namespace Totten.Solution.Ragstore.ApplicationService.Interfaces;

public interface IMessageService<SendableClass>
    : ISendable<SendableClass> where SendableClass : ISendable<SendableClass>
{
    Task<Result<Success>> Send(SendableClass sendableClass);
}
