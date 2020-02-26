using System;
using System.Data.SqlClient;

namespace Managed_resources
{
	class ClientProxy : IDisposable
	{
		private SqlConnection sqlConnection = new SqlConnection("connection string");
		private bool disposed;
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		private void Dispose(bool disposing)
		{
			if (!this.disposed)
			{
				if (disposing)
				{
					if (sqlConnection != null)
					{
						sqlConnection.Dispose();
					};
				}
			}

			this.disposed = true;
		}
	}
}
