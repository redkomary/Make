using Make.Application.API;
using Make.Application.Import.Controllers;
using Make.Application.RunJob.Controllers;
using Microsoft.Extensions.DependencyInjection;

namespace Make.Application.DependencyInjection;

/// <summary>
/// Сервис внедрения зависимостей.
/// </summary>
public static class DependencyInjection
{
	/// <summary>
	/// Добавляет зависимости модуля.
	/// </summary>
	public static IServiceCollection AddApplication(this IServiceCollection services)
	{
		return services
			.AddTransient<IImportJobsFromFileController, ImportJobsFromFileController >()
			.AddTransient<IRunJobController , RunJobController>();
	}
}