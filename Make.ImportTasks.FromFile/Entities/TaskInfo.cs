using Make.ImportTasks.FromFile.Services;

namespace Make.ImportTasks.FromFile;

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