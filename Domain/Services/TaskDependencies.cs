using Domain.Entities;

namespace Domain.Services;

internal class TaskDependencies
{
	/// <summary>
	/// Конструктор.
	/// </summary>
	/// <param name="task">Текущая задача.</param>
	public TaskDependencies(ITask task)
	{
		Task = task;
	}


	/// <summary>
	/// Текущая задача.
	/// </summary>
	public ITask Task { get; }

	/// <summary>
	/// Готова к исполнению.
	/// </summary>
	public bool ReadyToExecute => !Descendants.Any();

	/// <summary>
	/// Задачи, от которых напрямую зависит текущая.
	/// </summary>
	public HashSet<ITask> Descendants { get; } = new();

	/// <summary>
	/// Задачи, которые напрямую зависят от текущей.
	/// </summary>
	public HashSet<ITask> Ancestors { get; } = new();
}