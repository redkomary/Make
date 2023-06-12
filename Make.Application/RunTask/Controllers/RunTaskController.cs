using Make.Application.RunTask.Entities;
using Make.Application.RunTask.Services;
using Make.Domain.Entities;
using Task = System.Threading.Tasks.Task;

namespace Make.Application.RunTask.Controllers;

public class RunTaskController
{
	private readonly TaskDependenciesBuilder _taskDependenciesBuilder = new();
	private readonly TasksRunner _runner = new();


	public async Task Run(ITask task, CancellationToken cancellationToken)
	{
		IEnumerable<TaskDependencies> tasksWithDependencies = _taskDependenciesBuilder.Build(task);
		await _runner.Run(tasksWithDependencies, cancellationToken);
	}
}