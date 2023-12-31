﻿using Make.Application.API;


namespace Make.ConsoleApp;

internal class Application
{
	private readonly IImportJobsController _importJobsController;


	public Application(IImportJobsController importJobsController)
	{
		_importJobsController = importJobsController;
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

		var cancellationTokenSource = new CancellationTokenSource();
		CancellationToken cancellationToken = cancellationTokenSource.Token;

		try
		{
			_importJobsController.ImportJobsFromFile(filePath, cancellationToken);
			await _importJobsController.RunJob(targetJobName, cancellationToken);
		}
		catch (OperationCanceledException)
		{
			Console.WriteLine("Операция отменена пользователем.");
		}
	}
}