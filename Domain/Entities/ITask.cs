namespace Make.Domain.Entities;

public interface ITask : IEntity
{
	public string Name { get; }

	public ICollection<IOperation> Operations { get; }

	public ICollection<ITask> Children { get; }


	public void Execute();
}