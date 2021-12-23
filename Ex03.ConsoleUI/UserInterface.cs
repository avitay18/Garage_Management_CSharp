using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using Ex03.GarageLogic;

namespace Ex03.ConsoleUI
{
    public class UserInterface
    {
        private static bool m_ValidInput = false;
        public static void Run(Garage i_Garage)
        {
            while(true)
            {
                int userSelection = 0;
                Console.Clear();
                DisplayMenu();
                CheckExceptions(ref userSelection, 1, 8);
                switch(userSelection)
                {
                    case 1:
                        AddNewVehicleToGarage(i_Garage);
                        break;
                    case 2:
                        ShowVehiclesLicenseNumbers(i_Garage);
                        break;
                    case 3:
                        ChangeCarStatus(i_Garage);
                        break;
                    case 4:
                        InflateTireToMax(i_Garage);
                        break;
                    case 5:
                        AddFuel(i_Garage);
                        break;
                    case 6:
                        AddEnergy(i_Garage);
                        break;
                    case 7:
                        DisplayVehicleData(i_Garage);
                        break;
                    case 8:
                        Environment.Exit(0);
                        break;
                }

                m_ValidInput = false;
            }
        }
        public static void AddNewVehicleToGarage(Garage i_Garage)
        {
            m_ValidInput = false;
            int userSelection = 0;
            Console.Clear();
            StringBuilder vehicleToAdd = new StringBuilder(Environment.NewLine);
            vehicleToAdd.Append(" Please enter which vehicle you would like to enter the garage" + Environment.NewLine);
            vehicleToAdd.Append(" 1) ElectricCar" + Environment.NewLine);
            vehicleToAdd.Append(" 2) FuelCar" + Environment.NewLine);
            vehicleToAdd.Append(" 3) ElectricMotorcycle" + Environment.NewLine);
            vehicleToAdd.Append(" 4) FuelMotorcycle" + Environment.NewLine);
            vehicleToAdd.Append(" 5) Truck" + Environment.NewLine);
            Console.WriteLine(vehicleToAdd);
            CheckExceptions(ref userSelection, 1, 5);
            m_ValidInput = false;
            string licenseNumber = GetValidLicenseNumberFromUser();
            if(i_Garage.CheckIfVehicleAlreadyInGarage(licenseNumber))
            {
                Console.WriteLine("Vehicle already in garage");
                return;
            }
            VehicleData newCustomer = new VehicleData(GetValidFullName(), GetValidPhoneNumber(),VehicleData.eVehicleStatus.InFix);
            Vehicle newVehicle = VehicleCreator.CreateNewVehicle((VehicleCreator.eVehicleType)userSelection); // Create vehicle
            newVehicle.LicenseNumber = licenseNumber;
            i_Garage.CheckIfVehicleAlreadyInGarage(newVehicle.LicenseNumber);
            Console.WriteLine("Please enter model name: ");
            newVehicle.ModelName = Console.ReadLine();
            Console.WriteLine("Please enter wheels manufacture name: ");
            string manufactureName = Console.ReadLine();

            foreach(Wheels wheel in newVehicle.Wheels)
            {
                wheel.ManufacturerName = manufactureName;
            }
            switch(userSelection)
            {
                case 1:
                case 2:
                    (newVehicle as Car).Color = GetValidCarColor();
                    (newVehicle as Car).Doors = GetValidDoorsAmount();

                    foreach (Wheels wheel in newVehicle.Wheels)
                    {
                        wheel.MaxAirPressure = Car.sr_CarMaxAirPressure;
                    }

                    if (newVehicle.Engine is Fuel)
                    {
                        (newVehicle.Engine as Fuel).MaxFuel = Car.sr_CarMaxFuelCapacity;
                    }
                    else if (newVehicle.Engine is Electric)
                    {
                        (newVehicle.Engine as Electric).MaxEnergy = Car.sr_CarMaxElecticTime;
                    }
                    break;
                case 3:
                case 4:
                    (newVehicle as Motorcycle).LicenseType = GetValidLicenseType();
                    (newVehicle as Motorcycle).EngineVolume = GetValidEngineVolume();

                    foreach (Wheels wheel in newVehicle.Wheels)
                    {
                        wheel.MaxAirPressure = Motorcycle.sr_MotorcycleMaxAirPressure;
                    }

                    if (newVehicle.Engine is Fuel)
                    {
                        (newVehicle.Engine as Fuel).MaxFuel = Motorcycle.sr_MotorcycleMaxFuelCapacity;
                    }
                    else if (newVehicle.Engine is Electric)
                    {
                        (newVehicle.Engine as Electric).MaxEnergy = Motorcycle.sr_MotorcycleMaxElecticTime;
                    }
                    break;
                case 5:
                    (newVehicle as Truck).CargoVolume = GetValidCargoVolume();
                    (newVehicle as Truck).HasCapacity = GetIfTruckHasCapacity();
                    (newVehicle.Engine as Fuel).MaxFuel = Truck.sr_TruckMaxFuelCapacity;

                    foreach (Wheels wheel in newVehicle.Wheels)
                    {
                        wheel.MaxAirPressure = Truck.sr_TruckMaxAirPressure;
                    }
                    break;
            }

            m_ValidInput = false;
            i_Garage.AddVehicleToGarage(newVehicle, newCustomer);
            PressAnyKeyToContinue();
        } 

