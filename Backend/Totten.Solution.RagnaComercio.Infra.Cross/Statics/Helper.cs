namespace Totten.Solution.RagnaComercio.Infra.Cross.Statics
{
    public static class Helper
    {
        public static Task<T> AsTask<T>(this T obj, CancellationToken cancellationToken = default)
            => Task.Run(() => obj, cancellationToken);
        public static TOut Apply<TIn, TOut>(this TIn obj, Func<TIn, TOut> act) => act(obj);
    }
}
