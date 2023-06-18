using Make.Domain.Entities;


namespace Make.ImportJobs;

/// <summary>
/// Сервис импорта задач.
/// </summary>
/// <typeparam name="TDataSource">Тип источника данных для импорта.</typeparam>
public interface IJobsImporter<in TDataSource>
	where TDataSource : IDataSource
{
	/// <summary>
	/// Загружает задачи из указанного источника.
	/// </summary>
	/// <param name="source">Источник данных для импорта.</param>
	/// <param name="cancellationToken">Токен отмены операции.</param>
	/// <returns>Возвращает список загруженных задач.</returns>
	public IEnumerable<IJob> ImportFrom(TDataSource source, CancellationToken cancellationToken);
}