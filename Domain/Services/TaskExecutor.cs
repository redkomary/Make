using Domain.Entities;
using Task = System.Threading.Tasks.Task;

namespace Domain.Services;

public class TaskExecutor
{
	private readonly TaskDependenciesBuilder _taskDependenciesBuilder = new();
	private readonly TasksRunner _runner = new();


	public async Task Execute(ITask task, CancellationToken cancellationToken)
	{
		IEnumerable<TaskDependencies> tasksWithDependencies = _taskDependenciesBuilder.Build(task);
		await _runner.Run(tasksWithDependencies, cancellationToken);
	}
}