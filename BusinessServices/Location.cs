using System;
using System.Collections.Generic;

namespace BusinessServices
{
	public abstract class Location
	{
		//a location such as a branch or distribution center may have vehicles
		public List<Vehicle> Vehicles;
		protected Location(){
			this.Vehicles = new List<Vehicle>();
		}
    }
}
