namespace AdventOfCode2025;

[TestClass]
public class UtilsTests
{
	[TestMethod]
	public async Task TestWrapping()
	{
		Assert.AreEqual(99, Utils.WrapValue(99));
		Assert.AreEqual(1, Utils.WrapValue(1));
		Assert.AreEqual(0, Utils.WrapValue(100));
		Assert.AreEqual(1, Utils.WrapValue(101));
		Assert.AreEqual(2, Utils.WrapValue(102));
		Assert.AreEqual(99, Utils.WrapValue(-1));
		Assert.AreEqual(98, Utils.WrapValue(-2));
		Assert.AreEqual(97, Utils.WrapValue(-3));
		Assert.AreEqual(96, Utils.WrapValue(-4));
		await Task.CompletedTask;
	}

	[TestMethod]
	public async Task TestCountWrapping()
	{
		Assert.AreEqual(1, Utils.CountWrappings(52, 48));
		// Assert.AreEqual(0, Utils.CountWrappings(0, -5));
		Assert.AreEqual(-1, Utils.CountWrappings(0, -5));
		Assert.AreEqual(10, Utils.CountWrappings(50, 1000));
		Assert.AreEqual(-10, Utils.CountWrappings(50, -1000));
		Assert.AreEqual(0, Utils.CountWrappings(50, 49));
		Assert.AreEqual(1, Utils.CountWrappings(50, 50));
		Assert.AreEqual(1, Utils.CountWrappings(50, 51));
		Assert.AreEqual(2, Utils.CountWrappings(50, 150));
		//Assert.AreEqual(-1, Utils.CountWrappings(50, -50));
		Assert.AreEqual(0, Utils.CountWrappings(50, -50));
		Assert.AreEqual(-1, Utils.CountWrappings(50, -51));
		Assert.AreEqual(-1, Utils.CountWrappings(0, -1));
		Assert.AreEqual(-5, Utils.CountWrappings(0, -500));
		//Assert.AreEqual(-4, Utils.CountWrappings(0, -500));
		Assert.AreEqual(-1, Utils.CountWrappings(0, -100));
		//Assert.AreEqual(0, Utils.CountWrappings(0, -100));
		//Assert.AreEqual(-1, Utils.CountWrappings(0, -101));
		Assert.AreEqual(-2, Utils.CountWrappings(0, -101));
		await Task.CompletedTask;
	}

	[TestMethod]
	public async Task TestHasRepeatingDigitSequences()
	{
		var result = Utils.RangeHasDoubleDigitSequences(new ValueTuple<int, int>(11, 22));
		Assert.IsNotNull(result);
		Assert.HasCount(2, result);
		await Task.CompletedTask;
	}

	[TestMethod]
	public async Task TestHasRepeatingDigitSequences2()
	{
		var result = Utils.HasDoubleDigitSequences(123123);
		Assert.IsNotNull(result);
		Assert.AreEqual(123.ToString(), result.Value.RepeatedDigit);
		Assert.AreEqual(2, result.Value.RepeatedCount);
		Assert.AreEqual(123123.ToString(), result.Value.FullSequence);
		await Task.CompletedTask;
	}
}
