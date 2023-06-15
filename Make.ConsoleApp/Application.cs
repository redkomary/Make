using Make.Application.API;

namespace Make.ConsoleApp;

internal class Application
{
	private readonly IImportJobsFromFileController _importJobsController;
	private readonly IRunJobController _runJobController;


	public Application(
		IImportJobsFromFileController importJobsController,
		IRunJobController runJobController)
	{
		_importJobsController = importJobsController;
		_runJobController = runJobController;
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
		string targetJobName = args[1];

		_importJobsController.Import(filePath);
		await _runJobController.Run(targetJobName, CancellationToken.None);
	}
}