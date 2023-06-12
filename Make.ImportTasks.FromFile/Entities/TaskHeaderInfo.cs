namespace Make.ImportTasks.FromFile.Entities;

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