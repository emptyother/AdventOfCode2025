using System.Diagnostics;

namespace AdventOfCode2025;

[TestClass]
public class Day2
{
	[TestMethod]
	public void Day2_Task1_Sample()
	{
		var path = Path.Combine(AppContext.BaseDirectory, "Data", "day2_input_sample.txt");
		var content = File.ReadAllText(path);
		Day2Task1(content);
	}

	[TestMethod]
	public void Day2_Task1_Full()
	{
		var path = Path.Combine(AppContext.BaseDirectory, "Data", "day2_input.txt");
		var content = File.ReadAllText(path);
		Day2Task1(content);
	}

	[TestMethod]
	public void Day2_Task2_Sample()
	{
		var path = Path.Combine(AppContext.BaseDirectory, "Data", "day2_input_sample.txt");
		var content = File.ReadAllText(path);
		Day2Task2(content);
	}

	[TestMethod]
	public void Day2_Task2_Full()
	{
		var path = Path.Combine(AppContext.BaseDirectory, "Data", "day2_input.txt");
		var content = File.ReadAllText(path);
		Day2Task2(content);
	}

	private static void Day2Task1(string content)
	{

		var ranges = content.Split(',');
		var rangeList = new List<(long start, long end)>();
		foreach (var range in ranges)
		{
			var bounds = range.Split('-');
			var start = long.Parse(bounds[0]);
			var end = long.Parse(bounds[1]);
			if (end <= start) throw new Exception("Invalid range");
			rangeList.Add((start, end));
		}
		var allInvalidIds = new List<long>();
		foreach (var range in rangeList)
		{
			var result = Utils.RangeHasDoubleDigitSequences(range);
			var debugText = $" - `{range.start}`-`{range.end}`";
			if (result.Count == 0)
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

	private static void Day2Task2(string content)
	{

		var ranges = content.Split(',');
		var rangeList = new List<(long start, long end)>();
		foreach (var range in ranges)
		{
			var bounds = range.Split('-');
			var start = long.Parse(bounds[0]);
			var end = long.Parse(bounds[1]);
			if (end <= start) throw new Exception("Invalid range");
			rangeList.Add((start, end));
		}
		var allInvalidIds = new List<long>();
		foreach (var range in rangeList)
		{
			var result = Utils.RangeHasRepeatingDigitSequences(range);
			var debugText = $" - `{range.start}`-`{range.end}`";
			if (result.Count == 0)
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