        public static void DisplayMenu()
        {
            StringBuilder menu = new StringBuilder(Environment.NewLine);
            menu.Append(" Welcome to Avitay and Asher Garage Manager" + Environment.NewLine);
            menu.Append(" Please choose an action and press enter: " + Environment.NewLine);
            menu.Append(" ----------------------------------" + Environment.NewLine);
            menu.Append("  1) Add a new vehicle to the garage" + Environment.NewLine);
            menu.Append("  2) Show license numbers by their status" + Environment.NewLine);
            menu.Append("  3) Change vehicle status" + Environment.NewLine);
            menu.Append("  4) Inflate vehicle wheels Pressure to maximum" + Environment.NewLine);
            menu.Append("  5) Fuel vehicle" + Environment.NewLine);
            menu.Append("  6) Charge vehicle" + Environment.NewLine);
            menu.Append("  7) Show vehicle full data" + Environment.NewLine);
            menu.Append("  8) Exit" + Environment.NewLine);
            menu.Append(" ----------------------------------" + Environment.NewLine + Environment.NewLine);
            Console.WriteLine(menu);
        }

        private static void DisplayVehicleData(Garage i_Garage)
        {
            string licenseNumber = GetValidLicenseNumberFromUser();
            if(!i_Garage.CheckIfVehicleAlreadyInGarage(licenseNumber))
            {
                Console.WriteLine("Vehicle not exist in the garage");
                PressAnyKeyToContinue();
                return;
            }

            Vehicle vehicle = i_Garage.ReturnVehicleByLicenseNumber(licenseNumber);

            string vehicleData = string.Format(
                "License number is: {0}, " + " Model name is: {1}, " + " Customer name is: {2}, " + Environment.NewLine
                + "Wheels manufacture name: {3}, current air pressure: {4}, max air pressure: {5}, "
                + Environment.NewLine + "energy percentage left: {6}%, ",
                vehicle.LicenseNumber,
                vehicle.ModelName,
                i_Garage.GetCustomerNameByLicense(licenseNumber),
                vehicle.Wheels[0].ManufacturerName,
                vehicle.Wheels[0].CurrentAirPressure,
                vehicle.Wheels[0].MaxAirPressure,
                (float)Math.Round(vehicle.EnergyPercentageLeft, 2));
            if(vehicle is Car)
            {
                string carData = string.Format(
                    "Car number of doors is: {0}, " + "Color is: {1}",
                    (vehicle as Car).Doors,
                    (vehicle as Car).Color);
                if(vehicle.Engine is Fuel)
                {
                    string fuelData = string.Format("Fuel type is: {0}, ", (vehicle.Engine as Fuel).FuelType);
                    Console.WriteLine(vehicleData + fuelData + carData);
                }

                if(vehicle.Engine is Electric)
                {
                    string electricData = string.Format("this car uses battery, ");
                    Console.WriteLine(vehicleData + electricData + carData);
                }
            }

            if(vehicle is Motorcycle)
            {
                string motorcycleData = string.Format(
                    "Lisence type is: {0}, " + "Engine volume is: {1}",
                    (vehicle as Motorcycle).LicenseType,
                    (vehicle as Motorcycle).EngineVolume);
                if(vehicle.Engine is Fuel)
                {
                    string fuelData = string.Format("Fuel type is: {0}, ", (vehicle.Engine as Fuel).FuelType);
                    Console.WriteLine(vehicleData + fuelData + motorcycleData);
                }

                if(vehicle.Engine is Electric)
                {
                    string electricData = string.Format("this motorcycle uses battery, ");
                    Console.WriteLine(vehicleData + electricData + motorcycleData);
                }
            }

            if(vehicle is Truck)
            {
                string truckData = string.Format(
                    "Cargo volume is: {0}, " + "truck has capacity: {1}",
                    (vehicle as Truck).CargoVolume,
                    (vehicle as Truck).HasCapacity);
                    string fuelData = string.Format("Fuel type is: {0}, ", (vehicle.Engine as Fuel).FuelType);
                    Console.WriteLine(vehicleData + fuelData + truckData);
            }
            PressAnyKeyToContinue();
        }

