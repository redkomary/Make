namespace Make.Domain.Entities;

public abstract class ActionBase : IAction
{
	public long Id { get; set; }

	public string Name { get; set; } = string.Empty;


	public abstract void Execute();

	public override string ToString() => Name;
}