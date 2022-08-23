using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ex03.GarageLogic;

namespace Ex03.ConsoleUI
{
    public class GarageUI
    {
        public enum eGarageFunctions
        {
            AddVehicleToGarage = 1,
            DisplayAListOfVehicleLicenseNumberInTheGarage,
            ChangeVehicleStatusInGarage,
            InflatingVehicleWheelsToMax,
            AddFuelToVehicle,
            ChargeElectricVehicle,
            ViewVehicleData,
            Exit,
        }

        private readonly Garage r_Garage;
        private readonly GarageUIOutput r_GarageUIOutput;
        private bool m_GarageIsOpen;

        public GarageUI()
        {
            r_Garage = new Garage();
            r_GarageUIOutput = new GarageUIOutput();
            m_GarageIsOpen = true;
        }

        public void RunProgram()
        {
            eGarageFunctions userChoice;

            while (m_GarageIsOpen)
            {
                r_GarageUIOutput.MainMenu();
                userChoice = (eGarageFunctions)getUserChoiceAndCheckValidityByRangeNumbers(1, 8);
                activateFunctionAccordingToTheUserChoice(userChoice);
                if(m_GarageIsOpen)
                {
                    chooseToContinue();
                }
            }
        }

        private void activateFunctionAccordingToTheUserChoice(eGarageFunctions i_UserChoice)
        {
            switch (i_UserChoice)
            {
                case eGarageFunctions.AddVehicleToGarage:
                    addVehicleToGarage();
                    break;

                case eGarageFunctions.DisplayAListOfVehicleLicenseNumberInTheGarage:
                    displayTheListOfVehiclesInTheGarage();
                    break;

                case eGarageFunctions.ChangeVehicleStatusInGarage:
                    changeVehicleStatusInGarage();
                    break;

                case eGarageFunctions.InflatingVehicleWheelsToMax:
                    inflatingVehicleWheelsToMax();
                    break;

                case eGarageFunctions.AddFuelToVehicle:
                    addFuelToVehicle();
                    break;

                case eGarageFunctions.ChargeElectricVehicle:
                    chargeElectricVehicle();
                    break;

                case eGarageFunctions.ViewVehicleData:
                    viewVehicleData();
                    break;

                case eGarageFunctions.Exit:
                    m_GarageIsOpen = false;
                    break;
            }
        }

        private void addVehicleToGarage()
        {
            int userChoice;
            Vehicle vehicleToAdd;
            string licenseNumber, ownerName, ownerNumber;

            licenseNumber = getStringWithOnlyLettersAndDigits("Please enter the license number of your vehicle: ");
            if (r_Garage.isVehicleInGarage(licenseNumber))
            {
                r_GarageUIOutput.PrintVehicleAlreadyInGarage();
                r_Garage.ChangeVehicleStatusInGarage(licenseNumber, VehicleInGarage.eVehicleStatus.InRepair);
            }
            else
            {
                ownerName = getStringWithOnlyLetters("Owner name: ");
                ownerNumber = getStringWithOnlyDigits("Owner number: ");
                r_GarageUIOutput.MenuOfVehiclesToAddToGarage();
                userChoice = getUserChoiceAndCheckValidityByRangeNumbers(1, 5);
                vehicleToAdd = Factory.CreateVehicle((Factory.eVehicleType)userChoice);
                setVehicleDetails(vehicleToAdd, licenseNumber);
                r_Garage.AddvehicleToGarage(vehicleToAdd, ownerName, ownerNumber);
                r_GarageUIOutput.PrintVehicleAddedSuccesfully();
            }
        }

        private void displayTheListOfVehiclesInTheGarage()
        {
            int userChoice;
            r_GarageUIOutput.ViewAllVehiclesOrFilterByStatus();
            userChoice = getUserChoiceAndCheckValidityByRangeNumbers(1, 2);
            List<string> listOfVehiclesInTheGarage = null;

            switch (userChoice)
            {
                case 1:
                    listOfVehiclesInTheGarage = displayTheListOfVehiclesInTheGarageByStatus();
                    break;

                case 2:
                    listOfVehiclesInTheGarage = r_Garage.GetTheListOfAllVehiclesInTheGarage();
                    break;
            }

            r_GarageUIOutput.PrintListOfVehiclesInGarage(listOfVehiclesInTheGarage);
        }

