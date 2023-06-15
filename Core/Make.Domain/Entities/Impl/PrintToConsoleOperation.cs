namespace Make.Domain.Entities.Impl;

public class PrintToConsoleOperation : OperationBase
{
	public override void Execute()
	{
		Console.WriteLine($"  {Name}");
	}
}