using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ex03.GarageLogic
{
    public static class Factory
    {
        public enum eVehicleType
        {
            FuelCar = 1,
            ElectricCar,
            FuelMotorcycle,
            ElectricMotorcycle,
            Truck,
        }

        public static Vehicle CreateVehicle(eVehicleType i_VehicleType)
        {
            Vehicle vehicleForCustomer = null;

            switch (i_VehicleType)
            {
                case eVehicleType.FuelCar:
                    vehicleForCustomer = new Car(Vehicle.eFuelType.Fuel);
                    break;

                case eVehicleType.ElectricCar:
                    vehicleForCustomer = new Car(Vehicle.eFuelType.Electric);
                    break;

                case eVehicleType.FuelMotorcycle:
                    vehicleForCustomer = new Motorcycle(Vehicle.eFuelType.Fuel);
                    break;

                case eVehicleType.ElectricMotorcycle:
                    vehicleForCustomer = new Motorcycle(Vehicle.eFuelType.Electric);
                    break;

                case eVehicleType.Truck:
                    vehicleForCustomer = new Truck(Vehicle.eFuelType.Fuel);
                    break;
            }

            return vehicleForCustomer;

        }
    }
}
