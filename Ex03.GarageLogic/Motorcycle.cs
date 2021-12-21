using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    public class Motorcycle : Vehicle
    {
        private eLicenseType m_licenseType;
        private int m_EngineVolume;
        public static readonly float sr_MotorcycleMaxAirPressure = 30f;
        public static readonly int sr_MotorcycleNumOfWheels = 2;
        public static readonly float sr_MotorcycleMaxFuelCapacity = 5.8f;
        public static readonly float sr_MotorcycleMaxElecticTime = 2.3f;
        public static readonly Fuel.eFuelType sr_MotorcycleFuelType = Fuel.eFuelType.Octan98;
        public enum eLicenseType
        {
            A=1,
            A2,
            AA,
            B,
        }

        public Motorcycle(Engine i_Engine,List<Wheels> i_Wheels) : base(i_Engine, i_Wheels)
        {

        }
        public int EngineVolume {
            get
            {
                return this.m_EngineVolume;
            }
            set
            {
                this.m_EngineVolume = value;
            }
        }
        public eLicenseType LicenseType {
            get
            {
                return this.m_licenseType;
            }
            set
            {
                this.m_licenseType = value;
            }
        }

    }
}
