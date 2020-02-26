using System;
using Microsoft.Win32.SafeHandles;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;

namespace Releasing_resources
{
	public class SafeFileWriter : IDisposable
	{
		public const uint GENERIC_READ = 0x80000000;
		public const uint GENERIC_WRITE = 0x40000000;
		public const uint FILE_SHARE_WRITE = 0x00000002;
		public const uint CREATE_NEW = 1;
		public const uint CREATE_ALWAYS = 2;
		public const uint OPEN_EXISTING = 3;
		public const short FILE_ATTRIBUTE_NORMAL = 0x80;
		public const short INVALID_HANDLE_VALUE = -1;

		private SafeFileHandle safeHandle;
		IntPtr ptr;
		private bool disposed;

		public SafeFileWriter(string filePath)
		{
			this.Load(filePath);
		}

		public void Write(StringBuilder text)
		{
			if (disposed)
			{
				throw new ObjectDisposedException("Safe file handler was closed");
			}

			var natOverlap = new NativeOverlapped { OffsetLow = (int)0 };
			uint writenSize;
			var isSuccess = FileNativeMethods.WriteFile(
				ptr,
				text,
				(uint)Encoding.Unicode.GetByteCount(text.ToString()),
				out writenSize,
				ref natOverlap);

			if (!isSuccess)
			{
				Console.WriteLine("error");
			}
		}

		private void Load(string filePath)
		{
			if (disposed)
			{
				throw new ObjectDisposedException("Safe file handler was closed");
			}

			if (filePath == null || filePath.Length == 0)
			{
				throw new FileNotFoundException("specified file was not founf");
			}

			ptr = FileNativeMethods.CreateFile(filePath, GENERIC_READ|GENERIC_WRITE, 0, IntPtr.Zero, CREATE_NEW, 0, IntPtr.Zero);
			safeHandle = new SafeFileHandle(ptr, true);

			if (safeHandle.IsInvalid)
			{
				Marshal.ThrowExceptionForHR(Marshal.GetHRForLastWin32Error());
			}
		}
		
		public void Dispose()
		{
			this.Dispose(true);

			GC.SuppressFinalize(this);
		}

		private void Dispose(bool disposing)
		{
			if (this.disposed)
			{
				return;
			}

			if (disposing)
			{
				if(this.safeHandle != null && !this.safeHandle.IsInvalid)
				{
					this.safeHandle.Dispose();
				}
			}

			this.disposed = true;
		}
	}
}