using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    public class Truck : Vehicle
    {
        private bool? m_HasCapacity;
        private float? m_CargoVolume;
        public static readonly float sr_TruckMaxAirPressure = 25f;
        public static readonly int sr_TruckNumOfWheels = 16;
        public static readonly float sr_TruckMaxFuelCapacity = 130f;
        public static readonly Fuel.eFuelType sr_TruckFuelType = Fuel.eFuelType.Soler;

        public Truck(Engine i_Engine, List<Wheels> i_Wheels) : base(i_Engine, i_Wheels)
        {

        }

        public bool HasCapacity { get; set; }
        public float CargoVolume { get; set; }
    }
}
