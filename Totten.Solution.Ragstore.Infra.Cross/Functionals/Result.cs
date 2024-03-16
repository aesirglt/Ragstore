namespace Totten.Solution.Ragstore.Infra.Cross.Functionals;
using System;

public readonly struct Result<TErr, TValue>
    where TErr : Exception
{
    private readonly bool _success;
    public readonly TValue? _value;
    public readonly TErr? _err;

    private Result(TValue? v, TErr? e, bool success)
    {
        _value = v;
        _err = e;
        _success = success;
    }

    public bool IsOk => _success;

    public R Match<R>(Func<TValue?, R> success, Func<TErr?, R> failure)
        => _success ? success(_value) : failure(_err);

    public Unit IfFail(Action<TErr> func)
    {
        if (!_success)
            func(_err);

        return new Unit();
    }
    public static Result<TErr, TValue> Ok(TValue v)
        => new (v, default, true);

    public static Result<TErr, TValue> Err(TErr e)
        =>  new(default, e, false);

    public static implicit operator Result<TErr, TValue>(TValue value) => Ok(value);
    public static implicit operator Result<TErr, TValue>(TErr e) => Err(e);
}
