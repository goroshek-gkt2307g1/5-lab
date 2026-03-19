using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _5_lab
{
	public class Folder : FileSystemItem
	{
		public List<FileSystemItem> items = [];

		public Folder(string name) : base(name) 
		{
		}

		public override long GetSize()
		{
			long sum_size = 0;
			foreach (FileSystemItem item in items)
			{
				sum_size += item.GetSize();
			}
			return sum_size;
		}

		public override void Add(FileSystemItem item)
		{
			items.Add(item);
		}

		public override void Remove(FileSystemItem item)
		{
			items.Remove(item);
		}

		public override FileSystemItem GetChild(int index)
		{
			return items[index];
		}

	}
}
