using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ex03.GarageLogic
{
    public class Garage
    {
        private Dictionary<string, VehicleInGarage> m_VehicleInGarageArray;

        public Garage()
        {
            m_VehicleInGarageArray = new Dictionary<string, VehicleInGarage>();
        }

        public void AddvehicleToGarage(Vehicle i_NewVehicle, string i_OwnerName, string i_OwnerNumber)
        {
            VehicleInGarage vehicleToAddToGarage = new VehicleInGarage(i_NewVehicle, i_OwnerName, i_OwnerNumber);
            m_VehicleInGarageArray.Add(i_NewVehicle.LicenseNumber, vehicleToAddToGarage);
        }

        public List<string> GetTheListOfVehiclesInTheGarageByStatus(VehicleInGarage.eVehicleStatus i_VehicleStatus)
        {
            List<string> listOfVehiclesInTheGarage = new List<string>();
            string firstStringInList = string.Format("The license numbers of the vehicles that are in the garage in the status {0} are: ",
                                                     VehicleInGarage.convertStatusToString(i_VehicleStatus));
            listOfVehiclesInTheGarage.Add(firstStringInList);

            foreach (VehicleInGarage currentVehicle in m_VehicleInGarageArray.Values)
            {
                if (currentVehicle.Status.Equals(i_VehicleStatus))
                {
                    listOfVehiclesInTheGarage.Add(currentVehicle.Vehicle.LicenseNumber);
                }
            }

            if (listOfVehiclesInTheGarage.Count == 1)
            {
                listOfVehiclesInTheGarage.Clear();
                firstStringInList = string.Format("There is no vehicles with status {0} in the garage! ",
                                                     VehicleInGarage.convertStatusToString(i_VehicleStatus));
                listOfVehiclesInTheGarage.Add(firstStringInList);
            }

            return listOfVehiclesInTheGarage;
        }

        public List<string> GetTheListOfAllVehiclesInTheGarage()
        {
            List<string> listOfVehiclesInTheGarage = new List<string> { "The license numbers of the vehicles that are in the garage are: " };

            foreach (VehicleInGarage currentVehicle in m_VehicleInGarageArray.Values)
            {
                listOfVehiclesInTheGarage.Add(currentVehicle.Vehicle.LicenseNumber);
            }

            if(listOfVehiclesInTheGarage.Count == 1)
            {
                listOfVehiclesInTheGarage.Clear();
                listOfVehiclesInTheGarage.Add("There is no vehicles in the garage! ");
            }

            return listOfVehiclesInTheGarage;
        }

        public void ChangeVehicleStatusInGarage(string i_LicenseNumber, VehicleInGarage.eVehicleStatus i_NewVehicleStatus)
        {
            if (findVehicleInGarage(i_LicenseNumber, out VehicleInGarage currentVehicle))
            {
                currentVehicle.Status = i_NewVehicleStatus;
            }
            else
            {
                throw (new ArgumentException("This vehicle does not exist in the garage!"));
            }
        }

        public void InflatingWheelsToMax(string i_LicenseNumber)
        {
            List<Wheel> wheelsList;

            if (findVehicleInGarage(i_LicenseNumber, out VehicleInGarage currentVehicle))
            {
                wheelsList = currentVehicle.Vehicle.WheelsList;

                for (int i = 0; i < wheelsList.Count; i++)
                {
                    wheelsList[i].InflateAirPressureToMax();
                }
            }
            else
            {
                throw (new ArgumentException("This vehicle does not exist in the garage!"));
            }
        }

        public void AddFuel(string i_LicenseNumber, FuelEngine.eFuelType i_FuelType, float i_FuelToAdd)
        {
            if (findVehicleInGarage(i_LicenseNumber, out VehicleInGarage currentVehicle))
            {
                if (currentVehicle.Vehicle.Engine is FuelEngine)
                {
                    FuelEngine vehicleFuelEngine = currentVehicle.Vehicle.Engine as FuelEngine;
                    vehicleFuelEngine.AddFuel(i_FuelToAdd, i_FuelType);
                }
                else
                {
                    throw (new ArgumentException("Your vehicle is not powered by fuel!"));
                }
            }
            else
            {
                throw (new ArgumentException("This vehicle does not exist in the garage!"));
            }
        }

        public void AddCharge(string i_LicenseNumber, float i_MinutesOfChargeToAdd)
        {
            if (findVehicleInGarage(i_LicenseNumber, out VehicleInGarage currentVehicle))
            {
                if (currentVehicle.Vehicle.Engine is ElectricEngine)
                {
                    ElectricEngine vehicleElectricEngine = currentVehicle.Vehicle.Engine as ElectricEngine;
                    vehicleElectricEngine.AddChargeToBattery(i_MinutesOfChargeToAdd);
                }
                else
                {
                    throw (new ArgumentException("Your vehicle is not electric!"));
                }
            }
            else
            {
                throw (new ArgumentException("This vehicle does not exist in the garage!"));
            }
        }

        public string GetAllDetailsAboutSpecificVehicle(string i_LicenseNumber)
        {
            string outputString;

            if (findVehicleInGarage(i_LicenseNumber, out VehicleInGarage currentVehicle))
            {
                outputString = vehicleDetails(currentVehicle);
            }
            else
            {
                throw (new ArgumentException("This vehicle does not exist in the garage!"));
            }

            return outputString;
        }

        private string vehicleDetails(VehicleInGarage i_CurrentVehicleInGarage)
        {
            StringBuilder vehicleStringDetails = new StringBuilder();
            Vehicle currentVehicle = i_CurrentVehicleInGarage.Vehicle;

            vehicleStringDetails.AppendFormat(
@"License number: {0}
Owner name: {1}
Owner number: {2}
Statuse in garage: {3}

Model name: {4} 
Energy precent left: {5}

Wheels details: 
{6}
{7}
{8}", currentVehicle.LicenseNumber, i_CurrentVehicleInGarage.OwnerName, i_CurrentVehicleInGarage.OwnerNumber,
      VehicleInGarage.convertStatusToString(i_CurrentVehicleInGarage.Status), currentVehicle.ModelName,
      currentVehicle.EnergyPrecentLeft, currentVehicle.WheelsListData(), currentVehicle.VehicleDetails(), currentVehicle.EngineDetails());

            return vehicleStringDetails.ToString();
        }

        private bool findVehicleInGarage(string i_LicenseNumber, out VehicleInGarage o_VehicleInGarage)
        {
            return m_VehicleInGarageArray.TryGetValue(i_LicenseNumber, out o_VehicleInGarage);
        }

        public bool isVehicleInGarage(string i_LicenseNumber)
        {
            return m_VehicleInGarageArray.ContainsKey(i_LicenseNumber);
        }
    }
}
