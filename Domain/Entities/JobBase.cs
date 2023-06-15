namespace Make.Domain.Entities;

public abstract class JobBase : IJob
{
	public long Id { get; set; }

	public string Name { get; set; } = string.Empty;

	public ICollection<IOperation> Operations { get; set; } = new List<IOperation>();

	public ICollection<IJob> Children { get; set; } = new List<IJob>();


	public abstract void Execute();

	public override string ToString() => Name;
}