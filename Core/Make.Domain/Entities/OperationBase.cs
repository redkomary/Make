namespace Make.Domain.Entities;

/// <summary>
/// Базовый класс действия задачи.
/// </summary>
public abstract class OperationBase : IOperation
{
	/// <inheritdoc />
	public long Id { get; set; }

	/// <inheritdoc />
	public string Name { get; set; } = string.Empty;


	/// <inheritdoc />
	public abstract void Execute();

	/// <inheritdoc />
	public override string ToString() => Name;
}