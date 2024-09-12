namespace Extensions.Result;

public class Result<TValue> : Result
{
	private readonly TValue? _value;

	public TValue? Value => IsSuccess
		? _value
		: throw new InvalidOperationException("The value of failure result inaccessible");

	internal Result(TValue value) : base(true)
		=> _value = value;

	internal Result(Error error) : base(error) { }

	public static implicit operator Result<TValue>(TValue value)
		=> new(value);

	public static implicit operator Result<TValue>(Error error) => new(error);
}
