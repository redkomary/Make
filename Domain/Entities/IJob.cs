namespace Make.Domain.Entities;

public interface IJob : IEntity
{
	public string Name { get; }

	public ICollection<IOperation> Operations { get; }

	public ICollection<IJob> Children { get; }


	public void Execute();
}