using Make.Domain.Entities;
using Make.ImportTasks.FromFile.Entities;
using Make.ImportTasks.FromFile.Services;

namespace Make.ImportTasks.FromFile;

public class TasksImporterFromFile : ITasksImporter<FilePathDataSource>
{
	private readonly FileReader _fileReader = new();
	private readonly TaskInfoParser _parser = new();
	private readonly TasksCreator _creator  = new();


	public IEnumerable<ITask> ImportFrom(FilePathDataSource filePathSource)
	{
		IEnumerable<IReadOnlyList<string>> blocks = _fileReader.Read(filePathSource.FilePath);
		IEnumerable<TaskInfo> taskInfos = blocks.Select(_parser.Parse);
		return _creator.Create(taskInfos);
	}
}