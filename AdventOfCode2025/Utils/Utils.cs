namespace AdventOfCode2025;

public class Utils
{
	private const int minValue = 0;
	private const int maxValue = 99;

	public static int WrapValue(int value)
	{
		const int size = (maxValue - minValue) + 1;
		var shifted = value - minValue;
		var wrappedZeroBased = ((shifted % size) + size) % size;
		return wrappedZeroBased + minValue;
	}

	/// <summary>
	/// Counts how many times a dial, with a range of 0 to 99, pass zero when modified.
	/// Should not count the starting position or ending position, only when it passes.
	/// </summary>
	/// <param name="dial"></param>
	/// <param name="modification"></param>
	/// <returns></returns>
	/// <exception cref="ArgumentException"></exception>
	public static int CountWrappings(int dial, int modification)
	{
		var size = (maxValue - minValue) + 1;
		if (size <= 0) throw new ArgumentException("Invalid range.");
		var shiftedStart = dial - minValue;
		var shiftedEnd = shiftedStart + modification;
		return FlooredDivision(shiftedEnd, size) - FlooredDivision(shiftedStart + 1, size);
		//if (modification >= 0)
		//{
		//	// Count forward crossings, excluding the starting position.
		//	return FlooredDivision(shiftedEnd, size) - FlooredDivision(shiftedStart + 1, size);
		//}
		//else
		//{
		//	// Count backward crossings, excluding the starting position.
		//	var crossings = CeiledDivision(shiftedEnd, size) - CeiledDivision(shiftedStart, size);
		//	// If we end exactly on a boundary (e.g., zero), do not count that endpoint.
		//	if ((shiftedEnd % size) == 0) crossings++;
		//	return crossings;
		//}
	}

	private static int FlooredDivision(int a, int b) => (int)Math.Floor((double)a / b);
	private static int CeiledDivision(int a, int b) => (int)Math.Ceiling((double)a / b);

	public static RepeatedDigits? HasDoubleDigitSequences(long current)
	{
		// Same as below except I only need to divide by 2.
		var currentStr = current.ToString();
		// Must divide evenly:
		if (currentStr.Length % 2 != 0) return null;
		var partLength = currentStr.Length / 2;
		var firstPart = currentStr.Substring(0, partLength);
		var secondPart = currentStr.Substring(partLength, partLength);
		if (firstPart == secondPart)
		{
			return new RepeatedDigits
			{
				RepeatedDigit = firstPart,
				RepeatedCount = 2,
				FullSequence = currentStr
			};
		}
		return null;
	}

	public static RepeatedDigits? HasRepeatingDigitSequences(long current)
	{
		var currentStr = current.ToString();
		for (var divider = 2; divider <= currentStr.Length; divider++)
		{
			// split into parts of 'divider' length
			if (currentStr.Length % divider != 0) continue; // must divide evenly
			var partLength = currentStr.Length / divider;
			var parts = new List<string>();
			for (var i = 0; i < divider; i++)
			{
				var part = currentStr.Substring(i * partLength, partLength);
				parts.Add(part);
			}
			// Find if all parts are the same
			var firstPart = parts[0];
			var allSame = parts.All(p => p == firstPart);
			if (allSame)
			{
				return new RepeatedDigits
				{
					RepeatedDigit = firstPart,
					RepeatedCount = divider,
					FullSequence = currentStr
				};
			}
		}
		// No repeating sequences found.
		return null;
	}

	public static List<RepeatedDigits> RangeHasDoubleDigitSequences((long start, long end) range)
	{
		var current = range.start;
		var list = new List<RepeatedDigits>();
		while (current <= range.end)
		{
			var res = HasDoubleDigitSequences(current);
			if (res.HasValue)
			{
				list.Add(res.Value);
			}
			current++;
		}
		return list;
	}

	public static List<RepeatedDigits> RangeHasRepeatingDigitSequences((long start, long end) range)
	{
		var current = range.start;
		var list = new List<RepeatedDigits>();
		while (current <= range.end)
		{
			var res = HasRepeatingDigitSequences(current);
			if (res.HasValue)
			{
				list.Add(res.Value);
			}
			current++;
		}
		return list;
	}
}
