using Make.Utilities;

namespace Make.Domain.Entities.Impl;

public class PrintToConsoleTask : TaskBase
{
	public override void Execute()
	{
		Console.WriteLine(Name);
		Operations.ForEach(operation => operation.Execute());
	}
}