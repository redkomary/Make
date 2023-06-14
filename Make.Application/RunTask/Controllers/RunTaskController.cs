using Make.Application.API;
using Make.Application.RunTask.Entities;
using Make.Application.RunTask.Services;
using Make.DataAccess;
using Make.Domain.Entities;

using Task = System.Threading.Tasks.Task;

namespace Make.Application.RunTask.Controllers;

public class RunTaskController : IRunTaskController
{
	private readonly IRepository<ITask> _taskRs;
	private readonly TaskDependenciesBuilder _taskDependenciesBuilder = new();
	private readonly TasksRunner _runner = new();


	public RunTaskController(IRepository<ITask> taskRs)
	{
		_taskRs = taskRs;
	}


	public async Task Run(string taskName, CancellationToken cancellationToken)
	{
		ITask task = _taskRs.GetAll().FirstOrDefault(task => task.Name == taskName) ??
			throw new KeyNotFoundException($"Задача \"{taskName}\" не найдена.");

		IEnumerable<TaskDependencies> tasksWithDependencies = _taskDependenciesBuilder.Build(task);
		await _runner.Run(tasksWithDependencies, cancellationToken);
	}
}