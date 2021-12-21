using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    public class VehicleData
    {
        private string m_CustomerName;
        private string m_CustomerPhone;
        private eVehicleStatus m_VehicleStatus;
        public enum eVehicleStatus
        {
            InFix = 0,
            Fixed,
            Paid,
        }

        public VehicleData(string i_CustomerName, string i_CustomerPhone, eVehicleStatus i_VehicleStatus)
        {
            this.m_CustomerName = i_CustomerName;
            this.m_CustomerPhone = i_CustomerPhone;
            this.m_VehicleStatus = i_VehicleStatus;
        }
        public string CustomerName {
            get
            {
                return this.m_CustomerName;
            }
            set
            {
                this.m_CustomerName = value;
            }
        }
        public string CustomerPhone {
            get
            {
                return this.m_CustomerPhone;
            }
            set
            {
                this.m_CustomerPhone = value;
            }
        }
        public eVehicleStatus VehicleStatus { get; set; }

    }
}
