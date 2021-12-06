using System;
using System.Collections.Generic;
using System.Text;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Net.Mail;

namespace IO.Swagger.Misc
{
    public static class TypeValidator
    {
        public static bool IsValidValue(string TypeName, string value)
        {
            switch (TypeName)
            {
                case "Integer":
                    return ValidateInteger(value);

                case "Real":
                    return ValidateReal(value);
                
                case "Char":
                    return ValidateChar(value);

                case "String":
                    return ValidateString(value);

                case "Email":
                    return ValidateEmail(value);

                case "Enum":
                    return ValidateEnum(value);

                default:
                    return false;
            }
        }

        private static bool ValidateChar(string value)
        {
            char buff;
            if (char.TryParse(value, out buff))
            {
                return true;
            }
            return false;
        }

        private static bool ValidateInteger(string value)
        {
            int buff;
            if (int.TryParse(value, out buff))
            {
                return true;
            }
            return false;
        }

        private static bool ValidateReal(string value)
        {
            double buff;
            if (double.TryParse(value, out buff))
            {
                return true;
            }
            return false;
        }

        private static bool ValidateString(string value)
        {
            return true;
        }

        private static bool ValidateEnum(string value)
        {
            Regex regex = new Regex(@"\s*(\s*\w* *(= *\d*)?\s*,?)*?\s*");
            MatchCollection matches = regex.Matches(value);
            if (matches.Count > 0)
            {
                return true;
            }
            return false;
        }

        private static bool ValidateEmail(string value)
        {
            try
            {
                MailAddress m = new MailAddress(value);
                return true;
            }
            catch (FormatException)
            {
                return false;
            }
        }
    }
}
