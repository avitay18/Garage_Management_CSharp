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
        private float m_CurrentAirPressure;
        private readonly float m_MaxAirPressure;

        public Wheels(float i_MaxAirPressure)
        {
            this.m_CurrentAirPressure = 0;
            this.m_ManufacturerName = null;
            this.m_MaxAirPressure = i_MaxAirPressure;
        }
        
        public string ManufacturerName { get; set; }

        public float CurrentAirPressure
        {
            get
            {
                return this.m_CurrentAirPressure;
            }
            set
            {
                this.m_CurrentAirPressure = value;
            }
        }

        public float MaxAirPressure { get; set; }

        public void InflateTire(float i_Inflate)
        {
            if(CurrentAirPressure + i_Inflate <= MaxAirPressure)
            {
                CurrentAirPressure += i_Inflate;
            }
            else
            {
                throw new ValueOutOfRangeException(new Exception(), 0, MaxAirPressure - CurrentAirPressure);
            }
        }
        public void InflateTireToMax()
        {
            this.m_CurrentAirPressure = this.m_MaxAirPressure;
        }
    }
}
