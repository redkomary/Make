using Make.Application.API;
using Make.Application.RunJob.Entities;
using Make.Application.RunJob.Services;
using Make.DataAccess;
using Make.Domain.Entities;

namespace Make.Application.RunJob.Controllers;

public class RunJobController : IRunJobController
{
	private readonly IRepository<IJob> _jobRs;
	private readonly JobDependenciesBuilder _dependenciesBuilder = new();
	private readonly JobsRunner _runner = new();


	public RunJobController(IRepository<IJob> jobRs)
	{
		_jobRs = jobRs;
	}


	public async Task Run(string jobName, CancellationToken cancellationToken)
	{
		IJob job = _jobRs.GetAll().FirstOrDefault(job => job.Name == jobName) ??
			throw new KeyNotFoundException($"Задача \"{jobName}\" не найдена.");

		IEnumerable<JobDependencies> jobsWithDependencies = _dependenciesBuilder.Build(job);
		await _runner.Run(jobsWithDependencies, cancellationToken);
	}
}