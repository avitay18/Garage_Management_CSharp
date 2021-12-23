using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    public class Electric : Engine
    {
        private float m_CurrentEnergy;
        private float m_MaxEnergy;

        public Electric(float i_CurrentEnergy, float MaxEnergy)
        {
            this.m_CurrentEnergy = i_CurrentEnergy;
            this.m_MaxEnergy = MaxEnergy;
        }

        public float CurrentEnergy
        {
            get
            {
                return this.m_CurrentEnergy;
            }
            set
            {
                this.m_CurrentEnergy = value;
            }
        }
        public float MaxEnergy
        {
            get
            {
                return this.m_MaxEnergy;
            }
            set
            {
                this.m_MaxEnergy = value;
            }
        }
        public override void AddEnergy(float i_BatteryTimeToAdd)
        {
            if (this.m_CurrentEnergy + i_BatteryTimeToAdd <= this.m_MaxEnergy)
            {
                this.m_CurrentEnergy += i_BatteryTimeToAdd;
                
            }
            else
            {
                throw new ValueOutOfRangeException(new Exception(), 0, MaxEnergy - CurrentEnergy);
            }
        }
    }
}
