namespace Make.Application.API;

public interface IRunTaskController
{
	public Task Run(string taskName, CancellationToken cancellationToken);
}