namespace Domain.Entities;

public abstract class Action : IAction
{
	public string Name { get; }

	public abstract void Execute();
}