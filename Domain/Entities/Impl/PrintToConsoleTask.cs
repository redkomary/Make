using Make.Utilities;

namespace Make.Domain.Entities.Impl;

public class PrintToConsoleTask : TaskBase
{
	public PrintToConsoleTask(string name)
		: this(name, Enumerable.Empty<IAction>())
	{
	}

	public PrintToConsoleTask(string name, IEnumerable<IAction> actions)
		: base(name, actions)
	{
	}


	public override void Execute()
	{
		Console.WriteLine(Name);
		Actions.ForEach(action => action.Execute());
	}
}