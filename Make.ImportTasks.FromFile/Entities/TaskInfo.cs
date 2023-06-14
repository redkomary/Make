namespace Make.ImportTasks.FromFile.Entities;

internal class TaskInfo
{
	public TaskInfo(TaskHeaderInfo header, IEnumerable<OperationInfo> operations)
	{
		Header = header;
		Operations = operations;
	}


	public TaskHeaderInfo Header { get; }

	public IEnumerable<OperationInfo> Operations { get; }
}