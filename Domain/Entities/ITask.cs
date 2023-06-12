namespace Make.Domain.Entities;

public interface ITask : IWork
{
	public string Name { get; }

	public IEnumerable<IAction> Actions { get; }

	public IEnumerable<ITask> Children { get; }
}