namespace Make.Domain.Entities;

public abstract class TaskBase : ITask
{
	public long Id { get; set; }

	public string Name { get; set; } = string.Empty;

	public ICollection<IOperation> Operations { get; set; } = new List<IOperation>();

	public ICollection<ITask> Children { get; set; } = new List<ITask>();


	public abstract void Execute();

	public override string ToString() => Name;
}