﻿using Make.Application.API;
using Make.DataAccess;
using Make.Domain.Entities;
using Make.ImportTasks;
using Make.ImportTasks.FromFile;
using Make.Utilities;

namespace Make.Application.Import.Controllers;

public class ImportTasksFromFileController : IImportTasksFromFileController
{
	private readonly ITasksImporter<FilePathDataSource> _importer;
	private readonly IRepository<ITask> _taskRs;
	private readonly IRepository<IAction> _actionRs;


	public ImportTasksFromFileController(
		ITasksImporter<FilePathDataSource> importer,
		IRepository<ITask> taskRs,
		IRepository<IAction> actionRs)
	{
		_importer = importer;
		_taskRs = taskRs;
		_actionRs = actionRs;
	}


	public void Import(string filePath)
	{
		var dataSource = new FilePathDataSource(filePath);
		IEnumerable<ITask> tasks = _importer.ImportFrom(dataSource);
		foreach (ITask task in tasks)
		{
			_taskRs.Create(task);
			task.Actions.ForEach(_actionRs.Create);
		}
	}
}