using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Releasing_resources
{
	class Program
	{
		static void Main(string[] args)
		{
			string unmanagedFile = @"C:\unmanaged\unmanagedFile.docx";

			SafeFileWriter fileWriter = new SafeFileWriter(unmanagedFile);
			StringBuilder text = new StringBuilder("unmanaged resources");

			fileWriter.Write(text);
		}
	}
}
