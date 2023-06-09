namespace Make.ImportTasks.FromFile.Services;

internal class TaskInfoParser
{
	public TaskInfo Parse(IReadOnlyList<string> lines)
	{
		if (!lines.Any())
			throw new ArgumentException("Не удалось создать описание задачи из пустого блока.", nameof(lines));

		TaskHeaderInfo header = ParseHeader(lines[0]);

		IEnumerable<ActionInfo> actions = lines
			.Skip(1)
			.Select(ParseAction);

		return new TaskInfo(header, actions);
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

	private ActionInfo ParseAction(string str)
	{
		string name = str.Trim();
		return new ActionInfo(name);
	}
}