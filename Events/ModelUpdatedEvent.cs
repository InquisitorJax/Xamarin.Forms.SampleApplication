using System;
using Prism.Events;
using Prism.Mvvm;

namespace SampleApplication
{
	public enum ModelUpdateEvent
	{
		Created,
		Updated,
		Deleted
	}

	public class ModelUpdatedMessageEvent<T> : PubSubEvent<ModelUpdatedMessageResult<T>> where T : ModelBase
	{
		public static void Publish(T updatedModel, ModelUpdateEvent updateEvent)
		{
			var messenger = CC.EventMessenger;
			var updateResult = new ModelUpdatedMessageResult<T>
			{
				UpdatedModel = updatedModel,
				UpdateEvent = updateEvent
			};
			messenger.GetEvent<ModelUpdatedMessageEvent<T>>().Publish(updateResult);
		}
	}

	public class ModelUpdatedMessageResult<T> where T : ModelBase
	{
		public T UpdatedModel { get; set; }

		public ModelUpdateEvent UpdateEvent { get; set; }
	}
}

