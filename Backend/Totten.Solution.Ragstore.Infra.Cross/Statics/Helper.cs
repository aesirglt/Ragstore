namespace Totten.Solution.Ragstore.Infra.Cross.Statics
{
    public static class Helper
    {
        public static Task<T> AsTask<T>(this T obj) => Task.FromResult(obj);
        public static TOut Apply<TIn, TOut>(this TIn obj, Func<TIn, TOut> act) => act(obj);
    }
}
