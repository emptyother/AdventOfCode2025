using System.Diagnostics;

namespace AdventOfCode2025;

[TestClass]
public class Tasks
{
	[TestMethod]
	public void Day1_Task1()
	{
		var path = Path.Combine(AppContext.BaseDirectory, "Data", "input.txt");
		var zeroDialHits = 0;
		var dial = 50;
		foreach (var line in File.ReadLines(path))
		{
			var isNegative = line.StartsWith("L");
			var value = int.Parse(line.Substring(1));
			value = isNegative ? -value : value;
			dial = Utils.WrapValue(dial + value);
			if (dial == 0) zeroDialHits++;
		}
		Debug.WriteLine($" Hit zero {zeroDialHits} times. ");
	}

	[TestMethod]
	public void Day1_Task2_Sample()
	{
		var path = Path.Combine(AppContext.BaseDirectory, "Data", "input2_sample.txt");
		var dialZeroHitsTotal = 0;
		var dialPassedZeroTotal = 0;
		var dial = 50;
		Debug.WriteLine($"The dial starts by pointing at {dial}");
		foreach (var line in File.ReadLines(path))
		{
			var isNegative = line.StartsWith("L");
			var value = int.Parse(line.Substring(1));
			value = isNegative ? -value : value;
			// If dial is already at zero, we don't count it as passing zero again.
			var passedZeroCount = dial == 0 ? 0 : Math.Abs(Utils.CountWrappings(dial, value));
			dial = Utils.WrapValue(dial + value);
			if (dial == 0)
			{
				dialZeroHitsTotal++;
			}
			else
			{
				dialPassedZeroTotal += passedZeroCount;
			}
			var debugString = $"The dial is rotated {line} to point at {dial}";
			if (passedZeroCount > 0 && dial != 0)
			{
				debugString += $"; during this rotation, it points at zero {passedZeroCount} time(s).";
			}
			else
			{
				debugString += ".";
			}
			Debug.WriteLine(debugString);
		}
		Debug.WriteLine($" Hit zero {dialZeroHitsTotal} times and passed zero {dialPassedZeroTotal} times. ");
		Assert.AreEqual(3, dialZeroHitsTotal);
		Assert.AreEqual(3, dialPassedZeroTotal);
	}

	[TestMethod]
	public void Day1_Task2_Final()
	{
		var path = Path.Combine(AppContext.BaseDirectory, "Data", "input.txt");
		var dialZeroHitsTotal = 0;
		var dialPassedZeroTotal = 0;
		var dial = 50;
		foreach (var line in File.ReadLines(path))
		{
			var isNegative = line.StartsWith("L");
			var value = int.Parse(line.Substring(1));
			value = isNegative ? -value : value;
			if (value == 0) throw new Exception("Unexpected value");
			var passedZeroCount = dial == 0 ? 0 : Math.Abs(Utils.CountWrappings(dial, value));
			dial = Utils.WrapValue(dial + value);
			if (dial == 0)
			{
				dialZeroHitsTotal++;
			}
			else
			{
				dialPassedZeroTotal += passedZeroCount;
			}
			var debugString = $"The dial is rotated {line} to point at {dial}";
			if (dial == 0)
			{
				debugString += "; it hits zero during this rotation.";
			}
			else if (passedZeroCount > 0)
			{
				debugString += $"; during this rotation, it points at zero {passedZeroCount} time(s).";
			}
			else
			{
				debugString += ".";
			}
			Debug.WriteLine(debugString);
		}
		Debug.WriteLine($" Hit zero {dialZeroHitsTotal} times and passed zero {dialPassedZeroTotal} times. ");
		Debug.WriteLine($" Password is {dialZeroHitsTotal + dialPassedZeroTotal}. ");
	}

	[TestMethod]
	public void Day2_Task1_Sample()
	{
		var path = Path.Combine(AppContext.BaseDirectory, "Data", "day2_input_sample.txt");
		var content = File.ReadAllText(path);
		Day2Task(content);
	}

	[TestMethod]
	public void Day2_Task1_Full()
	{
		var path = Path.Combine(AppContext.BaseDirectory, "Data", "day2_input.txt");
		var content = File.ReadAllText(path);
		Day2Task(content);
	}

	private static void Day2Task(string content)
	{

		var ranges = content.Split(',');
		var rangeList = new List<(long start, long end)>();
		foreach (var range in ranges)
		{
			var bounds = range.Split('-');
			var start = long.Parse(bounds[0]);
			var end = long.Parse(bounds[1]);
			if(end <= start) throw new Exception("Invalid range");
			rangeList.Add((start, end));
		}
		var allInvalidIds = new List<long>();
		foreach (var range in rangeList)
		{
			var result = Utils.RangeHasRepeatingDigitSequences(range);
			var debugText = $" - `{range.start}`-`{range.end}`";
			if(result.Count == 0)
			{
				debugText += " contains no invalid IDs.";
			}
			else
			{
				debugText += $" has {result.Count} invalid ids,";
				var allSequences = result.Select(e => long.Parse(e.FullSequence));
				var ands = string.Join(" and ", allSequences);
				debugText += $" {ands}.";
				allInvalidIds.AddRange(allSequences);
			}
			Debug.WriteLine(debugText);
		}
		var sumOfAllInvalidIds = allInvalidIds.Sum();
		Debug.WriteLine($" Sum of all invalid IDs: {sumOfAllInvalidIds} ");
	}
}
