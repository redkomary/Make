using Make.Domain.Entities;

namespace Make.ImportJobs;

public interface IJobsImporter<in TDataSource>
	where TDataSource : IDataSource
{
	public IEnumerable<IJob> ImportFrom(TDataSource source);
}