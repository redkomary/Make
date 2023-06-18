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
	private readonly JobInfoParser _parser  = new();
	private readonly JobsCreator _creator   = new();


	/// <inheritdoc />
	public IEnumerable<IJob> ImportFrom(FilePathDataSource filePathSource, CancellationToken cancellationToken)
	{
		IEnumerable<IReadOnlyList<string>> blocks = _fileReader.Read(filePathSource.FilePath, cancellationToken);
		IEnumerable<JobInfo> jobInfos = ParseJobs(blocks, cancellationToken);
		return _creator.Create(jobInfos, cancellationToken);
	}


	private IEnumerable<JobInfo> ParseJobs(IEnumerable<IReadOnlyList<string>> blocks, CancellationToken cancellationToken)
	{
		IEnumerable<Task<JobInfo>> jobParsingTasks = blocks.Select(block => RunJobParsing(block, cancellationToken));
		Task<JobInfo[]> parseJobInfosTask = Task.WhenAll(jobParsingTasks);

		return parseJobInfosTask.Result;
	}

	private Task<JobInfo> RunJobParsing(IReadOnlyList<string> block, CancellationToken cancellationToken)
	{
		return Task.Run(() => _parser.Parse(block), cancellationToken);
	}
}