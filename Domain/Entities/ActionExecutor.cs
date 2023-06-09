namespace Domain.Entities;

public class ActionExecutor
{
	public void Execute(IAction action)
	{
		Console.WriteLine(action.Name);
	}
}