using System;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace SampleApplication
{

	public interface INavigationService
	{
		Task GoBack();

		Task NavigateAsync(string destination, Dictionary<string, string> args = null, bool modal = false, bool forgetCurrentPage = false);

		Task ResumeAsync();

		Task SuspendAsync();

		object Current { get;}
	}
}

