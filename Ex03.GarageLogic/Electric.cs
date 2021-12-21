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
        private readonly float m_MaxEnergy;

        public Electric(float i_CurrentEnergy, float MaxEnergy)
        {
            this.m_CurrentEnergy = i_CurrentEnergy;
            this.m_MaxEnergy = MaxEnergy;
        }

        public float CurrentEnergy { get; set; }
        public float MaxEnergy { get; set; }
        public override void AddEnergy(float i_FuelToAdd)
        {
            if (this.m_CurrentEnergy + i_FuelToAdd <= this.m_MaxEnergy)
            {
                this.m_CurrentEnergy += i_FuelToAdd;
            }
            else
            {
                //throw exception
            }
        }
    }
}
