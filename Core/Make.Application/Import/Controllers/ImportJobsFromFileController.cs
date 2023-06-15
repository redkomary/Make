using Make.Application.API;
using Make.DataAccess;
using Make.Domain.Entities;
using Make.ImportJobs;
using Make.ImportJobs.FromFile;
using Make.Utilities;

namespace Make.Application.Import.Controllers;

/// <summary>
/// Контроллер, предоставляющий методы для инструмента импорта задач из файла.
/// </summary>
public class ImportJobsFromFileController : IImportJobsFromFileController
{
	private readonly IJobsImporter<FilePathDataSource> _importer;
	private readonly IRepository<IJob> _jobRs;
	private readonly IRepository<IOperation> _operationRs;


	/// <summary>
	/// Конструктор.
	/// </summary>
	/// <param name="importer">Сервис импорта задач.</param>
	/// <param name="jobRs">Репозиторий задач.</param>
	/// <param name="operationRs">Репозиторий действий.</param>
	public ImportJobsFromFileController(
		IJobsImporter<FilePathDataSource> importer,
		IRepository<IJob> jobRs,
		IRepository<IOperation> operationRs)
	{
		_importer = importer;
		_jobRs = jobRs;
		_operationRs = operationRs;
	}


	/// <inheritdoc />
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