namespace Make.Domain.Entities;

/// <summary>
/// Действие задачи.
/// </summary>
public interface IOperation : IEntity
{
	/// <summary>
	/// Наименование действия.
	/// </summary>
	public string Name { get; }


	/// <summary>
	/// Выполнить действие.
	/// </summary>
	public void Execute();
}