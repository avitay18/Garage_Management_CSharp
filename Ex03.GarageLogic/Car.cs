using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    public class Car : Vehicle
    {

        private eColor m_Color;
        private eDoors m_Doors;
        public static readonly float sr_CarMaxAirPressure = 29f;
        public static readonly int sr_CarNumOfWheels = 4;
        public static readonly float sr_CarMaxFuelCapacity = 48f;
        public static readonly float sr_CarMaxElecticTime = 2.6f;
        public static readonly Fuel.eFuelType sr_CarFuelType = Fuel.eFuelType.Octan95;
        public enum eColor
        {
            Red=1,
            White,
            Black,
            Blue,
        }
        public enum eDoors
        {
            Two = 2,
            Three,
            Four,
            Five,
        }
        public Car(Engine i_Engine, List<Wheels> i_Wheels) : base(i_Engine, i_Wheels)
        {
            
        }

        public eColor Color {
            get
            {
                return this.m_Color;
            }
            set
            {
                this.m_Color = value;
            }
        }

        public eDoors Doors {
            get
            {
                return this.m_Doors;
            }
            set
            {
                this.m_Doors = value;
            }
        }
        



        
    }
}
