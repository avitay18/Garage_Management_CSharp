using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    public class Wheels
    {
        private readonly string? m_ManufacturerName;
        private float? m_CurrentAirPressure;
        private readonly float m_MaxAirPressure;

        public Wheels(float i_MaxAirPressure)
        {
            this.m_CurrentAirPressure = null;
            this.m_ManufacturerName = null;
            this.m_MaxAirPressure = i_MaxAirPressure;
        }
        
        public string ManufacturerName { get; set; }

        public float CurrentAirPressure { get; set; }

        public float MaxAirPressure { get; set; }

        public void InflateTire(float i_Inflate)
        {
            if(CurrentAirPressure + i_Inflate <= MaxAirPressure)
            {
                CurrentAirPressure += i_Inflate;
            }
            else
            {
                //throw exception
            }
        }
    }
}
