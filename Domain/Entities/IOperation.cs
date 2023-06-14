namespace Make.Domain.Entities;

public interface IOperation : IEntity
{
	public string Name { get; }


	public void Execute();
}