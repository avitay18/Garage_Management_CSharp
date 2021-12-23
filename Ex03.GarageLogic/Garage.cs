using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    public class Garage
    {
        private List<Vehicle> m_VehiclesInGarage;
        private List<VehicleData> m_VehiclesDataInGarage;

        public Garage()
        {
            this.m_VehiclesInGarage = new List<Vehicle>();
            this.m_VehiclesDataInGarage = new List<VehicleData>();
        }

        public List<Vehicle> VehiclesInGarage
        {
            get
            {
                return this.m_VehiclesInGarage;
            }
        }
        public List<VehicleData> VehiclesDataInGarage
        {
            get
            {
                return this.m_VehiclesDataInGarage;
            }
        }
        public void AddVehicleToGarage(Vehicle newVehicle, VehicleData newCustomer)
        {
            this.m_VehiclesInGarage.Add(newVehicle);
            this.m_VehiclesDataInGarage.Add(newCustomer);
        }
        public string GetCustomerNameByLicense(string i_LicenseNumber)
        {
            int i = 0;
            foreach(Vehicle vehicle in this.m_VehiclesInGarage)
            {
                if(vehicle.LicenseNumber == i_LicenseNumber)
                {
                    break;
                }

                i++;
            }
            return this.m_VehiclesDataInGarage[i].CustomerName;
        }
        public bool CheckIfVehicleAlreadyInGarage(string i_LicenseNumber)
        {
            foreach(Vehicle vehicle in this.m_VehiclesInGarage)
            {
                int i = 0;
                if(vehicle.LicenseNumber == i_LicenseNumber)
                {
                    this.m_VehiclesDataInGarage[i].VehicleStatus = VehicleData.eVehicleStatus.InFix;
                    return true;
                }

                i++;
            }

            return false;
        }

        public Vehicle ReturnVehicleByLicenseNumber(string i_LicenseNumber)
        {
            foreach (Vehicle vehicle in m_VehiclesInGarage)
            {
                if (i_LicenseNumber == vehicle.LicenseNumber)
                {
                    return vehicle;
                }
            }

            return null;
        }
        public List<string> ShowLicenseNumberOfVehiclesInGarage(int userSelection)
        {
            List<string> licenseNumbersToShow = new List<string>();
            int i = 0;
            switch(userSelection)
            {
                case 1:
                    foreach (Vehicle vehicle in this.m_VehiclesInGarage)
                    {
                        licenseNumbersToShow.Add(vehicle.LicenseNumber);
                        i++;
                    }
                    break;
                case 2:
                    foreach (Vehicle vehicle in this.m_VehiclesInGarage)
                    {
                        if (this.m_VehiclesDataInGarage[i].VehicleStatus == VehicleData.eVehicleStatus.InFix)
                        {
                            licenseNumbersToShow.Add(vehicle.LicenseNumber);
                        }
                        i++;
                    }
                    break;
                case 3:
                    foreach (Vehicle vehicle in this.m_VehiclesInGarage)
                    {
                        if (this.m_VehiclesDataInGarage[i].VehicleStatus == VehicleData.eVehicleStatus.Fixed)
                        {
                            licenseNumbersToShow.Add(vehicle.LicenseNumber);
                        }
                        i++;
                    }
                    break;
                case 4:
                    foreach (Vehicle vehicle in this.m_VehiclesInGarage)
                    {
                        if (this.m_VehiclesDataInGarage[i].VehicleStatus == VehicleData.eVehicleStatus.Paid)
                        {
                            licenseNumbersToShow.Add(vehicle.LicenseNumber);
                        }
                        i++;
                    }
                    break;
            }

            return licenseNumbersToShow;

        }

    }
}
