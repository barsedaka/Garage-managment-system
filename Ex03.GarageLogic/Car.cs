using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ex03.GarageLogic
{
    public class Car : Vehicle
    {
        public enum eColor
        {
            Red = 1,
            White,
            Green,
            Blue,
        }

        public enum eIndexInUserRequestsListCar
        {
            Color = 0,
            DoorsNumber,
        }

        private eColor m_CarColor;
        private int m_DoorsNumber;

        public Car(Vehicle.eFuelType i_FuelType) : base(4, 29)
        {
            SetEngine(i_FuelType);
        }

        public override void SetEngine(eFuelType i_FuelType)
        {
            if (i_FuelType == eFuelType.Fuel)
            {
                m_Engine = new FuelEngine(FuelEngine.eFuelType.Octan95, 38f);
            }
            else
            {
                m_Engine = new ElectricEngine(3.3f);
            }
        }

        public override List<string> VehicleRequestsFromUser()
        {
            List<string> requestsFromUser = new List<string>{
@"Car color: 
1. Red
2. White
3. Green
4. Blue", "Number of doors: "};

            return requestsFromUser;
        }

        public override void SetSpecificVehicleDetails(int i_RequestIndex, string i_UserStringInput)
        {
            switch (i_RequestIndex)
            {
                case (int)eIndexInUserRequestsListCar.Color:
                    checkIfColorIsValidAndUpdate(i_UserStringInput);
                    break;

                case (int)eIndexInUserRequestsListCar.DoorsNumber:
                    checkIfDoorsNumberIsValidAndUpdate(i_UserStringInput);
                    break;
            }
        }

        private void checkIfColorIsValidAndUpdate(string i_UserStringInput)
        {
            if (Validation.CheckIfValidIntNumberAndInRange(i_UserStringInput, 1, 4, out int colorChoice))
            {
                m_CarColor = (eColor)colorChoice;
            }
        }

        private void checkIfDoorsNumberIsValidAndUpdate(string i_UserStringInput)
        {
            if (Validation.CheckIfValidIntNumberAndInRange(i_UserStringInput, 2, 5, out int doorsNumberChoice))
            {
                m_DoorsNumber = doorsNumberChoice;
            }
        }

        public override string VehicleDetails()
        {
            StringBuilder vehicleStringDetails = new StringBuilder();

            vehicleStringDetails.AppendFormat(
@"Car color: {0}
Doors number: {1}", m_CarColor.ToString(), m_DoorsNumber);

            return vehicleStringDetails.ToString();
        }
    }
}
