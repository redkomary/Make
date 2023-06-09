using Domain.Entities;

namespace Domain.Services;

internal class TaskDependenciesBuilder
{
	public TaskDependencies Build(ITask task)
	{
		var cache = new Dictionary<ITask, TaskDependencies>();
		BuildRecursively(task, cache);
		return cache[task];
	}


	private static void BuildRecursively(ITask task, IDictionary<ITask, TaskDependencies> cache)
	{
		TaskDependencies dependencies;
		if (!cache.TryAdd(task, dependencies = new TaskDependencies(task)))
			return;

		foreach (ITask childTask in task.Children)
		{
			dependencies.Descendants.Add(childTask);

			BuildRecursively(childTask, cache);

			cache[childTask].Ancestors.Add(task);
		}
	}
}