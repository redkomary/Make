using Make.Domain.Entities;

namespace Make.Application.RunJob.Entities;

internal class JobDependencies
{
	/// <summary>
	/// Конструктор.
	/// </summary>
	/// <param name="job">Текущая задача.</param>
	public JobDependencies(IJob job)
	{
		Job = job;
	}


	/// <summary>
	/// Текущая задача.
	/// </summary>
	public IJob Job { get; }

	/// <summary>
	/// Готова к исполнению.
	/// </summary>
	public bool ReadyToExecute => !OutComing.Any();

	/// <summary>
	/// Задачи, от которых напрямую зависит текущая.
	/// </summary>
	public HashSet<IJob> OutComing { get; } = new();

	/// <summary>
	/// Задачи, которые напрямую зависят от текущей.
	/// </summary>
	public HashSet<IJob> InComing { get; } = new();
}