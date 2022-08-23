using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ex03.GarageLogic
{
    public static class Validation
    {
        public static bool CheckIfValidIntNumberAndInRange(string i_UserStringInput, int i_Minimum, int i_Maximum, out int o_Choice)
        {
            bool isValidNumberInput = false;

            if (!Int32.TryParse(i_UserStringInput, out o_Choice))
            {
                throw (new FormatException("Wrong input!!!! "));
            }
            else if (o_Choice < i_Minimum || o_Choice > i_Maximum)
            {
                throw (new ValueOutOfRangeException(i_Maximum, i_Minimum));
            }
            else
            {
                isValidNumberInput = true;
            }

            return isValidNumberInput;
        }

        public static bool IsValidFloatNumber(string i_UserStringInput, float i_Maximum, bool i_NeedToCheckMax, out float o_Choice)
        {
            bool isValidNumberInput = false;

            if (!float.TryParse(i_UserStringInput, out o_Choice))
            {
                throw (new FormatException("Wrong input! You need to insert a number"));
            }
            else if (o_Choice < 0)
            {
                throw (new ArgumentException("Wrong input! You need to insert positive number"));
            }
            else if(i_NeedToCheckMax && o_Choice > i_Maximum)
            {
                throw (new ArgumentException("Wrong input! You need to insert a smaller number"));
            }
            else
            {
                isValidNumberInput = true;
            }

            return isValidNumberInput;
        }

        public static bool IsAllLettersOrDigits(string i_UserStringInput)
        {
            bool isValidInput = false;

            if (!i_UserStringInput.All(char.IsLetterOrDigit))
            {
                throw (new FormatException("Wrong input! You need to insert string that contains only letters and digits "));
            }

            isValidInput = true;

            return isValidInput;
        }

        public static bool IsAllLetters(string i_UserStringInput)
        {
            bool isValidInput = false;

            if (!i_UserStringInput.All(char.IsLetter))
            {
                throw (new FormatException("Wrong input! You need to insert string that contains only letters"));
            }

            isValidInput = true;

            return isValidInput;
        }

        public static bool IsAllDigits(string i_UserStringInput)
        {
            bool isValidInput = false;

            if(!i_UserStringInput.All(char.IsDigit))
            {
                throw (new FormatException("Wrong input! You need to insert string that contains only digits"));
            }

            isValidInput = true;

            return isValidInput;
        }
    }
}
