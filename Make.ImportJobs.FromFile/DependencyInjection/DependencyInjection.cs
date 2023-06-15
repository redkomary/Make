using Microsoft.Extensions.DependencyInjection;

namespace Make.ImportJobs.FromFile.DependencyInjection;

public static class DependencyInjection
{
	public static IServiceCollection AddImporter(this IServiceCollection services)
	{
		return services
			.AddTransient<IJobsImporter<FilePathDataSource>, JobsImporterFromFile>();
	}
}