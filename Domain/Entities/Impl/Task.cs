namespace Make.Domain.Entities.Impl;

public class Task : ITask
{
	private readonly List<ITask> _children;

	public Task(string name)
		: this(name, Enumerable.Empty<IAction>())
	{
	}

	public Task(string name, IEnumerable<IAction> actions)
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


	public override string ToString() => Name;
}