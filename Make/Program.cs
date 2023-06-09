using Domain.Entities;
using Domain.Services;
using Make.ImportTasks.FromFile.Services;
using Action = Domain.Entities.Action;
using Task = Domain.Entities.Task;
using TTask = System.Threading.Tasks.Task;

namespace Make;

internal class Program
{
	public static async TTask Main(string[] args)
	{
		try
		{
			var importer = new TasksImporterFromFile();
			IEnumerable<ITask> tasks = importer.Import("makefile.txt");

			ITask targetTask = tasks.First(t => t.Name == "Target_05");

			var executor = new Domain.Services.TaskExecutor();
			await executor.Execute(targetTask, CancellationToken.None);
		}
		catch (Exception ex)
		{
			Console.WriteLine(ex);
		}

		Console.ReadKey();
	}
}