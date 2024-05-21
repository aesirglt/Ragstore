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
        ECallbackType.None => 30,
        ECallbackType.VIP1 => 10,
        ECallbackType.VIP2 => 5,
        ECallbackType.AGENT => 1,
        ECallbackType.SYSTEM => 1
    };
}

public enum EStoreCallbackType
{
    None = 0,
    BuyingStore,
    VendingStore
}