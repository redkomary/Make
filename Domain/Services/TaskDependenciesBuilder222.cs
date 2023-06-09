using Domain.Entities;

namespace Domain.Services;

internal class TaskDependenciesBuilder222
{
	private readonly Dictionary<ITask, TaskDependencies> _cache = new();
	private readonly Dictionary<ITask, ProcessingStatus> _processing = new();


	public IEnumerable<TaskDependencies> Build(ITask task)
	{
		BuildRecursively(task);
		return _cache.Values;
	}


	private bool HasCycles(ITask v)
	{
		_processing[v] = ProcessingStatus.InProgress;

		foreach (ITask child in v.Children)
		{
			if (_processing[child] == ProcessingStatus.NotStarted)
			{
				HasCycles(child);
			}
			else if (_processing[child] == ProcessingStatus.InProgress)
			{
				return true;
			}
		}


		//for (size_t i = 0; i < g[v].size(); ++i)
		//{
		//	int to = g[v][i];
		//	if (used[to] == 0)
		//	{
		//		p[to] = v;
		//		dfs(to);
		//	}
		//	else if (used[to] == 1)
		//		cycle = true;
		//}
		_processing[v] = ProcessingStatus.Done;
		return false;
	}

	private void BuildRecursively(ITask task)
	{
		TaskDependencies dependencies;
		if (!_cache.TryAdd(task, dependencies = new TaskDependencies(task)))
			return;

		foreach (ITask childTask in task.Children)
		{
			dependencies.OutComing.Add(childTask);

			BuildRecursively(childTask);

			_cache[childTask].InComing.Add(task);
		}
	}


	private enum ProcessingStatus
	{
		NotStarted,
		InProgress,
		Done
	}
}