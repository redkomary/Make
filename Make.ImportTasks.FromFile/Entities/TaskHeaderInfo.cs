namespace Make.ImportTasks.FromFile.Services;

internal class TaskHeaderInfo
{
	public TaskHeaderInfo(string name, IEnumerable<string> dependencies)
	{
		Name = name;
		Dependencies = dependencies;
	}

	public string Name { get; }

	public IEnumerable<string> Dependencies { get; }
}