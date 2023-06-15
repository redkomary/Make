using Make.Application.API;
using Make.Application.Import.Controllers;
using Make.Application.RunJob.Controllers;
using Microsoft.Extensions.DependencyInjection;

namespace Make.Application.DependencyInjection;

public static class DependencyInjection
{
	public static IServiceCollection AddApplication(this IServiceCollection services)
	{
		return services
			.AddTransient<IImportJobsFromFileController, ImportJobsFromFileController >()
			.AddTransient<IRunJobController , RunJobController>();
	}
}