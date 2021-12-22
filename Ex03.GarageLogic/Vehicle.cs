using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    public class Vehicle
    {
        protected string? m_ModelName;
        protected string? m_LicenseNumber;
        protected float m_EnergyPercentageLeft;
        protected List<Wheels> m_Wheels;
        protected Engine m_Engine;

        public Vehicle(Engine i_Engine,List<Wheels> i_Wheels)
        {
            this.m_Engine = i_Engine;
            this.m_Wheels = i_Wheels;
            this.m_ModelName = null;
            this.m_LicenseNumber = null;
            this.m_EnergyPercentageLeft = 0;
        }

        public string ModelName {
            get
            {
                return this.m_ModelName;
            }
            set
            {
                this.m_ModelName = value;
            }
        }
        public string LicenseNumber {
            get
            {
                return this.m_LicenseNumber;
            }
            set
            {
                this.m_LicenseNumber = value;
            }
        }

        public Engine Engine
        {
            get
            {
                return this.m_Engine;
            }
        }

        public List<Wheels> Wheels
        {
            get
            {
                return this.m_Wheels;
            }
        }

        public override string ToString()
        {
            string vehicleData = string.Format(
                "License number is: {0}," + " Model name is: {1}," + " Customer name is: {2},"
                + "Wheels manufacture name and max air pressure: {3} {4}," + " "); // status in garage 
            return base.ToString();
        }
    }
}
