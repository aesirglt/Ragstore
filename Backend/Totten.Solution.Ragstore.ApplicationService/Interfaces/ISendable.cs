namespace Totten.Solution.Ragstore.ApplicationService.Interfaces;

public interface ISendable<SendableClass>
    where SendableClass : ISendable<SendableClass>
{
}
