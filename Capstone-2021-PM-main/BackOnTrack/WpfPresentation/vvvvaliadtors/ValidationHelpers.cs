using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace WpfPresentation
{
    public static class ValidationHelpers
    {
        /// <summary>
        /// Chantal Shirley
        /// Created 2021/02/04
        /// 
        /// Receives an array of strings and determines if any strings
        /// are empty strings or only white space.
        /// </summary>
        /// <param name="arrayOfStrings"></param>
        /// <returns></returns>
        public static bool containsEmptyString(this string[] arrayOfStrings)
        {
            bool result = false;

            foreach (string s in arrayOfStrings)
            {
                if (String.IsNullOrEmpty(s) || String.IsNullOrWhiteSpace(s))
                {
                    return true;
                }
            }

            return result;
        }

        /// <summary>
        /// Chantal Shirley
        /// Created 2021/02/04
        /// 
        /// Returns true if the date is null, today, or in the past.
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public static bool dateTodayOrInPast(this DatePicker date)
        {
            bool result = false;

            if (date.SelectedDate.Equals(null)) // Prevents null object error from being thrown
            {
                return true;
            }

            DateTime selectedDate = (DateTime)date.SelectedDate;
            TimeSpan timeSpan = new TimeSpan(00); // Represents today's date 

            if (selectedDate.Subtract(DateTime.Now) < timeSpan) {
                return true;
            }

            return result;
        }

        /// <summary>
        /// Created 2021/02/04
        /// 
        /// Returns true if the date is null or after today.
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public static bool dateInFuture(this DatePicker date)
        {
            bool result = false;

            if (date.SelectedDate.Equals(null)) // Prevents null object error from being thrown
            {
                return true;
            }

            DateTime selectedDate = (DateTime)date.SelectedDate;
            TimeSpan timeSpan = new TimeSpan(1); // Represents tomorrow's date 

            if (selectedDate.Subtract(DateTime.Now) > timeSpan)
            {
                return true;
            }

            return result;
        }

        /// <summary>
        /// Chantal Shirley
        /// Created 2021/02/05
        /// 
        /// Returns true if string passed is convertible to an Int64.
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static bool isAnInteger(this string s)
        {
            bool result = false;

            if (String.IsNullOrEmpty(s) || String.IsNullOrWhiteSpace(s))
            {
                return result;
            }

            Int32 num;

            if (Int32.TryParse(s, out num))
            {
                return true;
            }

            return result;
        }
    }
}
