namespace Make.Domain.Entities;

// -> Operation
public interface IAction : IEntity
{
	public string Name { get; }


	public void Execute();
}