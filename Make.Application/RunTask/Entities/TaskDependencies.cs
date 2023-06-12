using Make.Domain.Entities;

namespace Make.Application.RunTask.Entities;

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
	public bool ReadyToExecute => !OutComing.Any();

	/// <summary>
	/// Задачи, от которых напрямую зависит текущая.
	/// </summary>
	public HashSet<ITask> OutComing { get; } = new();

	/// <summary>
	/// Задачи, которые напрямую зависят от текущей.
	/// </summary>
	public HashSet<ITask> InComing { get; } = new();
}