        private static void ShowVehiclesLicenseNumbers(Garage i_Garage)
        {
            m_ValidInput = false;
            int userSelection = 0;
            List<string> licenseNumbers;
            Console.Clear();
            StringBuilder text = new StringBuilder(Environment.NewLine);
            text.Append(" Please choose how you would like to see license numbers" + Environment.NewLine);
            text.Append(" 1) show all numbers in the garage " + Environment.NewLine);
            text.Append(" 2) show only cars inFix" + Environment.NewLine);
            text.Append(" 3) show only Fixed Cars" + Environment.NewLine);
            text.Append(" 4) show only Paid Cars" + Environment.NewLine);
            Console.WriteLine(text);
            CheckExceptions(ref userSelection, 1, 4);
            Console.WriteLine(Environment.NewLine);
            licenseNumbers = i_Garage.ShowLicenseNumberOfVehiclesInGarage(userSelection);
            for(int i = 0; i < licenseNumbers.Count; i++)
            {
                Console.WriteLine("{0}. vehicle plate number in garage: {1}",i+1, licenseNumbers[i]);
            }
            PressAnyKeyToContinue();
        }
        private static void ChangeCarStatus(Garage i_Garage)
        {
            string licenseNumber = GetValidLicenseNumberFromUser();

            foreach (Vehicle vehicle in i_Garage.VehiclesInGarage)
            {
                int i = 0;
                if (licenseNumber == vehicle.LicenseNumber)
                {
                    i_Garage.VehiclesDataInGarage[i].VehicleStatus = GetValidVehicleStatus();
                    Console.WriteLine("Vehicle was found and his status is changed!" + Environment.NewLine);
                    return;
                }
                i++;
            }
            Console.WriteLine("Vehicle was not found..." + Environment.NewLine);
            PressAnyKeyToContinue();
        }
        private static void InflateTireToMax(Garage i_Garage)
        {
            string licenseNumber = GetValidLicenseNumberFromUser();
            if(!i_Garage.CheckIfVehicleAlreadyInGarage(licenseNumber))
            {
                Console.WriteLine("vehicle was not found...");
                return;
            }

            foreach (Vehicle vehicle in i_Garage.VehiclesInGarage)
            {
                if (licenseNumber == vehicle.LicenseNumber)
                {
                    for (int i = 0; i < vehicle.Wheels.Count; i++)
                    {
                        vehicle.Wheels[i].InflateTireToMax();
                    }
                    Console.WriteLine("vehicle was found and his tires are inflated to max! ");
                }
            }
            PressAnyKeyToContinue();
        }

