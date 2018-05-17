using System;
using System.Runtime.Serialization;

namespace BusinessServices
{
    public class TransferService
	{
		//transfer vehicles from one location to another per business rules
		public void Transfer(Vehicle vehicle, Location fromLocation, Location toLocation)
		{
			//only vehicles in stand-by may be transferred
			if(vehicle.Status != VehicleStatusEnum.StandBy){
				throw new VehicleNotInStandByException("Only vehicles with a status of stand-by may be transferred.");
			}
			if(vehicle.Type == VehicleTypeEnum.Semi && toLocation is Branch){
				throw new SemiTransferToBranchException("A semi can be transferred between distribution centers, but not to branches");
			}
			toLocation.Vehicles.Add(vehicle);
			fromLocation.Vehicles.Remove(vehicle);
		}
    }

    //exceptions are included here in this file for the code exercise as they are only used with this service

	[Serializable]
	public class SemiTransferToBranchException : Exception
	{
		public SemiTransferToBranchException()
		{
		}

		public SemiTransferToBranchException(string message) : base(message)
		{
		}

		public SemiTransferToBranchException(string message, Exception innerException) : base(message, innerException)
		{
		}

		protected SemiTransferToBranchException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}

	[Serializable]
	public class VehicleNotInStandByException : Exception
	{
		public VehicleNotInStandByException()
		{
		}

		public VehicleNotInStandByException(string message) : base(message)
		{
		}

		public VehicleNotInStandByException(string message, Exception innerException) : base(message, innerException)
		{
		}

		protected VehicleNotInStandByException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
