using Make.Application.RunJob.Entities;
using Make.Domain.Entities;

namespace Make.Application.RunJob.Services;

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

		foreach (IJob childJob in job.Children)
		{
			bool cycleFound = _cache.TryGetValue(childJob, out Vertex? childJobVertex) &&
			                  childJobVertex.ProcessingStatus == ProcessingStatus.InProgress;

			if (cycleFound)
			{
				throw new InvalidOperationException($"Невозможно выполнить задачу \"{job.Name}\", " +
				                                    $"из-за циклической ссылки на задачу \"{childJob.Name}\".");
			}

			vertex.AddOutComingJob(childJob);

			if (childJobVertex == null || childJobVertex.ProcessingStatus == ProcessingStatus.NotStarted)
			{
				BuildRecursively(childJob);
			}

			_cache[childJob].AddInComingJob(job);
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