using Make.Utilities;

namespace Domain.Entities;

public class TaskExecutor
{
	private readonly ActionExecutor _actionExecutor = new();

	public void Execute(ITask task)
	{
		Console.WriteLine(task.Name);
		task.Actions.ForEach(_actionExecutor.Execute);
	}
}