        private static void AddFuel(Garage i_Garage)
        {
            string licenseNumber = GetValidLicenseNumberFromUser();
            if (!i_Garage.CheckIfVehicleAlreadyInGarage(licenseNumber))
            {
                Console.WriteLine("Vehicle not found in the garage...");
                PressAnyKeyToContinue();
                return;
            }
            float fuelToAdd = -1;
            while (fuelToAdd <= 0)
            {
                m_ValidInput = false;
                try
                {
                    Console.WriteLine("Please enter amount of fuel you would like to add: ");
                    GetGoodFloatUserSelection(ref fuelToAdd);
                }
                catch (FormatException)
                {
                    Console.WriteLine("please enter a positive number bigger than 0" + Environment.NewLine);
                }
            }

            foreach (Vehicle vehicle in i_Garage.VehiclesInGarage)
            {

                if (vehicle.LicenseNumber == licenseNumber)
                {
                    try
                    {
                        if (vehicle.Engine is Electric)
                        {
                            throw new ArgumentException();
                        }

                        vehicle.Engine.AddEnergy(fuelToAdd);
                        vehicle.EnergyPercentageLeft = (vehicle.Engine as Fuel).CurrentFuel
                                                       / (vehicle.Engine as Fuel).MaxFuel * 100;
                    }
                    catch (ValueOutOfRangeException e)
                    {
                        Console.Write(
                            "Error: The number should be between " + e.MinValue + " to " + e.MaxValue
                            + Environment.NewLine);
                    }
                    catch (ArgumentException)
                    {
                        Console.WriteLine("This is Electric type car, you cannot refuel it ");
                        PressAnyKeyToContinue();
                        return;
                    }
                }
            }
            PressAnyKeyToContinue();
        }
        private static void AddEnergy(Garage i_Garage)
        {
            string licenseNumber = GetValidLicenseNumberFromUser();
            if(!i_Garage.CheckIfVehicleAlreadyInGarage(licenseNumber))
            {
                Console.WriteLine("Vehicle not found in the garage...");
                PressAnyKeyToContinue();
                return;
            }
            float energyToAdd = -1;
            while(energyToAdd <= 0)
            {
                m_ValidInput = false;
                try
                {
                    Console.WriteLine("Please enter amount of energy you would like to add: ");
                    GetGoodFloatUserSelection(ref energyToAdd);
                }
                catch (FormatException)
                {
                    Console.WriteLine("please enter a positive number bigger than 0" + Environment.NewLine);
                }
            }

            foreach(Vehicle vehicle in i_Garage.VehiclesInGarage)
            {

                if(vehicle.LicenseNumber == licenseNumber)
                {
                    try
                    {
                        if(vehicle.Engine is Fuel)
                        {
                            throw new ArgumentException();
                        }

                        vehicle.Engine.AddEnergy(energyToAdd);
                        vehicle.EnergyPercentageLeft = (vehicle.Engine as Electric).CurrentEnergy
                                                       / (vehicle.Engine as Electric).MaxEnergy * 100;
                    }
                    catch(ValueOutOfRangeException e)
                    {
                        Console.Write(
                            "Error: The number should be between " + e.MinValue + " to " + e.MaxValue
                            + Environment.NewLine);
                    }
                    catch(ArgumentException)
                    {
                        Console.WriteLine("This is Fuel type car, you cannot charge it ");
                        PressAnyKeyToContinue();
                        return;
                    }
                }
            }
            PressAnyKeyToContinue();
        }
            
        
        private static string GetValidLicenseNumberFromUser()
        {
            while(true)
            {
                Console.WriteLine("Please enter Plate License Number: ");
                string licensePlateNumber = Console.ReadLine();
                if (Regex.IsMatch(licensePlateNumber, @"^[1-9]{7}$"))
                {
                    return licensePlateNumber;
                }
                Console.WriteLine("Wrong input please use numbers only");
            }
        }
        private static VehicleData.eVehicleStatus GetValidVehicleStatus()
        {
            StringBuilder menu = new StringBuilder(Environment.NewLine);
            menu.Append(" Please Choose vehicle status" + Environment.NewLine);
            menu.Append(" 0) inFIx" + Environment.NewLine);
            menu.Append(" 1) Fixed" + Environment.NewLine);
            menu.Append(" 2) Paid" + Environment.NewLine);
            Console.WriteLine(menu);
            while (true)
            {
                string vehicleStatus = Console.ReadLine();
                if (Regex.IsMatch(vehicleStatus, @"^[0-2]{1}$"))
                {
                    return (VehicleData.eVehicleStatus)Enum.Parse(typeof(VehicleData.eVehicleStatus), vehicleStatus);
                }
                Console.WriteLine("Wrong input please enter numbers between 1 to 3");
            }
        } 
        private static bool GetIfTruckHasCapacity()
        {
            while(true)
            {
                Console.WriteLine("Please Enter 1 if your truck has capacity and 0 if not: ");
                string hasCapacity = Console.ReadLine();
                if(Regex.IsMatch(hasCapacity, @"^[0-1]{1}$"))
                {
                    return hasCapacity == "1";
                }
            }
        }

        private static string GetValidPhoneNumber()
        {
            Console.WriteLine("Please Enter your phone number: ");
            while (true)
            {
                string phoneNumber = Console.ReadLine();
                if (Regex.IsMatch(phoneNumber, @"^[0-9]{10}$"))
                {
                    return phoneNumber;
                }
                Console.WriteLine("Please enter digits only and length of 10");
            }
        }

