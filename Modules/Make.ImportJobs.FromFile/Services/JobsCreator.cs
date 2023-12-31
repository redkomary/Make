﻿using Make.Domain.Entities;
using Make.Domain.Entities.Impl;
using Make.ImportJobs.FromFile.Entities;
using Make.Utilities;


namespace Make.ImportJobs.FromFile.Services;

internal class JobsCreator
{
	private readonly IDictionary<string, IJob> _jobs = new Dictionary<string, IJob>(StringComparer.InvariantCultureIgnoreCase);
	private readonly List<DependencyInfo> _dependencies = new();


	public IEnumerable<IJob> Create(IEnumerable<JobInfo> jobInfos, CancellationToken cancellationToken)
	{
		CreateJobs(jobInfos, cancellationToken);
		LinkJobs(cancellationToken);

		return _jobs.Values;
	}

	private void CreateJobs(IEnumerable<JobInfo> jobInfos, CancellationToken cancellationToken)
	{
		foreach (JobInfo jobInfo in jobInfos)
		{
			if (cancellationToken.IsCancellationRequested)
				throw new OperationCanceledException();

			if (_jobs.ContainsKey(jobInfo.Header.Name))
				throw new InvalidOperationException($"Задача \"{jobInfo.Header.Name}\" уже загружена. Наименование задачи должно быть уникальным.");

			IJob job = CreateJob(jobInfo);
			_jobs[job.Name] = job;

			IEnumerable<DependencyInfo> jobDependencies = CreateJobDependencies(jobInfo.Header);
			_dependencies.AddRange(jobDependencies);
		}
	}

	private static IJob CreateJob(JobInfo jobInfo)
	{
		IEnumerable<IOperation> operations = jobInfo.Operations.Select(CreateOperation);
		return new PrintToConsoleJob
		{
			Name = jobInfo.Header.Name,
			Operations = operations.ToHashSet()
		};
	}

	private static IOperation CreateOperation(OperationInfo operationInfo)
	{
		return new PrintToConsoleOperation { Name = operationInfo.Name };
	}


	private static IEnumerable<DependencyInfo> CreateJobDependencies(JobHeaderInfo jobHeader)
	{
		return jobHeader.Dependencies
			.Select(dependencyName => new DependencyInfo(jobHeader.Name, dependencyName));
	}

	private void LinkJobs(CancellationToken cancellationToken)
	{
		foreach (DependencyInfo dependencyInfo in _dependencies)
		{
			if (cancellationToken.IsCancellationRequested)
				throw new OperationCanceledException();

			IJob dependentJob = _jobs[dependencyInfo.DependentJobName];

			IJob subJob = _jobs.GetValueOrDefault(dependencyInfo.SubJobName) ??
				throw new KeyNotFoundException($"Задача \"{dependencyInfo.DependentJobName}\" зависит от несуществующей задачи \"{dependencyInfo.SubJobName}\".");

			dependentJob.SubJobs.Add(subJob);
		}
	}


	private class DependencyInfo
	{
		public DependencyInfo(string dependentJobName, string subJobName)
		{
			DependentJobName = dependentJobName;
			SubJobName = subJobName;
		}

		/// <summary>
		/// Наименование зависимой задачи.
		/// </summary>
		public string DependentJobName { get; }

		/// <summary>
		/// Наименование задачи, от которой зависит <see cref="DependentJobName"/>.
		/// </summary>
		public string SubJobName { get; }
	}
}