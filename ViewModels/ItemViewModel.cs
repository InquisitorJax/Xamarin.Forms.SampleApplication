using System;
using Prism.Events;
using System.Windows.Input;
using Prism.Commands;
using System.Threading.Tasks;
using Autofac;

namespace SampleApplication
{
	public class ItemViewModel : ViewModelBase
	{
		private readonly IRepository _repository;

		public ItemViewModel (IRepository repository)
		{
			_repository = repository;
			SaveItemCommand = DelegateCommand.FromAsyncHandler (SaveItemAsync);
		}

		public ICommand SaveItemCommand { get; private set;}

		private SampleItem _model;

		private bool _isNewModel;

		public SampleItem Model
		{
			get {return _model;}
			set { SetProperty (ref _model, value);}
		}

		public override async Task InitializeAsync (System.Collections.Generic.Dictionary<string, string> args)
		{
			if ( args != null && args.ContainsKey(Constants.Parameters.Id))
			{
				string id = args [Constants.Parameters.Id];
				var fetchResult = await _repository.FetchSampleItemAsync(id);
				if (fetchResult.IsValid())
				{
					Model = fetchResult.Model;	
				}
				else
				{
					await UserNotifier.ShowMessageAsync (fetchResult.Notification.ToString(), "Fetch Error");
				}
			}
			else
			{ //assume new model required
				Model = new SampleItem()
				{
					Id = Guid.NewGuid().ToString()
				};
				_isNewModel = true;
			}
		}			

		private async Task SaveItemAsync()
		{
			
			var eventMessenger = CC.IoC.Resolve<IEventAggregator>();
			ModelUpdateEvent updateEvent = _isNewModel ? ModelUpdateEvent.Created : ModelUpdateEvent.Updated;
			var saveResult= await _repository.SaveSampleItemAsync(Model, updateEvent);
			ModelUpdatedMessageResult<SampleItem> eventResult = new ModelUpdatedMessageResult<SampleItem>() { UpdatedModel = Model, UpdateEvent = updateEvent };
			if (saveResult.IsValid ()) 
			{
				eventMessenger.GetEvent<ModelUpdatedMessageEvent<SampleItem>> ().Publish (eventResult);
				await Close ();
			}
			else
			{
				UserNotifier.ShowMessageAsync (saveResult.ToString (), "Save Failed");
			}
		}

	}
}