        private List<string> displayTheListOfVehiclesInTheGarageByStatus()
        {
            int userChoice;
            r_GarageUIOutput.ViewListOfAllVehiclesStatusInGarage();
            userChoice = getUserChoiceAndCheckValidityByRangeNumbers(1, 3);
            List<string> listOfVehiclesInTheGarage = r_Garage.GetTheListOfVehiclesInTheGarageByStatus((VehicleInGarage.eVehicleStatus)userChoice);

            return listOfVehiclesInTheGarage;
        }

        private void changeVehicleStatusInGarage()
        {
            string licenseNumber = getStringWithOnlyLettersAndDigits("Please enter the license number of your vehicle: ");
            r_GarageUIOutput.AskForNewVehicleStatus();
            int userChoice = getUserChoiceAndCheckValidityByRangeNumbers(1, 3);

            try
            {
                r_Garage.ChangeVehicleStatusInGarage(licenseNumber, (VehicleInGarage.eVehicleStatus)userChoice);
                r_GarageUIOutput.PrintStatusChangedSuccesfully();
            }
            catch (ArgumentException ex)
            {
                r_GarageUIOutput.PrintExceptionMessage(ex.Message);
            }
        }

        private void inflatingVehicleWheelsToMax()
        {
            string licenseNumber = getStringWithOnlyLettersAndDigits("Please enter the license number of your vehicle: ");

            try
            {
                r_Garage.InflatingWheelsToMax(licenseNumber);
                r_GarageUIOutput.PrintWheelsInflatedSuccesfully();
            }
            catch (ArgumentException ex)
            {
                r_GarageUIOutput.PrintExceptionMessage(ex.Message);
            }
        }

        private void addFuelToVehicle()
        {
            string licenseNumber = getStringWithOnlyLettersAndDigits("Please enter the license number of your vehicle: ");
            FuelEngine.eFuelType fuelType = (FuelEngine.eFuelType)getFuelTypeAndCheckValidity();
            float fuelToAdd = getFloatNumberAndCheckValidity("The amount of fuel you want to add to your vehicle: ", 0, false);

            try
            {
                r_Garage.AddFuel(licenseNumber, fuelType, fuelToAdd);
                r_GarageUIOutput.PrintFuelAddedSuccesfully();
            }
            catch(ArgumentException ex)
            {
                r_GarageUIOutput.PrintExceptionMessage(ex.Message);
            }
            catch(ValueOutOfRangeException ex)
            {
                r_GarageUIOutput.PrintExceptionMessage(ex.Message);
            }
        }

        private void chargeElectricVehicle()
        {
            string licenseNumber = getStringWithOnlyLettersAndDigits("Please enter the license number of your vehicle: ");
            float minutesOfChargeToAdd = getFloatNumberAndCheckValidity("The amount of charging minutes you want to add to the vehicle: ", 0, false);

            try
            {
                r_Garage.AddCharge(licenseNumber, minutesOfChargeToAdd);
                r_GarageUIOutput.PrintChargAddedSuccesfully();
            }
            catch (ArgumentException ex)
            {
                r_GarageUIOutput.PrintExceptionMessage(ex.Message);
            }
            catch (ValueOutOfRangeException ex)
            {
                r_GarageUIOutput.PrintExceptionMessage(ex.Message);
            }
        }

        private void viewVehicleData()
        {
            string licenseNumber = getStringWithOnlyLettersAndDigits("Please enter the license number of your vehicle: ");
            string vehicleDetails;

            try
            {
                vehicleDetails = r_Garage.GetAllDetailsAboutSpecificVehicle(licenseNumber);
                r_GarageUIOutput.Print(vehicleDetails);
            }
            catch (ArgumentException ex)
            {
                r_GarageUIOutput.PrintExceptionMessage(ex.Message);
            }
        }

        private int getUserChoiceAndCheckValidityByRangeNumbers(int i_MinimumNumber, int i_MaximumNumber)
        {
            string stringUserChoice = string.Empty;
            int userChoice;
            bool isChoiceValid = false;

            while (!isChoiceValid)
            {
                stringUserChoice = Console.ReadLine();
                checkUserChoiceByRangeOfNumbers(stringUserChoice, ref isChoiceValid, i_MinimumNumber, i_MaximumNumber);
            }
            userChoice = int.Parse(stringUserChoice);

            return userChoice;
        }

