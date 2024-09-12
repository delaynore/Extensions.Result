namespace Extensions.Result;

public static class ResultExtensions
{
	public static T Match<TValue, T>(
		this Result<TValue> result, 
		Func<Result<TValue>, T> onSuccess,
		Func<Result<TValue>, T> onFailure)
	{
		return result.IsSuccess 
			? onSuccess(result) 
			: onFailure(result);
	}
}
