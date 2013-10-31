using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Text.RegularExpressions; 
/// <summary>
/// Summary description for DateDiff
/// </summary>
namespace NursingLibrary
{
    public class DateTimeManger
    {
        public enum DateInterval
        {
            Second, Minute, Hour, Day, Week, Month, Quarter, Year
        }


        public static long DateDiff(DateInterval Interval, System.DateTime StartDate, System.DateTime EndDate)
        {
            long lngDateDiffValue = 0;
            System.TimeSpan TS = new System.TimeSpan(EndDate.Ticks - StartDate.Ticks);
            switch (Interval)
            {
                case DateInterval.Day:
                    lngDateDiffValue = (long)TS.Days;
                    break;
                case DateInterval.Hour:
                    lngDateDiffValue = (long)TS.TotalHours;
                    break;
                case DateInterval.Minute:
                    lngDateDiffValue = (long)TS.TotalMinutes;
                    break;
                case DateInterval.Month:
                    lngDateDiffValue = (long)(TS.Days / 30);
                    break;
                case DateInterval.Quarter:
                    lngDateDiffValue = (long)((TS.Days / 30) / 3);
                    break;
                case DateInterval.Second:
                    lngDateDiffValue = (long)TS.TotalSeconds;
                    break;
                case DateInterval.Week:
                    lngDateDiffValue = (long)(TS.Days / 7);
                    break;
                case DateInterval.Year:
                    lngDateDiffValue = (long)(TS.Days / 365);
                    break;
            }
            return (lngDateDiffValue);
        }//end of DateDiff

        //zyh
        //public static string ConvertTo_MMDDYYYY_From_DDMMYYY(string inputdate)
        //{

        //    if (inputdate.Trim().Equals("")) return "";
        //    string[] arr = inputdate.Split(new char[] { '/' });
        //    string first;
        //    string output = "";
        //    if (arr != null)
        //    {
        //        if (arr[1].ToString().Length == 1)
        //        {
        //            first = "0" + arr[1].ToString();
        //        }
        //        else
        //        {
        //            first = arr[1].ToString();

        //        }
        //        string second;
        //        if (arr[0].ToString().Length == 1)
        //        {
        //            second = "0" + arr[0].ToString();
        //        }
        //        else
        //        {
        //            second = arr[0].ToString();

        //        }
        //        output = first.ToString() + "/" + second.ToString() + "/" + arr[2].ToString().Substring(0, 4);
        //    }
        //    return output;
        //}
        //zyh
        public static string CutTime(string inputdate)
        {
            if (inputdate.Trim().Equals("")) return "";
            string[] arr = inputdate.Split(new char[] { '/' });
            string first;
            if (arr[0].ToString().Length == 1)
            {
                first = "0" + arr[0].ToString();
            }
            else
            {
                first = arr[0].ToString();

            }
            string second;
            if (arr[1].ToString().Length == 1)
            {
                second = "0" + arr[1].ToString();
            }
            else
            {
                second = arr[1].ToString();

            }
            string output = first.ToString() + "/" + second.ToString() + "/" + arr[2].ToString().Substring(0, 4);

            return output;

            ////zyh
            //if (inputdate.Trim().Equals("")) return "";
            //DateTime d;
            //try
            //{
            //    d = System.Convert.ToDateTime(inputdate);
            //    return d.Date.ToShortDateString ();
            //}
            //catch (Exception ex)
            //{
            //    return "";
            //}
            ////zyh
        }
        public static int ValidateDate(string input)
        {
            //if (input.Trim().Equals("")) return 1;

            //string p5 = @"(\d+)/(\d+)/(\d+)";
            //Match m5 = Regex.Match(input, p5);

            //if (m5.Success)
            //{
            //    string[] arr = input.Split(new char[] { '/' });
            //    int month;
            //    int day;
            //    int year;
            //    day = Convert.ToInt16(arr[1]);
            //    month = Convert.ToInt16(arr[0]);

            //    if (arr[2].ToString().Length >= 4)
            //    {
            //        year = Convert.ToInt16(arr[2].Substring(0, 4));
            //    }
            //    else 
            //    {
            //        return 0;
            //    }

            //    if (day == 31 && (month == 4 || month == 6 || month == 9 || month == 11))
            //    {
            //        return 0; // 31st of a month with 30 days
            //    }
            //    if (day >= 30 && month == 2)
            //    {
            //        return 0; // February 30th or 31st
            //    }
            //    if (month == 2 && day == 29 && !(year % 4 == 0 && (year % 100 != 0 || year % 400 == 0)))
            //    {
            //        return 0; //# February 29th outside a leap year
            //    }
            //    if (month > 12)
            //    {
            //        return 0;
            //    }
            //    if (day > 31)
            //    {
            //        return 0;
            //    }
            //    if (year.ToString().Length < 4)
            //    {
            //        return 0;
            //    }
            //    return 1; // Valid date

            //}
            //else
            //{
            //    return 0;
            //}

            //zyh
            DateTime d;
            try
            {
                if (input == "") { return 1; }
                d = System.Convert.ToDateTime(input);
                return 1;
            }
            catch
            {
                return 0;
            }
            //zyh

        }
        public static string TranslateHours(string hour)
        {
            string returnValue = hour;
            switch (hour)
            {
                case "1":
                case "13":
                    returnValue = "1";
                    break;
                case "2":
                case "14":
                    returnValue = "2";
                    break;
                case "3":
                case "15":
                    returnValue = "3";
                    break;
                case "4":
                case "16":
                    returnValue = "4";
                    break;
                case "5":
                case "17":
                    returnValue = "5";
                    break;
                case "6":
                case "18":
                    returnValue = "6";
                    break;
                case "7":
                case "19":
                    returnValue = "7";
                    break;
                case "8":
                case "20":
                    returnValue =  "8";
                    break;
                case "9":
                case "21":
                    returnValue = "9";
                    break;
                case "10":
                case "22":
                    returnValue = "10";
                    break;
                case "11":
                case "23":
                    returnValue = "11";
                    break;
                case "12":
                case "24":
                    returnValue = "12";
                    break;
                default:
                    returnValue = hour;
                    break;
            }
            return returnValue;
        }

        public static bool CompareDates(string FirstDate, string SecondDate)
        {
            DateTime datestart = Convert.ToDateTime("01/01/2000");
            DateTime dateend = Convert.ToDateTime("01/01/2000");
            bool isValid2 = false;
            bool isValid1 = false;
            bool results = false;

            isValid1 = DateTime.TryParse(FirstDate, out datestart);
            if (isValid1)
            {
                isValid2 = DateTime.TryParse(SecondDate, out dateend);
                if (isValid2)
                {
                    int first = dateend.CompareTo((datestart));

                    if (first > 0)
                    {
                        results = true;
                    }
                }
            }
            return results;
        }
    }
}