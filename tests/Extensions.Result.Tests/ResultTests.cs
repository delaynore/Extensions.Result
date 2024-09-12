using FluentAssertions;

namespace Extensions.Result.Tests;

public class ResultTests
{
	[Fact]
	public void Success_ShouldBeSuccess()
	{
		var result = Result.Success();

		result.IsSuccess.Should().BeTrue();
		result.IsFailure.Should().BeFalse();
		result.Error.Should().Be(Error.None);
	}

	[Fact]
	public void Failure_ShouldBeFailure()
	{
		var error = new Error("Code", "Desc");
		var result = Result.Failure(error);

		result.IsFailure.Should().BeTrue();
		result.IsSuccess.Should().BeFalse();
		result.Error.Should().Be(error);
	}

	[Fact]	
	public void Failure_ErrorNull_ThrowsException()
	{
		var act = () => Result.Failure(null!);

		act.Should().Throw<ArgumentNullException>();
	}

	[Fact]
	public void Success_WithValue()
	{
		var result = Result.Success(5);

		result.IsSuccess.Should().BeTrue();
		result.IsFailure.Should().BeFalse();
		result.Value.Should().Be(5);
	}

	[Fact]
	public void Failure_AccessToValueViolated()
	{
		var error = new Error("Code", "Desc");
		var result = Result.Failure<int>(error);

		result.IsSuccess.Should().BeFalse();
		result.IsFailure.Should().BeTrue();
		var act = () => result.Value;
		act.Should().Throw<InvalidOperationException>();
	}
}