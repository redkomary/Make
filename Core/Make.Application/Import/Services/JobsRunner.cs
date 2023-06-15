using System.Collections.Concurrent;
using Make.Application.Import.Entities;
using Make.Domain.Entities;
using Make.Utilities;

namespace Make.Application.Import.Services;

internal class JobsRunner
{
	private readonly IDictionary<IJob, JobDependencies> _allJobs = new Dictionary<IJob, JobDependencies>();
	private readonly ConcurrentQueue<JobDependencies> _queue = new();


	public async Task Run(IEnumerable<JobDependencies> jobs, CancellationToken cancellationToken)
	{
		jobs.ForEach(t => _allJobs.Add(t.Job, t));
		await Run(cancellationToken);
	}


	private async Task Run(CancellationToken cancellationToken)
	{
		int jobsCount = _allJobs.Count;
		var runningTasks = new HashSet<Task>();

		_allJobs.Values
			.Where(job => job.ReadyToExecute)
			.ForEach(_queue.Enqueue);

		while (jobsCount > 0 || !_queue.IsEmpty)
		{
			while (_queue.TryDequeue(out JobDependencies? jobDependencies))
			{
				Task runningTask = RunJob(jobDependencies, cancellationToken);
				runningTasks.Add(runningTask);
			}

			Task completedTask = await Task.WhenAny(runningTasks);
			runningTasks.Remove(completedTask);

			jobsCount--;
		}

		await Task.WhenAll(runningTasks);
	}

	private async Task RunJob(JobDependencies jobDependencies, CancellationToken cancellationToken)
	{
		IJob job = jobDependencies.Job;

		await Task.Run(job.Execute, cancellationToken);

		// Задача выполнена -> больше от неё никто не зависит.
		foreach (IJob ancestor in jobDependencies.InComing)
		{
			JobDependencies ancestorDependencies = _allJobs[ancestor];
			ancestorDependencies.OutComing.Remove(job);

			if (ancestorDependencies.ReadyToExecute)
				_queue.Enqueue(ancestorDependencies);
		}
	}
}