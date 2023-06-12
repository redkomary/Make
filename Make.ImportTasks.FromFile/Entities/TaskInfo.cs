namespace Make.ImportTasks.FromFile.Entities;

internal class TaskInfo
{
	public TaskInfo(TaskHeaderInfo header, IEnumerable<ActionInfo> actions)
	{
		Header = header;
		Actions = actions;
	}


	public TaskHeaderInfo Header { get; }

	public IEnumerable<ActionInfo> Actions { get; }
}