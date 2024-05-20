namespace Totten.Solution.Ragstore.Domain.Features.CallbackAggregation;
public enum ECallbackType
{
    None,
    VIP1,
    VIP2,
    AGENT,
    SYSTEM
}

public static class CallbackTypeExp
{
    public static int GetMinutesToSendMessage(this ECallbackType type) => type switch
    {
        ECallbackType.None => 60,
        ECallbackType.VIP1 => 20,
        ECallbackType.VIP2 => 10,
        ECallbackType.AGENT => 1,
        ECallbackType.SYSTEM => 1
    };
}