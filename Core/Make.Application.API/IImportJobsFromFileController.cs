namespace Make.Application.API;

/// <summary>
/// Контроллер, предоставляющий методы для инструмента импорта задач из файла.
/// </summary>
public interface IImportJobsFromFileController
{
	/// <summary>
	/// Загрузить задачи из файла.
	/// </summary>
	/// <param name="filePath">Путь к файлу импорта.</param>
	public void Import(string filePath);

	/// <summary>
	/// Запускает выполнение задачи вместе со всеми её зависимостями.
	/// </summary>
	/// <param name="jobName">Наименование задачи.</param>
	/// <param name="cancellationToken">Токен отмены операции.</param>
	public Task Run(string jobName, CancellationToken cancellationToken);
}