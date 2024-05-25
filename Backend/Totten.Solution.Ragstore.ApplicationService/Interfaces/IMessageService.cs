namespace Totten.Solution.Ragstore.ApplicationService.Interfaces;

using LanguageExt;
using LanguageExt.Common;

public interface IMessageService<SendableClass>
    : ISendable<SendableClass> where SendableClass : ISendable<SendableClass>
{
    Task<Result<Unit>> Send(SendableClass sendableClass);
}
