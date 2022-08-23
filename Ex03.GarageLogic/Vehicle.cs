using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ex03.GarageLogic
{
    public abstract class Vehicle
    {
        public enum eFuelType
        {
            Fuel = 1,
            Electric,
        }

        private string m_ModelName;
        private string m_LicenseNumber;
        private float m_EnergyPrecentLeft;
        protected List<Wheel> m_WheelsList;
        protected Engine m_Engine;

        public Vehicle(int i_NumberOfWheels, int i_MaxAirPressure)
        {
            setWheelsList(i_NumberOfWheels, i_MaxAirPressure);
        }

        public string ModelName
        {
            get { return m_ModelName; }
            set { m_ModelName = value; }
        }

        public string LicenseNumber
        {
            get { return m_LicenseNumber; }
            set { m_LicenseNumber = value; }
        }

        public float EnergyPrecentLeft
        {
            get { return m_EnergyPrecentLeft; }
            set { m_EnergyPrecentLeft = value; }
        }

        public List<Wheel> WheelsList
        {
            get { return m_WheelsList; }
        }

        public Engine Engine
        {
            get { return m_Engine; }
        }

        private void setWheelsList(int i_NumberOfWheels, int i_MaxAirPressure)
        {
            m_WheelsList = new List<Wheel>(i_NumberOfWheels);

            for (int i = 0; i < i_NumberOfWheels; i++)
            {
                m_WheelsList.Add(new Wheel(i_MaxAirPressure));
            }
        }

        public abstract void SetEngine(eFuelType i_FuelType);

        public abstract List<string> VehicleRequestsFromUser();

        public List<string> EngineRequestFromUser()
        {
            return m_Engine.RequestsFromUser();
        }

        public abstract void SetSpecificVehicleDetails(int i_RequestIndex, string i_UserStringInput);

        public void SetSpecificEngineDetails(int i_RequestIndex, string i_UserStringInput)
        {
            m_Engine.SetSpecificDetailByEngine(i_RequestIndex, i_UserStringInput);
        }

        public void SetWheelsList(string i_ManufacturerName, float i_CurrentAirPressure)
        {
            for (int i = 0; i < m_WheelsList.Count; i++)
            {
                m_WheelsList[i].ManufacturerName = i_ManufacturerName;
                m_WheelsList[i].CurrentAirPressure = i_CurrentAirPressure;
            }
        }

        public string WheelsListData()
        {
            StringBuilder wheelsStringDetails = new StringBuilder();
            string currentWheelDetails;

            for (int i = 0; i < m_WheelsList.Count; i++)
            {
                currentWheelDetails = m_WheelsList[i].WheelDetails();
                wheelsStringDetails.AppendFormat(
@"Wheel number {0}:
{1}", i + 1, currentWheelDetails);
            }

            return wheelsStringDetails.ToString();
        }

        public abstract string VehicleDetails();

        public string EngineDetails()
        {
            string engineStringData = m_Engine.EngineStringDetails();

            return engineStringData;
        }
    }
}
