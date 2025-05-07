namespace Totten.Solution.RagnaComercio.ApplicationService.Interfaces;

public interface ISendable<SendableClass>
    where SendableClass : ISendable<SendableClass>
{
}
