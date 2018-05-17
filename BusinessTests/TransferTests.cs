using Microsoft.VisualStudio.TestTools.UnitTesting;
using BusinessServices;
using System.Collections.Generic;
using System;
using System.Linq;

namespace BusinessTests
{
    [TestClass]
    public class TransferTests
    {
		[TestMethod]
        public void CanTransferStandByVehicle()
        {
            TransferService transferService = new TransferService();
            var fromBranch = new Branch();
			var toBranch = new Branch();
            
            var vehicle = new Vehicle()
            {
				Status = VehicleStatusEnum.StandBy
            };
            
			transferService.Transfer(vehicle, fromBranch, toBranch);
            //check that vehicle was transferred to the new branch
			Assert.AreEqual(toBranch.Vehicles.Count(), 1);
        }


        [TestMethod]
		[ExpectedException(typeof(SemiTransferToBranchException), "A semi can be transferred between distribution centers, but not to branches")]
        public void CannotTransferSemiToBranch()
        {
			TransferService transferService = new TransferService();
			var distributionCenter = new DistributionCenter();
			var branch = new Branch();
			//make a semi
			var vehicle = new Vehicle()
			{
				Type = VehicleTypeEnum.Semi
			};
            //add semi to dist. center
			distributionCenter.Vehicles.Add(vehicle);
            //attempt to transfer semi to branch, should fail with custom exception
			transferService.Transfer(vehicle, distributionCenter, branch);
        }

		[TestMethod]
        public void CannotTransferVehiclesWithoutStandByStatus()
        {
			//instantiate transfer service and branches
            TransferService transferService = new TransferService();
            var fromBranch = new Branch();
            var toBranch = new Branch();

			//instantiate vehicles without stand-by status
			var vehicles = new List<Vehicle>()
			{
				new Vehicle()
				{
					Status = VehicleStatusEnum.InRepair
				},
				new Vehicle()
				{
					Status = VehicleStatusEnum.InService
				},
				new Vehicle()
				{
					Status = VehicleStatusEnum.InTransit
				}
			};

			//add vehicles to start branch
			fromBranch.Vehicles.AddRange(vehicles);
			foreach(var v in vehicles)
			{
				try
                {
					transferService.Transfer(v, fromBranch, toBranch);
                    Assert.Fail("An exception should have been thrown"); //if this runs an exception was not thrown as it should have been
                }
                catch (Exception e)
                {
                    //check that this operation threw the expected exception
					Assert.IsInstanceOfType(e, typeof(VehicleNotInStandByException));
                }
			}
        }
    }
}
