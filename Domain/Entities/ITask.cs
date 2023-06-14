namespace Make.Domain.Entities;

public interface ITask : IEntity
{
	public string Name { get; }

	public ICollection<IAction> Actions { get; }

	public ICollection<ITask> Children { get; }


	public void Execute();
}