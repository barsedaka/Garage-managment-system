using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ex03.GarageLogic
{
    public class Wheel
    {
        private string m_ManufacturerName;
        private float m_CurrentAirPressure;
        private readonly float r_MaxAirPressure;

        public Wheel(float i_MaxAirPressure)
        {
            r_MaxAirPressure = i_MaxAirPressure;
        }

        public string ManufacturerName
        {
            get { return m_ManufacturerName; }
            set { m_ManufacturerName = value; }
        }

        public float CurrentAirPressure
        {
            get { return m_CurrentAirPressure; }
            set { m_CurrentAirPressure = value; }
        }

        public float MaxAirPressure
        {
            get { return r_MaxAirPressure; }
        }

        public void InflateAirPressure(float i_AirToAdd)
        {
            float airAfterAdd = i_AirToAdd + m_CurrentAirPressure;

            if (airAfterAdd <= r_MaxAirPressure)
            {
                m_CurrentAirPressure = airAfterAdd;
            }
            else
            {
                throw new ValueOutOfRangeException(0, r_MaxAirPressure - m_CurrentAirPressure);
            }
        }

        public void InflateAirPressureToMax()
        {
            m_CurrentAirPressure = r_MaxAirPressure;
        }

        public string WheelDetails()
        {
            StringBuilder wheelStringDetails = new StringBuilder();

            wheelStringDetails.AppendFormat(
@"Manufacturer Name: {0}
Current air pressure: {1}
", m_ManufacturerName, m_CurrentAirPressure);

            return wheelStringDetails.ToString();
        }
    }
}