        private static void PressAnyKeyToContinue()
        {
            Console.WriteLine("Press any key to go back to menu" + Environment.NewLine);
            Console.ReadKey();
        }
        private static string GetValidFullName()
        {
            Console.WriteLine("Please Enter your Full Name: ");
            while (true)
            {
                string fullName = Console.ReadLine();
                if (Regex.IsMatch(fullName, @"^[A-Za-z ]+$"))
                {
                    return fullName;
                }
                Console.WriteLine("Please Use letters only...");
            }
        }
        private static float GetValidCargoVolume()
        {
            Console.WriteLine("Please enter cargo volume for your truck: ");
            int cargoVolume = 0;
            CheckExceptions(ref cargoVolume, 0, 10000);
            return (float)cargoVolume;
        }
        private static int GetValidEngineVolume()
        {
            Console.WriteLine("Please enter engine volume for your motorcycle: ");
            int engineVolume = 0;
            CheckExceptions(ref engineVolume, 0, 4000);
            return engineVolume;
        }
        private static Car.eColor GetValidCarColor()
        {
            StringBuilder carColors = new StringBuilder(Environment.NewLine);
            carColors.Append(" Please Choose Car Color" + Environment.NewLine);
            carColors.Append(" 1) Red" + Environment.NewLine);
            carColors.Append(" 2) White" + Environment.NewLine);
            carColors.Append(" 3) Black" + Environment.NewLine);
            carColors.Append(" 4) Blue" + Environment.NewLine);
            Console.WriteLine(carColors);

            while (true)
            {
                string carColor = Console.ReadLine();
                if (Regex.IsMatch(carColor, @"^[1-4]{1}$"))
                {
                    return (Car.eColor)Enum.Parse(typeof(Car.eColor), carColor);
                }
                Console.WriteLine("Wrong input please enter numbers between 1 to 4");
            }
        }

        private static Motorcycle.eLicenseType GetValidLicenseType()
        {
            StringBuilder licenseTypes = new StringBuilder(Environment.NewLine);
            licenseTypes.Append(" Please Choose your License type" + Environment.NewLine);
            licenseTypes.Append(" 1) A" + Environment.NewLine);
            licenseTypes.Append(" 2) A2" + Environment.NewLine);
            licenseTypes.Append(" 3) AA" + Environment.NewLine);
            licenseTypes.Append(" 4) B" + Environment.NewLine);
            Console.WriteLine(licenseTypes);

            while (true)
            {
                string licenseType = Console.ReadLine();
                if (Regex.IsMatch(licenseType, @"^[1-4]{1}$"))
                {
                    return (Motorcycle.eLicenseType)Enum.Parse(typeof(Motorcycle.eLicenseType), licenseType);
                }
                Console.WriteLine("Wrong input please enter numbers between 1 to 4");
            }
        }
        private static Car.eDoors GetValidDoorsAmount()
        {
            StringBuilder doors = new StringBuilder(Environment.NewLine);
            doors.Append(" Please Choose Amount of doors" + Environment.NewLine);
            doors.Append(" 2) Two" + Environment.NewLine);
            doors.Append(" 3) Three" + Environment.NewLine);
            doors.Append(" 4) Four" + Environment.NewLine);
            doors.Append(" 5) Five" + Environment.NewLine);
            Console.WriteLine(doors);

            while (true)
            {
                string doorAmount = Console.ReadLine();
                if (Regex.IsMatch(doorAmount, @"^[2-5]{1}$"))
                {
                    return (Car.eDoors)Enum.Parse(typeof(Car.eDoors), doorAmount);
                }
                Console.WriteLine("Wrong input please enter numbers between 2 to 5");
            }
        }

        private static bool GetGoodIntUserSelection(ref int i_UserSelection, int i_MinValue, int i_MaxValue)
        {
            if (!int.TryParse(Console.ReadLine(),out i_UserSelection))
            {
                throw new FormatException();
            }

            if (i_UserSelection < i_MinValue || i_UserSelection > i_MaxValue)
            {
                throw new ValueOutOfRangeException(new Exception(), i_MinValue, i_MaxValue);
            }

            return true;
        }

        private static bool GetGoodFloatUserSelection(ref float i_UserSelection)
        {
            if (!float.TryParse(Console.ReadLine(), out i_UserSelection))
            {
                throw new FormatException();
            }
            
            return true;
        }
        private static void CheckExceptions(ref int i_UserSelection, int i_MinValue, int i_MaxValue)
        {
            while (!m_ValidInput)
            {
                try
                {
                    m_ValidInput = GetGoodIntUserSelection(ref i_UserSelection, i_MinValue, i_MaxValue);
                }
                catch (FormatException)
                {
                    Console.WriteLine("enter number please");
                }
                catch (ValueOutOfRangeException e)
                {
                    Console.WriteLine("Error: The number should be between " + e.MinValue + " to " + e.MaxValue);
                }
            }
        }

    }
}