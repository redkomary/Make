using Make.Domain.Entities;
using Make.ImportJobs.FromFile.Entities;
using Make.ImportJobs.FromFile.Services;

namespace Make.ImportJobs.FromFile;

/// <summary>
/// Сервис импорта задач.
/// </summary>
public class JobsImporterFromFile : IJobsImporter<FilePathDataSource>
{
	private readonly FileReader _fileReader = new();
	private readonly JobInfoParser _parser = new();
	private readonly JobsCreator _creator  = new();


	/// <inheritdoc />
	public IEnumerable<IJob> ImportFrom(FilePathDataSource filePathSource)
	{
		IEnumerable<IReadOnlyList<string>> blocks = _fileReader.Read(filePathSource.FilePath);
		IEnumerable<JobInfo> jobInfos = blocks.Select(_parser.Parse);
		return _creator.Create(jobInfos);
	}
}