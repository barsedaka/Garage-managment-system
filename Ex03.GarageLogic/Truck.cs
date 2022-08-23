using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ex03.GarageLogic
{
    public class Truck : Vehicle
    {
        public enum eIndexInUserRequestsListTruck
        {
            IsDrivesContent = 0,
            ContentCapacity,
        }

        private bool m_IsDrivesContent;
        private float m_ContentCapacity;

        public Truck(Vehicle.eFuelType i_FuelType) : base(16, 24)
        {
            SetEngine(i_FuelType);
        }

        public override void SetEngine(eFuelType i_FuelType)
        {
            m_Engine = new FuelEngine(FuelEngine.eFuelType.Soler, 120);
        }

        public override List<string> VehicleRequestsFromUser()
        {
            List<string> requestsFromUser = new List<string>();

            requestsFromUser.Add(
@"Do you drive refrigerated contents? 
1. Yes
2. No");
            requestsFromUser.Add("Content capacity: ");

            return requestsFromUser;
        }

        public override void SetSpecificVehicleDetails(int i_RequestIndex, string i_UserStringInput)
        {
            switch (i_RequestIndex)
            {
                case (int)eIndexInUserRequestsListTruck.IsDrivesContent:
                    CheckIfDrivesContentIsValidAndUpdate(i_UserStringInput);
                    break;

                case (int)eIndexInUserRequestsListTruck.ContentCapacity:
                    checkIfContentCapacityIsValidAndUpdate(i_UserStringInput);
                    break;
            }
        }

        private void CheckIfDrivesContentIsValidAndUpdate(string i_UserStringInput)
        {
            if (Validation.CheckIfValidIntNumberAndInRange(i_UserStringInput, 1, 2, out int userChoice))
            {
                m_IsDrivesContent = userChoice == 1;
            }
        }

        private void checkIfContentCapacityIsValidAndUpdate(string i_UserStringInput)
        {
            if(Validation.IsValidFloatNumber(i_UserStringInput, 0, false, out float contentCapacity))
            {
                m_ContentCapacity = contentCapacity;
            }
        }

        public override string VehicleDetails()
        {
            StringBuilder vehicleStringDetails = new StringBuilder();

            vehicleStringDetails.AppendFormat(
@"Drives content: {0}
Content capacity: {1}", m_IsDrivesContent.ToString(), m_ContentCapacity);

            return vehicleStringDetails.ToString();
        }
    }
}
