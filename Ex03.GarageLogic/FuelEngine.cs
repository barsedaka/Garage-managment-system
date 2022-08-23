using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ex03.GarageLogic
{
    public class FuelEngine : Engine
    {
        public enum eFuelType
        {
            Soler = 1,
            Octan95,
            Octan96,
            Octan98,
        }

        private readonly eFuelType m_FuelType;
        private float m_CurrentFuelLiters;
        private readonly float m_MaxFuelLiters;

        public FuelEngine(eFuelType i_FuelType, float i_MaxFuelLiters)
        {
            m_FuelType = i_FuelType;
            m_MaxFuelLiters = i_MaxFuelLiters;
        }

        internal void AddFuel(float i_FuelToAdd, eFuelType i_FuelType)
        {
            float fuelAfterAdd = m_CurrentFuelLiters + i_FuelToAdd;

            if (i_FuelType != m_FuelType)
            {
                throw (new ArgumentException("Wrong Fuel Type!"));
            }
            else if (fuelAfterAdd > m_MaxFuelLiters)
            {
                throw (new ValueOutOfRangeException(0, m_MaxFuelLiters - m_CurrentFuelLiters));
            }
            else
            {
                m_CurrentFuelLiters = fuelAfterAdd;
            }
        }

        public override List<string> RequestsFromUser()
        {
            List<string> requestsFromUser = new List<string> { "Current fuel liters: " };

            return requestsFromUser;
        }

        public override void SetSpecificDetailByEngine(int i_RequestIndex, string i_UserStringInput)
        {
            checkIfCurrentFuelLitersIsValidAndUpdate(i_UserStringInput);
        }

        private void checkIfCurrentFuelLitersIsValidAndUpdate(string i_UserStringInput)
        {
            if(Validation.IsValidFloatNumber(i_UserStringInput, m_MaxFuelLiters, true, out float currentFuelLiters))
            {
                m_CurrentFuelLiters = currentFuelLiters;
            }
        }

        public override string EngineStringDetails()
        {
            StringBuilder vehicleStringDetails = new StringBuilder();

            vehicleStringDetails.AppendFormat(
@"Fuel type: {0}
Current fuel liters: {1}", m_FuelType.ToString(), m_CurrentFuelLiters);

            return vehicleStringDetails.ToString();
        }
    }
}
