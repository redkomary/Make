namespace Make.Domain.Entities;

public abstract class TaskBase : ITask
{
	private readonly List<ITask> _children;


	protected TaskBase(string name, IEnumerable<IAction> actions)
	{
		Name = name;

		Actions = actions.ToList().AsReadOnly();

		_children = new List<ITask>();
		Children = _children.AsReadOnly();
	}


	public string Name { get; }

	public IEnumerable<IAction> Actions { get; }

	public IEnumerable<ITask> Children { get; }


	public void AddChild(ITask child) => _children.Add(child);

	public abstract void Execute();

	public override string ToString() => Name;
}