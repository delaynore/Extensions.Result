using System.Numerics;

namespace Extensions.Result;

public readonly struct ValueOption<T> : IEquatable<ValueOption<T>>, IEqualityOperators<ValueOption<T>, ValueOption<T>, bool> 
	where T : struct
{
	private readonly T? _value;

	internal ValueOption(T? value) => _value = value;

	public static ValueOption<T> Some(T value)
	{
		ArgumentNullException.ThrowIfNull(value, nameof(value));

		return new ValueOption<T>(value);
	}

	public static ValueOption<T> None() => new(default);

	public Option<TResult> Map<TResult>(Func<T, TResult> map) where TResult : class
		=> _value is null
			? Option.None<TResult>()
			: Option.Some(map(_value.Value));
	public readonly ValueOption<TResult> ValueMap<TResult>(Func<T, TResult> map) where TResult : struct
		=> _value is null
			? ValueOption.None<TResult>()
			: ValueOption.Some(map(_value.Value));

	public T Reduce(T orElse) => _value ?? orElse;

	public T Reduce(Func<T> orElse) => _value ?? orElse();

	#region
	public override bool Equals(object? obj) => obj is ValueOption<T> option && Equals(option);

	public override int GetHashCode() => _value?.GetHashCode() ?? 0;

	public override string ToString() => _value?.ToString() ?? "None";

	public bool Equals(ValueOption<T> other) => _value is null
		? other._value is null
		: _value.Equals(other._value);

	public static bool operator ==(ValueOption<T> left, ValueOption<T> right) => left._value is null 
		? right._value is null 
		: left.Equals(right);

	public static bool operator !=(ValueOption<T> left, ValueOption<T> right) => !(left == right);

	#endregion
}