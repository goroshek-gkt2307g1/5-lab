using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _5_lab
{
	internal class Program
	{
		static void Main(string[] args)
		{
			File file1 = new File("document.txt", 150);
			File file2 = new File("photo.jpg", 1024);
			File file3 = new File("music.mp3", 5120);
			File file4 = new File("video.mp4", 20480);

			Folder documents = new Folder("Документы");
			Folder media = new Folder("Медиа");
			Folder root = new Folder("Корень");

			documents.Add(file1);

			media.Add(file2);
			media.Add(file3);
			media.Add(file4);

			root.Add(documents);
			root.Add(media);

			Console.WriteLine($"размер папки 'Документы': {documents.GetSize()} байт");
			Console.WriteLine($"размер папки 'Медиа': {media.GetSize()} байт");
			Console.WriteLine($"размер корневой папки: {root.GetSize()} байт");

			//адаптер

			FileSystemAdapter adapter = new FileSystemAdapter(root);

			Console.WriteLine("\nСодержимое корня:");
			foreach (string item in adapter.ListItems("/"))
				Console.WriteLine($"  - {item}");

			Console.WriteLine("\nСодержимое папки Медиа:");
			foreach (string item in adapter.ListItems("/Медиа"))
				Console.WriteLine($"  - {item}");

			byte[] fileData = adapter.ReadFile("/Медиа/photo.jpg");
			Console.WriteLine($"\nПрочитан файл photo.jpg: {fileData.Length} байт");

			byte[] newData = new byte[256];
			adapter.WriteFile("/Документы/newfile.txt", newData);
			Console.WriteLine("Создан файл newfile.txt");

			adapter.DeleteItem("/Документы/newfile.txt");
			Console.WriteLine("Файл newfile.txt удален\n");

			//фасад
			Folder targetRoot = new Folder("Target");
			FileSystemAdapter targetAdapter = new FileSystemAdapter(targetRoot);

			SyncFacade facade = new SyncFacade(adapter, targetAdapter);

			facade.SyncFolder("/Медиа", "/");

			Console.WriteLine("Содержимое target после синхронизации:");
			foreach (string item in targetAdapter.ListItems("/"))
				Console.WriteLine($"     - {item}");

			Console.ReadLine();


		}
	}
}