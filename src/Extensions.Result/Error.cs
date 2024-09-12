namespace Extensions.Result;

public record Error(string Code, string? Description = default)
{
	public static Error None = new(string.Empty);
}
