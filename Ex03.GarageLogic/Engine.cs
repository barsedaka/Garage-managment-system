using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ex03.GarageLogic
{
    public abstract class Engine
    {
        public abstract List<string> RequestsFromUser();

        public abstract void SetSpecificDetailByEngine(int i_RequestIndex, string i_UserStringInput);

        public abstract string EngineStringDetails();
    }
}
