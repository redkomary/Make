using Make.Domain.Entities;

namespace Make.ImportTasks;

public interface ITasksImporter<in TDataSource>
	where TDataSource : IDataSource
{
	public IEnumerable<ITask> ImportFrom(TDataSource source);
}