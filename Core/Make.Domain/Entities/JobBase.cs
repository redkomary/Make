namespace Make.Domain.Entities;

/// <summary>
/// Базовый класс задачи.
/// </summary>
public abstract class JobBase : IJob
{
	/// <inheritdoc />
	public long Id { get; set; }

	/// <inheritdoc />
	public string Name { get; set; } = string.Empty;

	/// <inheritdoc />
	public ICollection<IOperation> Operations { get; set; } = new List<IOperation>();

	/// <inheritdoc />
	public ICollection<IJob> SubJobs { get; set; } = new List<IJob>();


	/// <inheritdoc />
	public abstract void Execute();

	/// <inheritdoc />
	public override string ToString() => Name;
}