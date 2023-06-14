namespace Make.Domain.Entities;

public abstract class TaskBase : ITask
{
	public long Id { get; set; }

	public string Name { get; set; } = string.Empty;

	public ICollection<IAction> Actions { get; set; } = new List<IAction>();

	public ICollection<ITask> Children { get; set; } = new List<ITask>();


	public abstract void Execute();

	public override string ToString() => Name;
}