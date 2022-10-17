using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace LaskutusProject
{
    internal class DateTimeCalculation
    {
        public static string Duedate(DateTime? _date, bool? _thirty)
        {
            //Error checking
            DateTime date = new DateTime();
            if (_date != null)
                date = (DateTime)_date;
            bool thirty = false;
            if (_thirty != null)
                thirty = (bool)_thirty;

            //Define variables
            DateTime dueDate = new DateTime();
            TimeSpan days = new TimeSpan();
            //check if thirty days or not
            if (thirty)  
                days = new TimeSpan(30, 0, 0, 0);
            else 
                days = new TimeSpan(14, 0, 0, 0);
            dueDate = date + days; //add amount to current date
            return dueDate.ToString("dd.MM.yy");//return as a string
        }

        internal static string Duedate(DateTime? selectedDate, object isChecked)
        {
            throw new NotImplementedException();
        }
    }
}
