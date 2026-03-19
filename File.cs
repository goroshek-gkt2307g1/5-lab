using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _5_lab
{
	public class File : FileSystemItem
	{
		private long _size;

		public File(string name, long size) : base(name)
		{
			_size = size;
		}

		public override long GetSize()
		{
			return _size;
		}
		public override void Add(FileSystemItem item)
		{
			throw new NotImplementedException();
		}
		public override void Remove(FileSystemItem item)
		{
			throw new NotImplementedException();
		}
		public override FileSystemItem GetChild(int index)
		{
			throw new NotImplementedException();
		}
	}
}
