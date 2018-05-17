using System;
using System.Collections.Generic;

namespace BusinessServices
{
	public abstract class Location
	{
		//a location such as a branch or distribution center may have vehicles
		public List<Vehicle> Vehicles;
        //when a derived class is instantiated, a new vehicle list is constructed
		protected Location(){
			this.Vehicles = new List<Vehicle>();
		}
    }
}
