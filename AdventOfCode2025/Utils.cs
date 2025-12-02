using System.Diagnostics;

namespace AdventOfCode2025;

public class Utils
{
	private static int minValue = 0;
	private static int maxValue = 99;

	public static int WrapValue(int value)
	{
		var size = (maxValue - minValue) + 1;
		if (size <= 0) throw new ArgumentException("Invalid range.");
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

	public static RepeatedDigits? HasRepeatingDigitSequences(int current)
	{
		var currentStr = current.ToString();
		for (var val = current; val > 0; val--)
		{
			var valString = val.ToString();
			// Count how many times the current valString exists in currentStr consecutively.:
			var count = 0;
			var testStr = currentStr;
			while (testStr.StartsWith(valString))
			{
				count++;
				testStr = testStr[valString.Length..];
			}
			if(count > 1)
			{
				return new RepeatedDigits
				{
					RepeatedDigit = valString,
					RepeatedCount = count,
					FullSequence = currentStr
				};
			}
		}
		// No repeating sequences found.
		return null;
	}

	public static List<RepeatedDigits> RangeHasRepeatingDigitSequences((int start, int end) range)
	{
		var current = range.start;
		var list = new List<RepeatedDigits>();
		while (current <= range.end)
		{
			Debug.WriteLine($"Range: {range.start} - {range.end}. Checking value: {current}.");
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

public struct RepeatedDigits
{
	public string? RepeatedDigit;
	public int RepeatedCount;
	public string FullSequence;
}
