namespace Make.Domain.Entities;

// -> Operation
public interface IAction
{
	public string Name { get; }


	public void Execute();
}