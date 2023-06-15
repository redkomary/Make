namespace Make.ImportJobs.FromFile.Entities;

internal class JobInfo
{
	public JobInfo(JobHeaderInfo header, IEnumerable<OperationInfo> operations)
	{
		Header = header;
		Operations = operations;
	}


	public JobHeaderInfo Header { get; }

	public IEnumerable<OperationInfo> Operations { get; }
}