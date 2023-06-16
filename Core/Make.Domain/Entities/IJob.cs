namespace Make.Domain.Entities;

/// <summary>
/// Задача.
/// </summary>
public interface IJob : IEntity
{
	/// <summary>
	/// Наименование задачи.
	/// </summary>
	public string Name { get; }

	/// <summary>
	/// Действия задачи.
	/// </summary>
	public ICollection<IOperation> Operations { get; }

	/// <summary>
	/// Задачи, от которых зависит данная.
	/// </summary>
	public ICollection<IJob> SubJobs { get; }


	/// <summary>
	/// Выполнить задачу.
	/// </summary>
	public void Execute();
}