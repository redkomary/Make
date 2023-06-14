using Make.Application.Import.Controllers;
using Make.Application.RunTask.Controllers;

namespace Make.ConsoleApp;

internal class Application
{
	private readonly ImportTasksController _importTasksController;
	private readonly RunTaskController _runTaskController;


	public Application(ImportTasksController importTasksController, RunTaskController runTaskController)
	{
		_importTasksController = importTasksController;
		_runTaskController = runTaskController;
	}


	public async Task Run(string[] args)
	{
		if (args.Length < 2)
		{
			throw new ArgumentOutOfRangeException(nameof(args),
				$"Ошибка запуска приложения. Приложению должен быть передан список параметров: {Environment.NewLine}" +
				$" - путь к файлу импорта {Environment.NewLine}" +
				$" - наименование задачи для выполнения");
		}

		string filePath = args[0];
		string targetTaskName = args[1];

		_importTasksController.Import(filePath);
		await _runTaskController.Run(targetTaskName, CancellationToken.None);
	}
}