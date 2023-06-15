namespace Make.ImportJobs.FromFile.Entities;

internal class JobHeaderInfo
{
	public JobHeaderInfo(string name, IEnumerable<string> dependencies)
	{
		Name = name;
		Dependencies = dependencies;
	}

	public string Name { get; }

	public IEnumerable<string> Dependencies { get; }
}