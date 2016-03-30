using System;
using SampleApplication;
using SQLite;
using System.IO;

namespace Application.Droid
{
	public class AndroidDatabaseFactory : IDatabaseConnectionFactory
	{
		#region IDatabaseConnectionFactory implementation
		//TODO: Wrap in LogicCommand
		public SQLiteAsyncConnection GetConnection ()
		{
			const string dbFileName = "SampleDatabse.db3";
			string documentsPath = System.Environment.GetFolderPath (System.Environment.SpecialFolder.Personal); // Documents folder
			string path = Path.Combine(documentsPath, dbFileName);

			SQLiteAsyncConnection connection = null;

			try
			{
				connection = new SQLiteAsyncConnection(path);

			}
			catch (SQLiteException ex)
			{
				//Log ex.message
			}

			return connection;
		}

		#endregion

	}
}