        private void checkUserChoiceByRangeOfNumbers(string i_StringUserChoice, ref bool io_IsChoiceValid,
                                                     int i_MinimumNumber, int i_MaximumNumber)
        {
            try
            {
                if (Validation.CheckIfValidIntNumberAndInRange(i_StringUserChoice, i_MinimumNumber, i_MaximumNumber, out int temp))
                {
                    io_IsChoiceValid = true;
                }
            }
            catch (FormatException ex)
            {
                r_GarageUIOutput.PrintExceptionMessage(ex.Message);
            }
            catch (ValueOutOfRangeException ex)
            {
                r_GarageUIOutput.PrintExceptionMessage(ex.Message);
            }
        }

        private void setVehicleDetails(Vehicle i_VehicleToAdd, string i_LicenseNumber)
        {
            bool isEngine = false;
            string wheelManufacturerName;
            float wheelCurrentAirPressure;
            List<string> vehicleRequestsFromUser, engineRequestsFromUser;

            i_VehicleToAdd.LicenseNumber = i_LicenseNumber;
            i_VehicleToAdd.ModelName = getStringWithOnlyLettersAndDigits("Model name: ");
            i_VehicleToAdd.EnergyPrecentLeft = getFloatNumberAndCheckValidity("Energy precent left: ", 0, false);
            wheelManufacturerName = getStringWithOnlyLetters("Wheel Manufacturer name: ");
            wheelCurrentAirPressure = getFloatNumberAndCheckValidity("Wheel Current Air Pressure: ",
                                                                      i_VehicleToAdd.WheelsList[0].MaxAirPressure, true);
            i_VehicleToAdd.SetWheelsList(wheelManufacturerName, wheelCurrentAirPressure);
            vehicleRequestsFromUser = i_VehicleToAdd.VehicleRequestsFromUser();
            setVehicleDetailsByRequestsFromUserList(vehicleRequestsFromUser, i_VehicleToAdd, isEngine);
            isEngine = true;
            engineRequestsFromUser = i_VehicleToAdd.EngineRequestFromUser();
            setVehicleDetailsByRequestsFromUserList(engineRequestsFromUser, i_VehicleToAdd, isEngine);
        }

        private void setVehicleDetailsByRequestsFromUserList(List<string> i_RequestsFromUser, Vehicle i_VehicleToAdd, bool i_IsEngine)
        {
            for (int i = 0; i < i_RequestsFromUser.Count; i++)
            {
                r_GarageUIOutput.Print(i_RequestsFromUser[i]);
                getValidInputBySpecificRequestAndUpdateVehicleDetails(i_VehicleToAdd, i, i_IsEngine);
            }
        }

        private void getValidInputBySpecificRequestAndUpdateVehicleDetails(Vehicle i_VehicleToAdd, int i_RequestIndex, bool i_IsEngine)
        {
            string userInput;
            bool isValidInput = false;

            while (!isValidInput)
            {
                userInput = Console.ReadLine();
                checkValidInputBySpecificRequest(userInput, ref isValidInput, i_VehicleToAdd, i_RequestIndex, i_IsEngine);
            }
        }

        private void checkValidInputBySpecificRequest(string i_UserStringInput, ref bool io_IsValidInput,
                                                        Vehicle i_VehicleToAdd, int i_RequestIndex, bool i_IsEngine)
        {
            try
            {
                if (i_IsEngine)
                {
                    i_VehicleToAdd.SetSpecificEngineDetails(i_RequestIndex, i_UserStringInput);
                }
                else
                {
                    i_VehicleToAdd.SetSpecificVehicleDetails(i_RequestIndex, i_UserStringInput);
                }

                io_IsValidInput = true;
            }
            catch (FormatException ex)
            {
                r_GarageUIOutput.PrintExceptionMessage(ex.Message);
            }
            catch (ValueOutOfRangeException ex)
            {
                r_GarageUIOutput.PrintExceptionMessage(ex.Message);
            }
            catch (ArgumentException ex)
            {
                r_GarageUIOutput.PrintExceptionMessage(ex.Message);
            }
        }

