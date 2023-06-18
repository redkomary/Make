using Make.Application.Import.Entities;
using Make.Domain.Entities;


namespace Make.Application.Import.Services;

internal class JobDependenciesBuilder
{
	private readonly IDictionary<IJob, Vertex> _cache = new Dictionary<IJob, Vertex>();


	public IEnumerable<JobDependencies> Build(IJob job)
	{
		BuildRecursively(job);
		return _cache.Values.Select(vertex => vertex.Job);
	}


	private void BuildRecursively(IJob job)
	{
		var vertex = new Vertex(job);
		_cache[job] = vertex;
		vertex.ProcessingStatus = ProcessingStatus.InProgress;

		foreach (IJob subJob in job.SubJobs)
		{
			bool cycleFound = _cache.TryGetValue(subJob, out Vertex? subJobVertex) &&
			                  subJobVertex.ProcessingStatus == ProcessingStatus.InProgress;

			if (cycleFound)
			{
				throw new InvalidOperationException($"Невозможно выполнить задачу \"{job.Name}\", " +
				                                    $"из-за циклической ссылки на задачу \"{subJob.Name}\".");
			}

			vertex.AddOutComingJob(subJob);

			if (subJobVertex == null || subJobVertex.ProcessingStatus == ProcessingStatus.NotStarted)
			{
				BuildRecursively(subJob);
			}

			_cache[subJob].AddInComingJob(job);
		}

		_cache[job].ProcessingStatus = ProcessingStatus.Done;
	}


	private enum ProcessingStatus
	{
		NotStarted,
		InProgress,
		Done
	}

	private class Vertex
	{
		public Vertex(IJob job)
		{
			Job = new JobDependencies(job);
		}


		public JobDependencies Job { get; }

		public ProcessingStatus ProcessingStatus { get; set; }


		public void AddOutComingJob(IJob job) => Job.OutComing.Add(job);

		public void AddInComingJob(IJob job) => Job.InComing.Add(job);
	}
}