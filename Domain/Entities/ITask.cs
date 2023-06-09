namespace Domain.Entities;

public interface ITask
{
	public string Name { get; }

	public IEnumerable<IAction> Actions { get; }

	public IEnumerable<ITask> Children { get; }


	public void Execute()
	{
		foreach (IAction action in Actions)
			action.Execute();
	}
}