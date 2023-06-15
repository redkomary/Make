namespace Make.Domain.Entities.Impl;

/// <summary>
/// Действие вывода наименования в консоль.
/// </summary>
public class PrintToConsoleOperation : OperationBase
{
	/// <inheritdoc />
	public override void Execute()
	{
		Console.WriteLine($"  {Name}");
	}
}