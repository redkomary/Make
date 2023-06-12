using Make.Domain.Entities;

namespace Make.ConsoleInteraction.Entities;

public class ActionExecutor : IExecutor<IAction>
{
	public void Execute(IAction action)
	{
		Console.WriteLine(action.Name);
	}
}