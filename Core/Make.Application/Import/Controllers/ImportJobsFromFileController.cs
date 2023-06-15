using Make.Application.API;
using Make.DataAccess;
using Make.Domain.Entities;
using Make.ImportJobs;
using Make.ImportJobs.FromFile;
using Make.Utilities;

namespace Make.Application.Import.Controllers;

public class ImportJobsFromFileController : IImportJobsFromFileController
{
	private readonly IJobsImporter<FilePathDataSource> _importer;
	private readonly IRepository<IJob> _jobRs;
	private readonly IRepository<IOperation> _operationRs;


	public ImportJobsFromFileController(
		IJobsImporter<FilePathDataSource> importer,
		IRepository<IJob> jobRs,
		IRepository<IOperation> operationRs)
	{
		_importer = importer;
		_jobRs = jobRs;
		_operationRs = operationRs;
	}


	public void Import(string filePath)
	{
		var dataSource = new FilePathDataSource(filePath);
		IEnumerable<IJob> jobs = _importer.ImportFrom(dataSource);
		foreach (IJob job in jobs)
		{
			_jobRs.Create(job);
			job.Operations.ForEach(_operationRs.Create);
		}
	}
}