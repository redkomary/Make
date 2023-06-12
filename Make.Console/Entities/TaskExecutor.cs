using Make.Domain.Entities;
using Make.Utilities;

namespace Make.ConsoleInteraction.Entities;

public class TaskExecutor : IExecutor<ITask>
{
	private readonly ActionExecutor _actionExecutor = new();

	public void Execute(ITask task)
	{
		Console.WriteLine(task.Name);
		task.Actions.ForEach(_actionExecutor.Execute);
	}
}