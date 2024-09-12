namespace Extensions.Result;

public class Result
{
	public bool IsSuccess { get; init; }

	public bool IsFailure => !IsSuccess;

	public Error Error { get; init; }

	protected Result(bool isSuccess)
	{
		IsSuccess = isSuccess;
		Error = Error.None;
	}

	protected Result(Error error)
	{
		ArgumentNullException.ThrowIfNull(error, nameof(error));

		IsSuccess = false;
		Error = error;
	}

	public static implicit operator Result(Error error) => new(error); 

	public static Result Success() => new(true);

	public static Result Failure(Error error) => new(error);

	public static Result<TValue> Success<TValue>(TValue value) => new(value);

	public static Result<TValue> Failure<TValue>(Error error) => new(error);
}