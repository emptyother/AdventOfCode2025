using System.Diagnostics;

namespace AdventOfCode2025
{
	[TestClass]
	public sealed class Test1
	{
		[TestInitialize]
		public void TestInit()
		{
		}

		[TestMethod]
		public void Day1_Task1()
		{
			var path = Path.Combine(AppContext.BaseDirectory, "Data", "input.txt");
			Assert.IsTrue(File.Exists(path), $"Missing test input at {path}");

			var dial = 50;
			Debug.WriteLine($"Starting at {dial}.");
			var zeroDialHits = 0;
			foreach (var line in File.ReadLines(path))
			{
				if (!line.StartsWith("L") && !line.StartsWith("R")) throw new Exception("Not a command.");
				var isNegative = line.StartsWith("L");
				var value = int.Parse(line.Substring(1));
				value = isNegative ? -value : value;
				dial = WrapValue(dial + value, 0, 99);
				if(dial == 0) zeroDialHits++;
				Debug.WriteLine($"{line}: Dial is now at {dial}");
			}
			Debug.WriteLine($"================================");
			Debug.WriteLine($" Hit zero {zeroDialHits} times. ");
		}

		[TestMethod]
		public async Task TestWrapping()
		{
			var min = 0;
			var max = 99;
			Assert.AreEqual(99, WrapValue(99, min, max));
			Assert.AreEqual(1, WrapValue(1, min, max));
			Assert.AreEqual(0, WrapValue(100, min, max));
			Assert.AreEqual(1, WrapValue(101, min, max));
			Assert.AreEqual(2, WrapValue(102, min, max));
			Assert.AreEqual(99, WrapValue(-1, min, max));
			Assert.AreEqual(98, WrapValue(-2, min, max));
			Assert.AreEqual(97, WrapValue(-3, min, max));
			Assert.AreEqual(96, WrapValue(-4, min, max));
			await Task.CompletedTask;
		}

		public static int WrapValue(int value, int minValue, int maxValue)
		{
			var size = (maxValue - minValue) + 1;
			if (size <= 0) throw new ArgumentException("Invalid range.");
			var shifted = value - minValue;
			var wrappedZeroBased = ((shifted % size) + size) % size;
			return wrappedZeroBased + minValue;
		}
	}
}
