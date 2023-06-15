namespace Make.Application.API;

public interface IRunJobController
{
	public Task Run(string jobName, CancellationToken cancellationToken);
}