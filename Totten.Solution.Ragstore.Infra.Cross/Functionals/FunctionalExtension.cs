namespace Totten.Solution.Ragstore.Infra.Cross.Functionals;
using System;
using Totten.Solution.Ragstore.Infra.Cross.Errors;

public static class FunctionalExtension
{
    public static TResult Apply<TResult, TInput>(this TInput input, Func<TInput, TResult> func)
        => func(input);
}
