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
        public void VehicleYearMustBeFourDigits()
        {
			//instantiate list of invalid years
            var years = new List<int>()
            {
                75,
                300,
                20022,
                20170
            };

			foreach (var y in years)
            {
                try
                {
					var model = new Model();
					var vehicle = new Vehicle(model, y, "abcdefghijklmnopqrs12345");
                    Assert.Fail("An exception should have been thrown"); //if this runs an exception was not thrown as it should have been
                }
                catch (Exception e)
                {
                    //check that this operation threw the expected exception
					Assert.IsInstanceOfType(e, typeof(InvalidVehicleYearException));
                }
            }
        }

		[TestMethod]
        public void VehicleVinMustMatchRegex()
        {
            //instantiate list of invalid vins
            var vins = new List<string>()
            {
				//only ends with 4 numeric
				"abcdefghijklmnopqrsa2345",
                //only 7 alpha - not implemented in regex
				//"abcdefg00000000000012345",
                //25 characters long
				"abcdefghijklmnopqrs123455",
                //23 characters long
				"abcdefghijklmnopqr12345"
            };

            foreach (var v in vins)
            {
                try
                {
					var model = new Model();
                    var vehicle = new Vehicle(model, 2000, v);
                    Assert.Fail("An exception should have been thrown"); //if this runs an exception was not thrown as it should have been
                }
                catch (Exception e)
                {
                    //check that this operation threw the expected exception
					Assert.IsInstanceOfType(e, typeof(InvalidVehicleVinException));
                }
            }
        }

		[TestMethod]
        public void CanTransferStandByVehicle()
        {
            TransferService transferService = new TransferService();
            var fromBranch = new Branch();
			var toBranch = new Branch();
            
			var model = new Model();
            var vehicle = new Vehicle(model, 2000, "abcdefghijklmnopqrs12345");
			vehicle.Status = VehicleStatusEnum.StandBy;
            
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
			var model = new Model();
            var vehicle = new Vehicle(model, 2000, "abcdefghijklmnopqrs12345");
			vehicle.Type = VehicleTypeEnum.Semi;
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
			var vehicles = new List<Vehicle>();
			var model = new Model();

            var vehicle1 = new Vehicle(model, 2000, "abcdefghijklmnopqrs12345");
			vehicle1.Status = VehicleStatusEnum.InRepair;
			vehicles.Add(vehicle1);

			var vehicle2 = new Vehicle(model, 2000, "abcdefghijklmnopqrs12345");
			vehicle2.Status = VehicleStatusEnum.InService;
			vehicles.Add(vehicle2);

			var vehicle3 = new Vehicle(model, 2000, "abcdefghijklmnopqrs12345");
			vehicle3.Status = VehicleStatusEnum.InTransit;
			vehicles.Add(vehicle3);
            
			//add vehicles to start branch
			fromBranch.Vehicles.AddRange(vehicles);
			foreach(var v in fromBranch.Vehicles)
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
