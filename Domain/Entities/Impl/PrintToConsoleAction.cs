namespace Make.Domain.Entities.Impl;

public class PrintToConsoleAction : ActionBase
{
	public PrintToConsoleAction(string name)
		: base(name)
	{
	}


	public override void Execute()
	{
		Console.WriteLine($"  {Name}");
	}
}