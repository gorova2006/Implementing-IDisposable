using System;
using System.Runtime.InteropServices;
using System.Security;
using Microsoft.Win32.SafeHandles;

namespace Releasing_resources
{
	public static class FileNativeMethods
	{
		[DllImport("kernel32.dll", EntryPoint = "CreateFile", SetLastError = true)]
		public static extern IntPtr CreateFile(
			string fileName,
			uint dwDesiredAccess,
			uint dwShareMode,
			IntPtr lpSecurityAttributes,
			uint dwCreationDisposition,
			uint dwFlagsAndAttributes,
			IntPtr hTemplateFile);

		[DllImport("kernel32.dll", BestFitMapping = true, CharSet = CharSet.Ansi)]
		public static extern bool WriteFile(
			IntPtr hFile,
			System.Text.StringBuilder lpBuffer,
			uint nNumberOfBytesToWrite,
			out uint lpNumberOfBytesWritten,
			[In] ref System.Threading.NativeOverlapped lpOverlapped);
	}
}