using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _5_lab
{
	public class SyncFacade
	{
		private IFileSystem _sourceFS;
		private IFileSystem _targetFS;

		public SyncFacade(IFileSystem source, IFileSystem target)
		{
			_sourceFS = source;
			_targetFS = target;
		}

		public void SyncFolder(string sourcePath, string targetPath)
		{
			var items = _sourceFS.ListItems(sourcePath);

			foreach (var item in items)
			{
				string sourceItemPath = sourcePath + "/" + item;
				string targetItemPath = targetPath + "/" + item;

				try
				{
					byte[] data = _sourceFS.ReadFile(sourceItemPath);
					_targetFS.WriteFile(targetItemPath, data);
					Console.WriteLine($" + {item} синхронизирован");
				}
				catch
				{
					Console.WriteLine($" - {item} пропущен (папка)");
				}
			}
		}

		public void Backup(string sourcePath, string backupPath)
		{
			try
			{
				var items = _sourceFS.ListItems(sourcePath);

				foreach (var item in items)
				{
					string sourceItemPath = sourcePath + "/" + item;
					string backupItemPath = backupPath + "/" + item;

					byte[] data = _sourceFS.ReadFile(sourceItemPath);
					_targetFS.WriteFile(backupItemPath, data);
				}

				Console.WriteLine("Резервное копирование завершено");
			}
			catch (Exception ex)
			{
				Console.WriteLine($"Ошибка при резервном копировании: {ex.Message}");
			}
		}
	}
}