        private string getStringWithOnlyDigits(string i_AskForUserString)
        {
            string stringUserInput = string.Empty;
            bool isUserInputValid = false;

            r_GarageUIOutput.Print(i_AskForUserString);
            while (!isUserInputValid)
            {
                stringUserInput = Console.ReadLine();
                checkIfContainsOnlyDigits(stringUserInput, ref isUserInputValid);
            }

            return stringUserInput;
        }

        private void checkIfContainsOnlyDigits(string i_StringUserInput, ref bool io_IsChoiceValid)
        {
            try
            {
                if (Validation.IsAllDigits(i_StringUserInput))
                {
                    io_IsChoiceValid = true;
                }
            }
            catch (FormatException ex)
            {
                r_GarageUIOutput.PrintExceptionMessage(ex.Message);
            }
        }

        private string getStringWithOnlyLetters(string i_AskForUserString)
        {
            string stringUserInput = string.Empty;
            bool isUserInputValid = false;

            r_GarageUIOutput.Print(i_AskForUserString);
            while (!isUserInputValid)
            {
                stringUserInput = Console.ReadLine();
                checkIfContainsOnlyLetters(stringUserInput, ref isUserInputValid);
            }

            return stringUserInput;
        }

        private void checkIfContainsOnlyLetters(string i_StringUserInput, ref bool io_IsChoiceValid)
        {
            try
            {
                if (Validation.IsAllLetters(i_StringUserInput))
                {
                    io_IsChoiceValid = true;
                }
            }
            catch (FormatException ex)
            {
                r_GarageUIOutput.PrintExceptionMessage(ex.Message);
            }
        }

        private string getStringWithOnlyLettersAndDigits(string i_AskForUserString)
        {
            string stringUserInput = string.Empty;
            bool isUserInputValid = false;

            r_GarageUIOutput.Print(i_AskForUserString);
            while (!isUserInputValid)
            {
                stringUserInput = Console.ReadLine();
                checkIfContainsOnlyLettersAndDigits(stringUserInput, ref isUserInputValid);
            }

            return stringUserInput;
        }

        private void checkIfContainsOnlyLettersAndDigits(string i_StringUserInput, ref bool io_IsChoiceValid)
        {
            try
            {
                if (Validation.IsAllLettersOrDigits(i_StringUserInput))
                {
                    io_IsChoiceValid = true;
                }
            }
            catch (FormatException ex)
            {
                r_GarageUIOutput.PrintExceptionMessage(ex.Message);
            }
        }

        private float getFloatNumberAndCheckValidity(string i_AskForUserString, float i_Maximum, bool i_NeedToCheckMax)
        {
            string stringUserChoice = string.Empty;
            float userFloatNumberChoice;
            bool isChoiceValid = false;

            r_GarageUIOutput.Print(i_AskForUserString);
            while (!isChoiceValid)
            {
                stringUserChoice = Console.ReadLine();
                checkIfFloatNumberAndValidity(stringUserChoice, ref isChoiceValid, i_Maximum, i_NeedToCheckMax);
            }

            userFloatNumberChoice = float.Parse(stringUserChoice);

            return userFloatNumberChoice;
        }

        private void checkIfFloatNumberAndValidity(string i_StringUserInput, ref bool io_IsChoiceValid, float i_Maximum, bool i_NeedToCheckMax)
        {
            try
            {
                if (Validation.IsValidFloatNumber(i_StringUserInput, i_Maximum, i_NeedToCheckMax, out float temp))
                {
                    io_IsChoiceValid = true;
                }
            }
            catch (FormatException ex)
            {
                r_GarageUIOutput.PrintExceptionMessage(ex.Message);
            }
            catch (ArgumentException ex)
            {
                r_GarageUIOutput.PrintExceptionMessage(ex.Message);
            }
        }

        private int getFuelTypeAndCheckValidity()
        {
            int fuelType;

            r_GarageUIOutput.AskForFuelType();
            fuelType = getUserChoiceAndCheckValidityByRangeNumbers(1, 4);

            return fuelType;
        }

        private void chooseToContinue()
        {
            r_GarageUIOutput.AskForContinue();
            Console.ReadLine();
        }
    }
}
