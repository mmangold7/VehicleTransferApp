namespace BusinessServices
{
	public class Vehicle
	{
		public Model Model { get; set; }
		public int Year { get; set; }
		public string Vin { get; set; }
		public VehicleStatusEnum Status { get; set; }
		public VehicleTypeEnum Type { get; set; }
	}
	public enum VehicleStatusEnum {StandBy, InTransit, InService, InRepair}
	public enum VehicleTypeEnum {Truck, Van, Semi}
}
