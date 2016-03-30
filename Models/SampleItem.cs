using System;
using Prism.Mvvm;

namespace SampleApplication
{
	public class SampleItem : ModelBase
	{

		private string _name;

		public string Name {
			get { return _name;}
			set { SetProperty (ref _name, value);}
		}

		private string _password;

		public string Password {
			get { return _password;}
			set { SetProperty (ref _password, value);}
		}

		private string _description;

		public string Description {
			get { return _description; }
			set { SetProperty (ref _description, value);}
		}

		private byte[] _picture;

		public byte[] Picture {
			get { return _picture; }
			set { SetProperty (ref _picture, value);}
		}
	}
}

