using Make.Application;
using Make.ImportTasks.FromFile;
using Microsoft.Extensions.DependencyInjection;

using TTask = System.Threading.Tasks.Task;

namespace Make.ConsoleApp;

internal class Program
{
	public static async TTask Main(string[] args)
	{
		args = new[] { "makefile.txt", "Target_05" };

		try
		{
			Application application = InitializeApplication();
			await application.Run(args);
		}
		catch (Exception ex)
		{
			Console.WriteLine(ex);
		}

		Console.ReadKey();
	}


	private static Application InitializeApplication()
	{
		try
		{
			ServiceProvider serviceProvider = ConfigureServices();
			return serviceProvider.GetRequiredService<Application>();
		}
		catch (Exception ex)
		{
			throw new ApplicationException("Не удалось инициализировать приложение.", ex);
		}
	}

	private static ServiceProvider ConfigureServices()
	{
		var services = new ServiceCollection();
		return services
			.AddApplication()
			.AddImporter()
			.AddSingleton<Application>()
			.BuildServiceProvider();
	}
}