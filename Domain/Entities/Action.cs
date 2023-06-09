namespace Domain.Entities;

public class Action : IAction
{
	public Action(string name)
	{
		Name = name;
	}


	public string Name { get; }


	public override string ToString() => Name;
}