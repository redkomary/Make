using Make.Application.RunTask.Entities;
using Make.Domain.Entities;

namespace Make.Application.RunTask.Services;

internal class TaskDependenciesBuilder
{
	private readonly IDictionary<ITask, Vertex> _cache = new Dictionary<ITask, Vertex>();


	public IEnumerable<TaskDependencies> Build(ITask task)
	{
		BuildRecursively(task);
		return _cache.Values.Select(vertex => vertex.Task);
	}


	private void BuildRecursively(ITask task)
	{
		var vertex = new Vertex(task);
		_cache[task] = vertex;
		vertex.ProcessingStatus = ProcessingStatus.InProgress;

		foreach (ITask childTask in task.Children)
		{
			bool cycleFound = _cache.TryGetValue(childTask, out Vertex? childTaskVertex) &&
			                  childTaskVertex.ProcessingStatus == ProcessingStatus.InProgress;

			if (cycleFound)
			{
				throw new InvalidOperationException($"Невозможно выполнить задачу \"{task.Name}\", " +
				                                    $"из-за циклической ссылки на задачу \"{childTask.Name}\".");
			}

			vertex.AddOutComingTask(childTask);

			if (childTaskVertex == null || childTaskVertex.ProcessingStatus == ProcessingStatus.NotStarted)
			{
				BuildRecursively(childTask);
			}

			_cache[childTask].AddInComingTask(task);
		}

		_cache[task].ProcessingStatus = ProcessingStatus.Done;
	}


	private enum ProcessingStatus
	{
		NotStarted,
		InProgress,
		Done
	}

	private class Vertex
	{
		public Vertex(ITask task)
		{
			Task = new TaskDependencies(task);
		}

		public TaskDependencies Task { get; }

		public ProcessingStatus ProcessingStatus { get; set; }


		public void AddOutComingTask(ITask task) => Task.OutComing.Add(task);

		public void AddInComingTask(ITask task) => Task.InComing.Add(task);
	}
}