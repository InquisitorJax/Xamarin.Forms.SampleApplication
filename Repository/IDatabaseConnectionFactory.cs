using System;
using SQLite;


namespace SampleApplication
{
	public interface IDatabaseConnectionFactory
	{
		//https://developer.xamarin.com/guides/xamarin-forms/working-with/databases/
		SQLiteAsyncConnection GetConnection();
	}
}

