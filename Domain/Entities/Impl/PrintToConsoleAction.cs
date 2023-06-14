namespace Make.Domain.Entities.Impl;

public class PrintToConsoleAction : ActionBase
{
	public override void Execute()
	{
		Console.WriteLine($"  {Name}");
	}
}