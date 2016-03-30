using System;
using Prism.Mvvm;
using SQLite;

namespace SampleApplication
{
	public abstract class ModelBase : BindableBase
	{
		private string _id;

		[PrimaryKey, Unique]
		public string Id 
		{
			get { return _id; }
			set { SetProperty (ref _id, value);}
		}

	}
}

