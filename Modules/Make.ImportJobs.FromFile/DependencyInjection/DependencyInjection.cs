using Microsoft.Extensions.DependencyInjection;


namespace Make.ImportJobs.FromFile.DependencyInjection;

/// <summary>
/// Сервис внедрения зависимостей.
/// </summary>
public static class DependencyInjection
{
	/// <summary>
	/// Добавляет зависимости модуля.
	/// </summary>
	public static IServiceCollection AddImporter(this IServiceCollection services)
	{
		return services
			.AddTransient<IJobsImporter<FilePathDataSource>, JobsImporterFromFile>();
	}
}