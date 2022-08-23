using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ex03.ConsoleUI
{
    internal class GarageUIOutput
    {
        internal void MainMenu()
        {
            Console.Clear();
            StringBuilder msg = new StringBuilder();

            msg.AppendFormat(
@"Please select one of the following options:
1. Add a new vehicle to the garage
2. View the list of license numbers of the vehicles in the garage
3. Change the condition of a vehicle in the garage
4. Inflating the wheels of a vehicle to the maximum
5. Refuel a vehicle that is powered by fuel
6. Charge an electric vehicle
7. View vehicle data
8. Exit the program");

            Console.WriteLine(msg);
        }

        internal void Print(string i_StringToPrint)
        {
            Console.Clear();
            Console.WriteLine(i_StringToPrint);
        }

        internal void PrintVehicleAlreadyInGarage()
        {
            Console.WriteLine("This vehicle already exists in the garage!");
        }

        internal void PrintExceptionMessage(string i_Message)
        {
            Console.WriteLine(i_Message);
            Console.WriteLine("Please try again");
        }

        internal void MenuOfVehiclesToAddToGarage()
        {
            Console.Clear();
            StringBuilder msg = new StringBuilder();

            msg.AppendFormat(
@"What kind of vehicle do you want to put in the garage?
1) Fuel Car
2) Electric Car
3) Fuel Motorcycle
4) Electric Motorcycle
5) Truck");
            Console.WriteLine(msg);
        }

        internal void ViewAllVehiclesOrFilterByStatus()
        {
            Console.Clear();
            StringBuilder msg = new StringBuilder();

            msg.AppendFormat(
@"Would you like to get the list of vehicles in the garage according to their status?
1) Yes
2) No");
            Console.WriteLine(msg);
        }

        internal void ViewListOfAllVehiclesStatusInGarage()
        {
            Console.Clear();
            StringBuilder msg = new StringBuilder();

            msg.AppendFormat(
@"What status would you like to see?
1) In Repair
2) Fixed
3) Paid Up");
            Console.WriteLine(msg);
        }

        internal void PrintListOfVehiclesInGarage(List<string> i_ListOfVehiclesInTheGarage)
        {
            Console.Clear();
            foreach (string licenseNumber in i_ListOfVehiclesInTheGarage)
            {
                Console.WriteLine(licenseNumber);
            }
        }

        internal void AskForNewVehicleStatus()
        {
            Console.Clear();
            StringBuilder msg = new StringBuilder();

            msg.AppendFormat(
@"Please choose the new status of your vehicle
1) In Repair
2) Fixed
3) Paid Up");
            Console.WriteLine(msg);
        }

        internal void PrintVehicleNotInGarage()
        {
            Console.WriteLine("This vehicle is not in the garage!");
        }

        internal void PrintVehicleAddedSuccesfully()
        {
            Console.Clear();
            Console.WriteLine("The vehicle added successfully to the garage!");
        }

        internal void PrintWheelsInflatedSuccesfully()
        {
            Console.Clear();
            Console.WriteLine("The wheels have been inflated successfully!");
        }

        internal void PrintStatusChangedSuccesfully()
        {
            Console.Clear();
            Console.WriteLine("The vehicle status in the garage has been successfully changed!");
        }

        internal void PrintFuelAddedSuccesfully()
        {
            Console.Clear();
            Console.WriteLine("Fuel added successfully!");
        }

        internal void PrintChargAddedSuccesfully()
        {
            Console.Clear();
            Console.WriteLine("Charge added successfully!");
        }

        internal void AskForFuelType()
        {
            Console.Clear();
            StringBuilder msg = new StringBuilder();

            msg.AppendFormat(
@"Choose your fuel type:
1. Soler
2. Octan95
3. Octan96
4. Octan98");
            Console.WriteLine(msg);
        }

        internal void AskForContinue()
        {
            Console.WriteLine();
            Console.WriteLine("Press any key to continue to main menu...");
        }
    }
}
