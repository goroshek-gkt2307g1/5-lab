using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _5_lab
{

	public interface IFileSystem
	{
		List<string> ListItems(string path);
		byte[] ReadFile(string path);
		void WriteFile(string path, byte[] data);
		void DeleteItem(string path);
	}

	public class FileSystemAdapter : IFileSystem
	{
		private FileSystemItem _root;

		public FileSystemAdapter(FileSystemItem root)
		{
			_root = root;
		}

		public FileSystemItem FindItem(string path)
		{
			if (path == null)
				return _root;
			else
			{
				string[] parts = path.Split('/');
				FileSystemItem currentItem = _root;
				foreach (string part in parts)
				{
					if (string.IsNullOrEmpty(part)) continue;
					else if (currentItem == null) continue;

					else if (currentItem is File )
					{
						return null;
					}

					else if (currentItem is Folder)
					{
						FileSystemItem foundItem = null;
						Folder currentFolder = (Folder)currentItem;
						foreach (FileSystemItem childItem in currentFolder.items)
						{
							if (childItem.Name == part)
							{
								foundItem = childItem; 
								break;
							}
                        }
						if (foundItem == null)
						{
							return null;
						}
						else
						{
							currentItem = foundItem; 
						}

					}

				}
				return currentItem;
			}
		}

		public List<string> ListItems(string path)
		{
			FileSystemItem obj = FindItem(path);
			if (obj == null)
			{
				throw new NotImplementedException();
			}

			if (obj is File)
			{
				throw new NotImplementedException();
			}

			Folder folder = (Folder)obj;
			List<string> result = new List<string>();
			foreach (FileSystemItem childItem in folder.items)
			{
				result.Add(childItem.Name);
			}
			return result;			
		}

		public byte[] ReadFile(string path)
		{
			FileSystemItem obj = FindItem(path);

			if (obj == null)
			{
				throw new Exception($"Файл по пути {path} не найден");
			}

			if (obj is Folder)
			{
				throw new Exception($"По пути {path} находится папка, а не файл");
			}

			File file = (File)obj;
			long size = file.GetSize();

			byte[] data = new byte[size];
			for (int i = 0; i < size; i++)
			{
				data[i] = 1;
			}

			Console.WriteLine($"Чтение файла {path} размером {size} байт");
			return data;
		}

		public void WriteFile(string path, byte[] data)
		{
			FileSystemItem existingItem = FindItem(path);

			//перезапись
			if (existingItem != null)
			{
				if (existingItem is Folder)
				{
					throw new Exception($"Ошибка. По пути {path} находится папка, нельзя записать файл поверх папки");
				}

				Console.WriteLine($"Перезапись файла {path} данными длиной {data.Length} байт");
				return;
			}

			//создание нового файла
			int lastSlashIndex = path.LastIndexOf('/');

			string folderPath;
			string fileName;

			if (lastSlashIndex == -1)
			{
				folderPath = "/";
				fileName = path;
			}
			else
			{
				folderPath = path.Substring(0, lastSlashIndex);
				fileName = path.Substring(lastSlashIndex + 1);
			}

			FileSystemItem folderItem = FindItem(folderPath);

			if (folderItem == null)
			{
				throw new Exception($"Папка {folderPath} не найдена");
			}

			if (folderItem is File)
			{
				throw new Exception($"По пути {folderPath} находится файл, а должна быть папка");
			}

			Folder targetFolder = (Folder)folderItem;

			foreach (FileSystemItem child in targetFolder.items)
			{
				if (child.Name == fileName)
				{
					throw new Exception($"В папке {folderPath} уже есть элемент с именем {fileName}");
				}
			}

			File newFile = new File(fileName, data.Length);
			targetFolder.Add(newFile);

			Console.WriteLine($"Создан новый файл {path} размером {data.Length} байт");
		}

		public void DeleteItem(string path)
		{
			if (path == "/" || path == "" || path == null)
			{
				throw new Exception("Нельзя удалить корневую папку");
			}

			FileSystemItem itemToDelete = null;
			Folder parentFolder = null;

			string[] parts = path.Split('/');
			FileSystemItem current = _root;
			Folder currentParent = null;

			foreach (string part in parts)
			{
				if (string.IsNullOrEmpty(part)) continue;

				if (current is File)
				{
					throw new Exception($"Невозможно пройти по пути, так как {current.Name} - файл");
				}

				Folder currentFolder = (Folder)current;
				FileSystemItem nextItem = null;

				foreach (FileSystemItem child in currentFolder.items)
				{
					if (child.Name == part)
					{
						nextItem = child;
						break;
					}
				}

				if (nextItem == null)
				{
					throw new Exception($"Элемент {part} не найден в пути {path}");
				}

				currentParent = currentFolder;
				current = nextItem;
			}

			//current - это удаляемый элемент, а currentParent - его родитель
			itemToDelete = current;
			parentFolder = currentParent;

			if (parentFolder == null)
			{
				throw new Exception("Не удалось определить родительскую папку");
			}

			parentFolder.Remove(itemToDelete);
			Console.WriteLine($"Удален элемент {path}");
		}
	}
}
