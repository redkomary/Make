namespace Make.Domain.Entities;

// -> Operation
public interface IAction : IWork
{
	public string Name { get; }
}