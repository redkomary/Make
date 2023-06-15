namespace Make.Application.API;

/// <summary>
/// Контроллер, предоставляющий методы выполнения задачи вместе со всеми её зависимостями.
/// </summary>
public interface IRunJobController
{
	/// <summary>
	/// Запускает выполнение задачи и всех её зависимостей.
	/// </summary>
	/// <param name="jobName">Наименование задачи.</param>
	/// <param name="cancellationToken">Токен отмены операции.</param>
	public Task Run(string jobName, CancellationToken cancellationToken);
}