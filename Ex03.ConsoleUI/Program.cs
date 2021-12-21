using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Runtime.Remoting.Services;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using Ex03.GarageLogic;

namespace Ex03.ConsoleUI
{
    public class Program
    {
        public static void Main()
        {
            ////Vehicle newElectricCar = VehicleCreator.CreateNewVehicle(VehicleCreator.eVehicleType.ElectricCar);
            ////Vehicle newFuelCar = VehicleCreator.CreateNewVehicle(VehicleCreator.eVehicleType.FuelCar);
            ////Vehicle newElectricMotorcycle = VehicleCreator.CreateNewVehicle(VehicleCreator.eVehicleType.ElectricMotorcycle);
            ////Vehicle newFuelMotorcycle = VehicleCreator.CreateNewVehicle(VehicleCreator.eVehicleType.FuelMotorcycle);

            ////Vehicle newTruck = VehicleCreator.CreateNewVehicle(VehicleCreator.eVehicleType.Truck);

            UserInterface.Run(new Garage());
            //UserInterface.AddNewVehicleToGarage();







        }
    }
}
