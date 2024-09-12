using System.Numerics;

namespace Extensions.Result;

public static class Option 
{
	public static Option<T> Some<T>(T value)
		where T : class
		=> Option<T>.Some(value);

	public static Option<T> None<T>()
		where T : class
		=> Option<T>.None();
}

public static class ValueOption
{
	public static ValueOption<T> Some<T>(T value) where T : struct
		=> ValueOption<T>.Some(value);

	public static ValueOption<T> None<T>() where T : struct
		=> ValueOption<T>.None();
}

public class Option<T> : IEquatable<Option<T>>, IEqualityOperators<Option<T>, Option<T>, bool>
	where T: class
{
	private readonly T? _value;

	internal Option(T? value) => (_value) = (value);

	public static Option<T> Some(T value)
	{
		ArgumentNullException.ThrowIfNull(value, nameof(value));

		return new Option<T>(value);
	}

	public static Option<T> None() => new(default);

	public Option<TResult> Map<TResult>(Func<T, TResult> map)
		where TResult : class
		=> _value is null 
			? Option.None<TResult>() 
			: Option.Some(map(_value));

	public ValueOption<TResult> ValueMap<TResult>(Func<T, TResult> map) where TResult : struct
		=> _value is null
			? ValueOption.None<TResult>()
			: ValueOption.Some(map(_value));

	public Option<T> If(Predicate<T> predicate) => _value is not null && predicate(_value) 
		? this 
		: Option.None<T>();

	public Option<T> IfNot(Predicate<T> predicate) => _value is not null && !predicate(_value) 
		? this 
		: Option.None<T>();

	public T Reduce(T orElse) => _value ?? orElse;

	public T Reduce(Func<T> orElse) => _value ?? orElse();

	#region

	public override bool Equals(object? obj) => obj is Option<T> option && Equals(option);

	public override int GetHashCode() => _value?.GetHashCode() ?? 0;

	public override string ToString() => _value?.ToString() ?? "None";

	public bool Equals(Option<T>? other)
	{
		return other is not null 
			&& (_value is null
				? other._value is null
				: _value.Equals(other._value));
	}

	public static bool operator ==(Option<T>? left, Option<T>? right) => left is null ? right is null : left.Equals(right);	
	public static bool operator !=(Option<T>? left, Option<T>? right) => !(left == right);
	
	#endregion
}
