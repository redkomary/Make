using Make.Domain.Entities;
using Make.ImportJobs.FromFile.Entities;
using Make.ImportJobs.FromFile.Services;

namespace Make.ImportJobs.FromFile;

public class JobsImporterFromFile : IJobsImporter<FilePathDataSource>
{
	private readonly FileReader _fileReader = new();
	private readonly JobInfoParser _parser = new();
	private readonly JobsCreator _creator  = new();


	public IEnumerable<IJob> ImportFrom(FilePathDataSource filePathSource)
	{
		IEnumerable<IReadOnlyList<string>> blocks = _fileReader.Read(filePathSource.FilePath);
		IEnumerable<JobInfo> jobInfos = blocks.Select(_parser.Parse);
		return _creator.Create(jobInfos);
	}
}