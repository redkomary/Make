using Make.Utilities;


namespace Make.Domain.Entities.Impl;

/// <summary>
/// Задача вывода наименования в консоль.
/// </summary>
public class PrintToConsoleJob : JobBase
{
	/// <inheritdoc />
	public override void Execute()
	{
		Console.WriteLine(Name);
		Operations.ForEach(operation => operation.Execute());
	}
}