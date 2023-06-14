using Make.ImportTasks.FromFile.Entities;

namespace Make.ImportTasks.FromFile.Services;

internal class TaskInfoParser
{
	public TaskInfo Parse(IReadOnlyList<string> lines)
	{
		if (!lines.Any())
			throw new ArgumentException("Не удалось создать описание задачи из пустого блока.", nameof(lines));

		TaskHeaderInfo header = ParseHeader(lines[0]);

		IEnumerable<OperationInfo> operations = lines
			.Skip(1)
			.Select(ParseOperation);

		return new TaskInfo(header, operations);
	}

	private TaskHeaderInfo ParseHeader(string str)
	{
		string[] parts = str.Split(':', StringSplitOptions.TrimEntries);

		if (parts.Length is 0 or > 2)
			throw new InvalidOperationException($"Не удалось прочитать заголовок задачи из строки \"{str}\".");

		string name = parts[0];

		IEnumerable<string> dependencies = parts.Length == 2
			? parts[1].Split(Array.Empty<string>(), StringSplitOptions.RemoveEmptyEntries)
			: Enumerable.Empty<string>();

		return new TaskHeaderInfo(name, dependencies);
	}

	private OperationInfo ParseOperation(string str)
	{
		string name = str.Trim();
		return new OperationInfo(name);
	}
}