using System;
using System.Threading.Tasks;
using Autofac;
using SQLite;

namespace SampleApplication
{

	public interface IRepository
	{
		Task Initialize();

		Task<FetchModelCollectionResult<SampleItem>> FetchSampleItemsAsync();	

		Task<FetchModelResult<SampleItem>> FetchSampleItemAsync(string id);

		Task<Notification> SaveSampleItemAsync (SampleItem item, ModelUpdateEvent updateEvent);
	}

	public class Repository : IRepository
	{
		#region IRepository implementation	

		public async Task<Notification>  SaveSampleItemAsync (SampleItem item, ModelUpdateEvent updateEvent)
		{
			Notification retNotification = Notification.Success ();
			try
			{
				if (updateEvent == ModelUpdateEvent.Created)
				{
					await _database.InsertAsync(item);
				}
				else
				{
					await _database.UpdateAsync(item);				
				}
			}
			catch (SQLiteException)
			{
				//LOG:
				retNotification.Add(new NotificationItem("Save Failed"));
			}

			return retNotification;
		}

		private SQLiteAsyncConnection _database;

		private bool _isInitialized = false;

		public async Task Initialize ()
		{

			try 
			{
				if (_isInitialized)
					return;
				_isInitialized = true;
				var connectionFactory = CC.IoC.Resolve<IDatabaseConnectionFactory>();
				_database = connectionFactory.GetConnection();
				await _database.CreateTableAsync<SampleItem>();			
			} 
			catch (SQLiteException) 
			{
				///report error	
			}
		}

		public async Task<FetchModelCollectionResult<SampleItem>> FetchSampleItemsAsync ()
		{
			FetchModelCollectionResult<SampleItem> retResult = new FetchModelCollectionResult<SampleItem>();
			var items = await _database.Table<SampleItem> ().ToListAsync();
			retResult.ModelCollection = items;
			return retResult;
		}

		public async Task<FetchModelResult<SampleItem>> FetchSampleItemAsync (string id)
		{
			FetchModelResult<SampleItem> retResult = new FetchModelResult<SampleItem> ();		

			var item = await _database.FindAsync<SampleItem>(id);
			retResult.Model = item;

			return retResult;
		}

		#endregion


	}
}

