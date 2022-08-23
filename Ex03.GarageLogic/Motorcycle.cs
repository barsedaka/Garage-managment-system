using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ex03.GarageLogic
{
    public class Motorcycle : Vehicle
    {
        public enum eLicenseType
        {
            A = 1,
            A1,
            B1,
            BB,
        }

        public enum eIndexInUserRequestsListMotorcycle
        {
            LicenseType = 0,
            EngineCapacity,
        }

        private eLicenseType m_LicenseType;
        private int m_EngineCapacity;

        public Motorcycle(Vehicle.eFuelType i_FuelType) : base(2, 31)
        {
            SetEngine(i_FuelType);
        }

        public override void SetEngine(eFuelType i_FuelType)
        {
            if (i_FuelType == eFuelType.Fuel)
            {
                m_Engine = new FuelEngine(FuelEngine.eFuelType.Octan98, 6.2f);
            }
            else
            {
                m_Engine = new ElectricEngine(2.5f);
            }
        }

        public override List<string> VehicleRequestsFromUser()
        {
            List<string> requestsFromUser = new List<string>();

            requestsFromUser.Add(
 @"License Type: 
1. A
2. A1
3. B1
4. BB");
            requestsFromUser.Add("Engine capacity: ");

            return requestsFromUser;
        }

        public override void SetSpecificVehicleDetails(int i_RequestIndex, string i_UserStringInput)
        {
            switch (i_RequestIndex)
            {
                case (int)eIndexInUserRequestsListMotorcycle.LicenseType:
                    CheckIfLicenseTypeIsValidAndUpdate(i_UserStringInput);
                    break;

                case (int)eIndexInUserRequestsListMotorcycle.EngineCapacity:
                    checkIfEngineCapacityIsValidAndUpdate(i_UserStringInput);
                    break;
            }
        }

        private void CheckIfLicenseTypeIsValidAndUpdate(string i_UserStringInput)
        {
            if (Validation.CheckIfValidIntNumberAndInRange(i_UserStringInput, 1, 4, out int LicenseChoice))
            {
                m_LicenseType = (eLicenseType)LicenseChoice;
            }
        }

        private void checkIfEngineCapacityIsValidAndUpdate(string i_UserStringInput)
        {
            int engineCapacity;

            if (!Int32.TryParse(i_UserStringInput, out engineCapacity))
            {
                throw (new FormatException("Wrong input!!!! "));
            }

            m_EngineCapacity = engineCapacity;
        }

        public override string VehicleDetails()
        {
            StringBuilder vehicleStringDetails = new StringBuilder();

            vehicleStringDetails.AppendFormat(
@"License type: {0}
Engine capacity: {1}", m_LicenseType.ToString(), m_EngineCapacity);

            return vehicleStringDetails.ToString();
        }
    }
}
