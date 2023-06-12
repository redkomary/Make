namespace Make.Domain.Entities;

public interface IExecutor<in TWork>
	where TWork : IWork
{
	public void Execute(TWork action);
}