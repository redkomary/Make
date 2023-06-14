using Make.Utilities;

namespace Make.Domain.Entities.Impl;

public class PrintToConsoleTask : TaskBase
{
	public override void Execute()
	{
		Console.WriteLine(Name);
		Actions.ForEach(action => action.Execute());
	}
}