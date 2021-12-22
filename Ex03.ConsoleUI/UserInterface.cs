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
                        break;
                    case 6:
                        break;
                    case 7:
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
                    break;
                case 3:
                case 4:
                    (newVehicle as Motorcycle).LicenseType = GetValidLicenseType();
                    (newVehicle as Motorcycle).EngineVolume = GetValidEngineVolume();
                    break;
                case 5:
                    (newVehicle as Truck).CargoVolume = GetValidCargoVolume();
                    (newVehicle as Truck).HasCapacity = GetIfTruckHasCapacity();
                    break;
            }

            m_ValidInput = false;
            i_Garage.AddVehicleToGarage(newVehicle, newCustomer);
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
                Console.WriteLine("{0} License number in garage: {1}",i, licenseNumbers[i]);
            }
        }
        private static void ChangeCarStatus(Garage i_Garage)
        {
            string licenseNumber = GetValidLicenseNumberFromUser();

            foreach (Vehicle vehicle in i_Garage.VehiclesInGarage)
            {
                int i = 0;
                if(licenseNumber == vehicle.LicenseNumber)
                {
                    i_Garage.VehiclesDataInGarage[i].VehicleStatus = GetValidVehicleStatus();
                    Console.WriteLine("Vehicle was found and his status is changed!");
                    return;
                }
                i++;
            }
            Console.WriteLine("Vehicle was not found...");
        }
        private static void InflateTireToMax(Garage i_Garage)
        {
            string licenseNumber = GetValidLicenseNumberFromUser();

            foreach (Vehicle vehicle in i_Garage.VehiclesInGarage)
            {
                if (licenseNumber == vehicle.LicenseNumber)
                {
                    for(int i = 0; i < vehicle.Wheels.Count; i++)
                    {
                        vehicle.Wheels[i].InflateTireToMax();
                    }
                    Console.WriteLine("Vehicle was found and his tires are inflated to max! ");
                    return;
                }
            }
            Console.WriteLine("Vehicle was not found...");
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
            menu.Append(" 1) inFIx" + Environment.NewLine);
            menu.Append(" 2) Fixed" + Environment.NewLine);
            menu.Append(" 3) Paid" + Environment.NewLine);
            Console.WriteLine(menu);
            while (true)
            {
                string vehicleStatus = Console.ReadLine();
                if (Regex.IsMatch(vehicleStatus, @"^[1-3]{1}$"))
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

