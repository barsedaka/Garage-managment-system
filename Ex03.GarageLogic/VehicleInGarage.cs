using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ex03.GarageLogic
{
    public class VehicleInGarage
    {
        public enum eVehicleStatus
        {
            InRepair = 1,
            Fixed,
            PaidUp,
        }

        private Vehicle m_Vehicle;
        private readonly string r_OwnerName;
        private readonly string r_OwnerNumber;
        private eVehicleStatus m_VehicleStatus;

        public VehicleInGarage(Vehicle i_NewVehicle, string i_OwnerName, string i_OwnerNumber)
        {
            m_Vehicle = i_NewVehicle;
            r_OwnerName = i_OwnerName;
            r_OwnerNumber = i_OwnerNumber;
            m_VehicleStatus = eVehicleStatus.InRepair;
        }

        internal Vehicle Vehicle
        {
            get { return m_Vehicle; }
        }

        internal string OwnerName
        {
            get { return r_OwnerName; }
        }

        internal string OwnerNumber
        {
            get { return r_OwnerNumber; }
        }

        internal eVehicleStatus Status
        {
            get { return m_VehicleStatus; }
            set { m_VehicleStatus = value; }
        }

        internal static string convertStatusToString(eVehicleStatus i_Status)
        {
            string stringStatus = string.Empty;

            switch(i_Status)
            {
                case eVehicleStatus.InRepair:
                    stringStatus = "In repair";
                    break;

                case eVehicleStatus.Fixed:
                    stringStatus = "Fixed";
                    break;

                case eVehicleStatus.PaidUp:
                    stringStatus = "Paid";
                    break;
            }

            return stringStatus;
        }
    }
}
