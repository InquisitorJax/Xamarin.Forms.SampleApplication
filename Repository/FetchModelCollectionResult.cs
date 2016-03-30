using System;
using System.Collections.Generic;

namespace SampleApplication
{
	public class FetchModelCollectionResult<T> : CommandResult where T : ModelBase
	{
		public IList<T> ModelCollection { get; set; }
	}

}

