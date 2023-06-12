using Make.Domain.Entities;
using Make.ImportTasks.FromFile.Entities;
using Make.Utilities;

using Action = Make.Domain.Entities.Impl.Action;
using Task   = Make.Domain.Entities.Impl.Task;

namespace Make.ImportTasks.FromFile.Services;

internal class TasksCreator
{
	private readonly IDictionary<string, Task> _tasks = new Dictionary<string, Task>(StringComparer.InvariantCultureIgnoreCase);
	private readonly List<DependencyInfo> _dependencies = new();


	public IEnumerable<ITask> Create(IEnumerable<TaskInfo> taskInfos)
	{
		CreateTasks(taskInfos);
		LinkTasks();

		return _tasks.Values;
	}

	private void CreateTasks(IEnumerable<TaskInfo> taskInfos)
	{
		foreach (TaskInfo taskInfo in taskInfos)
		{
			if (_tasks.ContainsKey(taskInfo.Header.Name))
				throw new InvalidOperationException($"Задача \"{taskInfo.Header.Name}\" уже загружена. Наименование задачи должно быть уникальным.");

			Task task = CreateTask(taskInfo);
			_tasks[task.Name] = task;

			IEnumerable<DependencyInfo> taskDependencies = CreateTaskDependencies(taskInfo.Header);
			_dependencies.AddRange(taskDependencies);
		}
	}

	private Task CreateTask(TaskInfo taskInfo)
	{
		IEnumerable<IAction> actions = taskInfo.Actions.Select(CreateAction);
		return new Task(taskInfo.Header.Name, actions);
	}

	private IAction CreateAction(ActionInfo actionInfo)
	{
		return new Action(actionInfo.Name);
	}


	private IEnumerable<DependencyInfo> CreateTaskDependencies(TaskHeaderInfo taskHeader)
	{
		return taskHeader.Dependencies
			.Select(dependencyName => new DependencyInfo(taskHeader.Name, dependencyName));
	}

	private void LinkTasks()
	{
		foreach (DependencyInfo dependencyInfo in _dependencies)
		{
			Task dependentTask = _tasks[dependencyInfo.DependentTaskName];

			Task subTask = _tasks.GetValueOrDefault(dependencyInfo.SubTaskName) ??
				throw new KeyNotFoundException($"Задача \"{dependencyInfo.DependentTaskName}\" зависит от несуществующей задачи \"{dependencyInfo.SubTaskName}\".");

			dependentTask.AddChild(subTask);
		}
	}


	private class DependencyInfo
	{
		public DependencyInfo(string dependentTaskName, string subTaskName)
		{
			DependentTaskName = dependentTaskName;
			SubTaskName = subTaskName;
		}

		/// <summary>
		/// Наименование зависимой задачи.
		/// </summary>
		public string DependentTaskName { get; }

		/// <summary>
		/// Наименование задачи, от которой зависит <see cref="DependentTaskName"/>.
		/// </summary>
		public string SubTaskName { get; }
	}
}