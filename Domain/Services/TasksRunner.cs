using System.Collections.Concurrent;
using Domain.Entities;
using Make.Utilities;

using TTask = System.Threading.Tasks.Task;

namespace Domain.Services;

internal class TasksRunner
{
	private readonly IDictionary<ITask, TaskDependencies> _allTasks = new Dictionary<ITask, TaskDependencies>();
	private readonly ConcurrentQueue<TaskDependencies> _queue = new();


	public TTask Run(IEnumerable<TaskDependencies> tasks)
	{
		tasks.ForEach(t => _allTasks.Add(t.Task, t));
		return Run();
	}


	private async TTask Run()
	{
		int tasksCount = _allTasks.Count;
		var runningTasks = new HashSet<TTask>();

		_allTasks.Values
			.Where(task => task.ReadyToExecute)
			.ForEach(_queue.Enqueue);

		while (tasksCount > 0 || !_queue.IsEmpty)
		{
			while (_queue.TryDequeue(out TaskDependencies? taskDependencies))
			{
				TTask runningTask = RunTask(taskDependencies);
				runningTasks.Add(runningTask);
			}

			TTask completedTask = await TTask.WhenAny(runningTasks);
			runningTasks.Remove(completedTask);

			Interlocked.Decrement(ref tasksCount);
		}

		await TTask.WhenAll(runningTasks);
	}

	private async TTask RunTask(TaskDependencies taskDependencies)
	{
		ITask task = taskDependencies.Task;

		await TTask.Run(task.Execute);

		// Задача выполнена -> больше от неё никто не зависит.
		foreach (ITask ancestor in taskDependencies.Ancestors)
		{
			TaskDependencies ancestorDependencies = _allTasks[ancestor];
			ancestorDependencies.Descendants.Remove(task);

			if (ancestorDependencies.ReadyToExecute)
				_queue.Enqueue(ancestorDependencies);
		}
	}
}