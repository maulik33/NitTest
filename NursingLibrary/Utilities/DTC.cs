using System;
using System.Text;

namespace NursingLibrary.DTC
{
    public static class DTC
    {
        public static double ToDouble(this object valueToConvert)
        {
            double returnValue;
            if (valueToConvert != null
                && double.TryParse(valueToConvert.ToString(), out returnValue))
            {
                return returnValue;
            }
            else
            {
                return 0;
            }
        }

        public static short ToShort(this object valueToConvert)
        {
            short returnValue;
            if (valueToConvert != null
                && short.TryParse(valueToConvert.ToString(), out returnValue))
            {
                return returnValue;
            }
            else
            {
                return 0;
            }
        }

        public static short? ToNullableShort(this object valueToConvert)
        {
            short returnValue;
            if (valueToConvert == null)
            {
                return null;
            }

            if (short.TryParse(valueToConvert.ToString(), out returnValue))
            {
                return returnValue;
            }
            else
            {
                return null;
            }
        }

        public static string ToText(this object valueToConvert)
        {
            if (valueToConvert == null)
            {
                return string.Empty;
            }

            return valueToConvert.ToString();
        }

        public static int ToInt(this object valueToConvert)
        {
            int returnValue;
            if (valueToConvert != null
                && int.TryParse(valueToConvert.ToString(), out returnValue))
            {
                return returnValue;
            }
            else
            {
                return 0;
            }
        }

        public static long ToLong(this object valueToConvert)
        {
            long returnValue;
            if (valueToConvert != null
                && long.TryParse(valueToConvert.ToString(), out returnValue))
            {
                return returnValue;
            }
            else
            {
                return 0;
            }
        }

        public static DateTime ToDateTime(this object valueToConvert)
        {
            DateTime returnValue;
            if (valueToConvert != null
                && DateTime.TryParse(valueToConvert.ToString(), out returnValue))
            {
                return returnValue;
            }
            else
            {
                return DateTime.MinValue;
            }
        }

        public static bool ToBool(this object valueToConvert)
        {
            bool returnValue;
            if (valueToConvert != null
                && bool.TryParse(valueToConvert.ToString(), out returnValue))
            {
                return returnValue;
            }
            else
            {
                return false;
            }
        }

        public static byte ToByte(this object valueToConvert)
        {
            byte returnValue;
            if (valueToConvert != null
                && byte.TryParse(valueToConvert.ToString(), out returnValue))
            {
                return returnValue;
            }
            else
            {
                return 0;
            }
        }

        public static byte? ToNullableByte(this object valueToConvert)
        {
            byte returnValue;
            if (valueToConvert == null)
            {
                return null;
            }

            if (byte.TryParse(valueToConvert.ToString(), out returnValue))
            {
                return returnValue;
            }
            else
            {
                return null;
            }
        }

        public static string ToPercent(this object valueToConvert)
        {
            double returnValue;
            if (valueToConvert == null)
            {
                return "0";
            }
            
            if (Double.TryParse(valueToConvert.ToString(), out returnValue))
            {
                return Math.Round(returnValue, 2).ToString("0.00");
            }
            else
            {
                return "0";
            }
        }

        public static DateTime? ToNullableDateTime(this string valueToConvert)
        {
            if (valueToConvert == null)
            {
                return null;
            }

            DateTime returnValue;
            if (DateTime.TryParse(valueToConvert.ToString(), out returnValue))
            {
                return returnValue;
            }
            else
            {
                return null;
            }
        }

        public static string ToAlphaNumericString(this string stringToConvert)
        {
            StringBuilder alphaNumericString = new StringBuilder();

            Random rnd = new Random();
            foreach (char c in stringToConvert)
            {
                int asciiValue = (int)c;
                if ((asciiValue > 47 && asciiValue < 58)
                    || (asciiValue > 64 && asciiValue < 91)
                    || (asciiValue > 96 && asciiValue < 123))
                {
                    alphaNumericString.Append(c);
                }
                else
                {
                    alphaNumericString.Append(rnd.Next(10));
                }
            }

            return alphaNumericString.ToString();
        }

        public static string Format(this DateTime? dateToFormat)
        {
            return dateToFormat.HasValue ? dateToFormat.Value.ToString("MM/dd/yyyy") : string.Empty;
        }

        public static bool IsEqual(this string stringText, string stringToCompare)
        {
            return string.Compare(stringText, stringToCompare, true) == 0;
        }

        public static string ToAcronymProper(this string stringToConvert)
        {
            StringBuilder strb = new StringBuilder();
            for (int charIndex = 0; charIndex < stringToConvert.Length; charIndex++)
            {
                bool insertSpace = false;
                if (Char.IsUpper(stringToConvert[charIndex]) && charIndex > 0)
                {
                    // If Prev Char is Upper, dont insert space
                    if (false == Char.IsUpper(stringToConvert[charIndex - 1]))
                    {
                        insertSpace = true;
                    }
                    else
                    {
                        if (charIndex < stringToConvert.Length - 1)
                        {
                            // If Next Char is Upper, dont insert space
                            insertSpace = !Char.IsUpper(stringToConvert[charIndex + 1]);
                        }
                    }
                }
                
                if (insertSpace)
                {
                    strb.Append(" ");
                }

                strb.Append(stringToConvert[charIndex]);
            }
            
            return strb.ToString();
        }
    }
}
