using Make.Utilities;

namespace Make.Domain.Entities.Impl;

public class PrintToConsoleJob : JobBase
{
	public override void Execute()
	{
		Console.WriteLine(Name);
		Operations.ForEach(operation => operation.Execute());
	}
}