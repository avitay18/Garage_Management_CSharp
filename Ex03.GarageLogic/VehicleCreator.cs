using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    public class VehicleCreator
    {
       public enum eVehicleType
       {
           ElectricCar=1,
           FuelCar,
           ElectricMotorcycle,
           FuelMotorcycle,
           Truck
       }

       public static Engine CreateNewEngine(eVehicleType i_VehicleType)
       {
           Engine newEngine = null;
           const float currentCapacity = 0;

           switch(i_VehicleType)
           {
                case eVehicleType.ElectricCar:
                    newEngine = new Electric(currentCapacity, Car.sr_CarMaxElecticTime);
                    break;
                case eVehicleType.FuelCar:
                    newEngine = new Fuel(Car.sr_CarMaxFuelCapacity, currentCapacity, Fuel.eFuelType.Octan95);
                    break;
                case eVehicleType.ElectricMotorcycle:
                    newEngine = new Electric(currentCapacity, Motorcycle.sr_MotorcycleMaxElecticTime);
                    break;
                case eVehicleType.FuelMotorcycle:
                    newEngine = new Fuel(Motorcycle.sr_MotorcycleMaxFuelCapacity, currentCapacity, Fuel.eFuelType.Octan98);
                    break;
                case eVehicleType.Truck:
                    newEngine = new Fuel(Truck.sr_TruckMaxFuelCapacity, currentCapacity, Fuel.eFuelType.Soler);
                    break;
           }

           return newEngine;
       }

       public static List<Wheels> CreateListOfWheels(eVehicleType i_VehicleType)
       {
           List<Wheels> wheelsList = new List<Wheels>();
           int numOfWheels = 0;
           float maxAirPressure = 0;

           switch (i_VehicleType)
           {
               case eVehicleType.ElectricCar:
               case eVehicleType.FuelCar:
                   numOfWheels = Car.sr_CarNumOfWheels;
                   maxAirPressure = Car.sr_CarMaxAirPressure;
                   break;
               case eVehicleType.ElectricMotorcycle:
               case eVehicleType.FuelMotorcycle:
                   numOfWheels = Motorcycle.sr_MotorcycleNumOfWheels;
                   maxAirPressure = Motorcycle.sr_MotorcycleMaxAirPressure;
                    break;
               case eVehicleType.Truck:
                   numOfWheels = Truck.sr_TruckNumOfWheels;
                   maxAirPressure = Truck.sr_TruckMaxAirPressure;
                   break;
           }

           for(int i = 0; i < numOfWheels; i++)
           {
               Wheels newWheel = new Wheels(maxAirPressure);
               wheelsList.Add(newWheel);
           }

           return wheelsList;
       }


       public static Vehicle CreateNewVehicle(eVehicleType i_VehicleType) 
       {
           Vehicle newVehicle = null;
           Engine newEngine = CreateNewEngine(i_VehicleType);
           List<Wheels> newWheels = CreateListOfWheels(i_VehicleType);

           switch (i_VehicleType) 
           {
               case eVehicleType.ElectricCar:
                   newVehicle = new Car(newEngine, newWheels);
                   break;
               case eVehicleType.FuelCar:
                   newVehicle = new Car(newEngine, newWheels);
                   break;
               case eVehicleType.ElectricMotorcycle:
                   newVehicle = new Motorcycle(newEngine, newWheels);
                   break;
               case eVehicleType.FuelMotorcycle:
                   newVehicle = new Motorcycle(newEngine, newWheels);
                   break;
               case eVehicleType.Truck:
                   newVehicle = new Truck(newEngine, newWheels);
                   break;
           }

            return newVehicle;
       }
    }
}
