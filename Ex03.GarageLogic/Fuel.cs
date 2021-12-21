using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    public class Fuel : Engine
    {
        private float? m_CurrentFuel;
        private readonly float? m_MaxFuel;
        private eFuelType m_fuelType;

        public enum eFuelType
        {
            Octan95,
            Octan96,
            Octan98,
            Soler
        }

        public Fuel(float maxFuel, float i_CurrentFuel, eFuelType i_FuelType)
        {
            this.m_MaxFuel = maxFuel;
            this.m_CurrentFuel = i_CurrentFuel;
            this.m_fuelType = i_FuelType;
        }

        public float CurrentFuel { get; set; }

        public float MaxFuel { get; set; }

        
        public override void AddEnergy(float i_FuelToAdd)
        {
            if(this.m_CurrentFuel + i_FuelToAdd <= this.m_MaxFuel)
            {
                this.m_CurrentFuel += i_FuelToAdd;
            }
            else
            {
                //throw exception
            }
        }
    }
}
