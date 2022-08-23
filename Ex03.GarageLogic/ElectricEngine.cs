using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ex03.GarageLogic
{
    public class ElectricEngine : Engine
    {
        private float m_HouresLeftInBattery;
        private readonly float m_MaxHouresInBattery;

        public ElectricEngine(float i_MaxHouresInBattery)
        {
            m_MaxHouresInBattery = i_MaxHouresInBattery;
        }

        internal void AddChargeToBattery(float i_TimeToAddInMinutes)
        {
            float timeToAddInHoures = i_TimeToAddInMinutes / 60f;
            float chargeAfterAdd = m_HouresLeftInBattery + timeToAddInHoures;

            if (chargeAfterAdd > m_MaxHouresInBattery)
            {
                throw (new ValueOutOfRangeException(0, m_MaxHouresInBattery - m_HouresLeftInBattery));
            }
            else
            {
                m_HouresLeftInBattery = chargeAfterAdd;
            }
        }

        public override List<string> RequestsFromUser()
        {
            List<string> requestsFromUser = new List<string>{"Houres left in battery: "};

            return requestsFromUser;
        }

        public override void SetSpecificDetailByEngine(int i_RequestIndex, string i_UserStringInput)
        {
            checkIfHouresLeftInBatteryIsValidAndUpdate(i_UserStringInput);
        }

        private void checkIfHouresLeftInBatteryIsValidAndUpdate(string i_UserStringInput)
        {
            if (Validation.IsValidFloatNumber(i_UserStringInput, m_MaxHouresInBattery, true, out float houresLeftInBattery))
            {
                m_HouresLeftInBattery = houresLeftInBattery;
            }
        }

        public override string EngineStringDetails()
        {
            StringBuilder vehicleStringDetails = new StringBuilder();

            vehicleStringDetails.AppendFormat(
@"Houers left in battery: {0}", m_HouresLeftInBattery);

            return vehicleStringDetails.ToString();
        }
    }
}
