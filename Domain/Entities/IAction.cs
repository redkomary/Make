namespace Domain.Entities;

public interface IAction
{
	public string Name { get; }

	public void Execute();
}