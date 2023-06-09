namespace Domain.Entities;

public class Task : ITask
{
	public string Name { get; }

	public IEnumerable<IAction> Actions { get; } =
		Enumerable.Empty<IAction>();

	public IEnumerable<ITask> Children { get; } =
		Enumerable.Empty<ITask>();
}