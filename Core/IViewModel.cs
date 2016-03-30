using System;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace SampleApplication
{
	public interface IViewModel
	{
		Task InitializeAsync (Dictionary<string, string> args);
		Task LoadStateAsync();
		Task SaveStateAsync ();
		void Closing ();
	}
}

