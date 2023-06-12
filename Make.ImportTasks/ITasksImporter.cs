using Make.Domain.Entities;

namespace Make.ImportTasks;

public interface ITasksImporter
{
	public IEnumerable<ITask> Import(string filePath);
}