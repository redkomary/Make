using System.Collections.Concurrent;
using Make.Application.RunTask.Entities;
using Make.Domain.Entities;
using Make.Utilities;
using TTask = System.Threading.Tasks.Task;

namespace Make.Application.RunTask.Services;

internal class TasksRunner
{
	private readonly IDictionary<ITask, TaskDependencies> _allTasks = new Dictionary<ITask, TaskDependencies>();
	private readonly ConcurrentQueue<TaskDependencies> _queue = new();


	public async TTask Run(IEnumerable<TaskDependencies> tasks, CancellationToken cancellationToken)
	{
		tasks.ForEach(t => _allTasks.Add(t.Task, t));
		await Run(cancellationToken);
	}


	private async TTask Run(CancellationToken cancellationToken)
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
				TTask runningTask = RunTask(taskDependencies, cancellationToken);
				runningTasks.Add(runningTask);
			}

			TTask completedTask = await TTask.WhenAny(runningTasks);
			runningTasks.Remove(completedTask);

			tasksCount--;
		}

		await TTask.WhenAll(runningTasks);
	}

	private async TTask RunTask(TaskDependencies taskDependencies, CancellationToken cancellationToken)
	{
		ITask task = taskDependencies.Task;

		await TTask.Run(task.Execute, cancellationToken);

		// Задача выполнена -> больше от неё никто не зависит.
		foreach (ITask ancestor in taskDependencies.InComing)
		{
			TaskDependencies ancestorDependencies = _allTasks[ancestor];
			ancestorDependencies.OutComing.Remove(task);

			if (ancestorDependencies.ReadyToExecute)
				_queue.Enqueue(ancestorDependencies);
		}
	}
}