using FluentAssertions;

namespace Extensions.Result.Tests;
public class OptionTests
{
	[Fact]
	public void ReduceValue_WhenSomeReturn5()
	{
		var option = Option.Some("string");

		var result = option.Reduce("s");

		result.Should().Be("string");
	}

	[Fact]
	public void ReduceProvider_WhenSomeReturn5()
	{
		var option = Option.Some<string>("string");

		var result = option.Reduce(() => string.Empty);

		result.Should().Be("string");
	}

	[Fact]
	public void ReduceValue_WhenNoneReturn10()
	{
		var option = Option.None<string>();

		var result = option.Reduce("str");

		result.Should().Be("str");
	}

	[Fact]
	public void ReduceProvider_WhenNoneReturnProvided()
	{
		var option = Option.None<string>();

		var result = option.Reduce(() => "string");

		result.Should().Be("string");
	}

	[Fact]
	public void Map_IfNone_ReturnAnotherNone()
	{
		var option = Option.None<int[]>();

		var result = option.Map(s => s.ToString()!);

		result.Should().Be(Option.None<string>());
	}

	[Fact]
	public void Map_IfSome_ReturnAnotherSome()
	{
		var option = Option.Some("string");

		var result = option.Map(s => "ss");

		result.Should().Be(Option.Some("ss"));
	}

	[Fact]
	public void ValueMap_IfNone_ReturnAnotherNone()
	{
		var option = Option.None<string>();

		var result = option.ValueMap(s => s[0]);

		result.Should().Be(ValueOption.None<char>());
	}

	[Fact]
	public void ValueMap_IfSome_ReturnAnotherSome()
	{
		var option = Option.Some("string");

		var result = option.ValueMap(s => s[0]);

		result.Should().Be(ValueOption.Some('s'));
	}

	[Fact]
	public void If_IfSomeAndTrue_ReturnsSome()
	{
		var option = Option.Some("string");

		var result = option.If(x => x.StartsWith("str"));

		result.Should().Be(Option.Some("string"));
	}

	[Fact]
	public void If_IfSomeAndFalse_ReturnsNone()
	{
		var option = Option.Some("string");

		var result = option.If(x => x.StartsWith("ing"));

		result.Should().Be(Option.None<string>());
	}

	[Fact]
	public void If_IfNone_ReturnsNone()
	{
		var option = Option.None<string>();

		var result = option.If(x => x.Length == 20);

		result.Should().Be(Option.None<string>());
	}

	[Fact]
	public void IfNot_IfSomeAndTrue_ReturnsSome()
	{
		var option = Option.Some("string");

		var result = option.IfNot(x => x.EndsWith("str"));

		result.Should().Be(Option.Some("string"));
	}

	[Fact]
	public void IfNot_IfSomeAndFalse_ReturnsNone()
	{
		var option = Option.Some("string");

		var result = option.IfNot(x => x.EndsWith("ing"));

		result.Should().Be(Option.None<string>());
	}

	[Fact]
	public void IfNot_IfNone_ReturnsNone()
	{
		var option = Option.None<string>();

		var result = option.IfNot(x => x.Length == 0);

		result.Should().Be(Option.None<string>());
	}

	[Fact]
	public void GetHashCode_None_0()
	{
		var option = Option.None<string>();

		var result = option.GetHashCode();

		result.Should().Be(0);
	}

	[Fact]
	public void GetHashCode_Some_HashCode()
	{
		var option = Option.Some("string");

		var result = option.GetHashCode();

		result.Should().Be("string".GetHashCode());
	}

	[Fact]
	public void ToString_None_None()
	{
		var option = Option.None<string>();

		var result = option.ToString();

		result.Should().Be("None");
	}

	[Fact]
	public void ToString_Some_ToString()
	{
		var option = Option.Some("s");

		var result = option.ToString();

		result.Should().Be("s");
	}

	[Theory]
	[MemberData(nameof(Options))]
	public void Equals_TwoOptionsEquals(Option<string> option1, Option<string> option2)
	{
		var result = option1.Equals(option2);
		result.Should().BeTrue();
	}

	public static IEnumerable<object[]> Options =
	[
		[Option.Some("syt"), Option.Some("syt")],
		[Option.Some("sy1t"), Option.Some("sy1t")],
		[Option.None<string>(), Option.None<string>()],
	];

	[Theory]
	[MemberData(nameof(OptionsNotEquals))]
	public void Equals_TwoOptionsNotEquals(Option<string> option1, Option<string> option2)
	{
		var result = option1.Equals(option2);
		result.Should().BeFalse();
	}

	public static IEnumerable<object[]> OptionsNotEquals =
	[
		[Option.Some("syt1"), Option.Some("syt")],
		[Option.Some("syt"), Option.Some("syt1")],
		[Option.None<string>(), Option.Some("sy1t")],
		[Option.Some("sy1t"), Option.None<string>()],
		[Option.Some("sy1t"), null!],
		[Option.None<string>(), null!],
	];

	[Theory]
	[InlineData(true)]
	[InlineData("ss")]
	[InlineData(5)]
	public void ObjectEquals_NotEquals(object thing)
	{
		var option = Option.Some("d");

		var result = option.Equals(thing);

		result.Should().BeFalse();
	}

	[Theory]
	[MemberData(nameof(Options))]
	public void ObjectEquals_Equals(Option<string> option, object option2)
	{
		var result = option.Equals(option2);

		result.Should().BeTrue();
	}

	[Theory]
	[MemberData(nameof(Options))]
	public void EqualityOperator_Equals(Option<string>? o1, Option<string> o2)
	{
		var result = o1 == o2;
		var result1 = o1 != o2;

		result.Should().BeTrue();
		result1.Should().BeFalse();
	}

	public static IEnumerable<object[]> OptionsEqualityOperatorsEqual =
	[
		[null!, null!],
		[Option.Some("syt"), Option.Some("syt")],
		[Option.Some("sy1t"), Option.Some("sy1t")],
		[Option.None<string>(), Option.None<string>()],
	];

	[Theory]
	[MemberData(nameof(OptionsNotEquals))]
	public void EqualityOperator_NotEquals(Option<string>? o1, Option<string> o2)
	{
		var result = o1 == o2;
		var result1 = o1 != o2;

		result.Should().BeFalse();
		result1.Should().BeTrue();
	}
}
