using Domain.Entities;

namespace Make.ImportTasks;

public interface ITasksImporter
{
	public IAsyncEnumerable<ITask> Import();
}