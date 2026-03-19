using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _5_lab
{
	public abstract class FileSystemItem
	{
		public string Name { get; set; }

		public FileSystemItem(string name)
		{
			Name = name;
		}

		public abstract long GetSize();
		public virtual void Add(FileSystemItem item)
		{

		}
		public virtual void Remove(FileSystemItem item)
		{

		}
		public virtual FileSystemItem GetChild(int index)
		{
			return null;
		}
	}
}
