using System;
using System.Runtime.Serialization;
using System.Text.RegularExpressions;

namespace BusinessServices
{
	public class Vehicle
	{
		public Model Model { get; set; }
		public int Year { get; set; }
		public string Vin { get; set; }
		public VehicleStatusEnum Status { get; set; }
		public VehicleTypeEnum Type { get; set; }

		public Vehicle(Model model, int year, string vin){
			//use regex to check input parameters and throw exceptions if requirements not met
            //regex for min/max 4 numeric characters
			Regex yearRegex = new Regex(@"^\d{4}$");
            //regex for 24 character alphanumeric ending in 5 numeric
			Regex vinRegex = new Regex(@"^[a-zA-Z0-9]{19}\d{5}$");
			if(!yearRegex.IsMatch(year.ToString())){
				throw new InvalidVehicleYearException("Vehicle year must be set to 4 digit numeric");
			} else if(!vinRegex.IsMatch(vin))
			{
				throw new InvalidVehicleVinException("Vin must be 24 alphanumeric characters with a minimum of 8 alphas, ending with 5 numeric.");
			}
			Model = model;
			Year = year;
			Vin = vin;
		}
	}

	//vehicle type and vehicle status enums are included here for code exercise clarity
    public enum VehicleStatusEnum { StandBy, InTransit, InService, InRepair }
    public enum VehicleTypeEnum { Truck, Van, Semi }

	//put exceptions in this file for code exercise readability, they're only thrown above

	[Serializable]
	public class InvalidVehicleVinException : Exception
	{
		public InvalidVehicleVinException()
		{
		}

		public InvalidVehicleVinException(string message) : base(message)
		{
		}

		public InvalidVehicleVinException(string message, Exception innerException) : base(message, innerException)
		{
		}

		protected InvalidVehicleVinException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}

	[Serializable]
	public class InvalidVehicleYearException : Exception
	{
		public InvalidVehicleYearException()
		{
		}

		public InvalidVehicleYearException(string message) : base(message)
		{
		}

		public InvalidVehicleYearException(string message, Exception innerException) : base(message, innerException)
		{
		}

		protected InvalidVehicleYearException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
