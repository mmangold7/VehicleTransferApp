using System;
using System.Collections.Generic;

namespace BusinessServices
{
	public class DistributionCenter : Location
	{
		//a distribution center has branches
		public List<Branch> Branches;
    }
}
