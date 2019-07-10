using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace TekoTest
{

    class Program
    {
        public static string GetVacationsIntersectionReport(List<Vacation> myVacations, List<Vacation> depVacations)
        {
            StringBuilder result = new StringBuilder();

            foreach (var depVacation in depVacations)
            {
                result.Append($"ID {depVacation.UserId} {depVacation.GetDates()}");

                string intersectionDates = string.Empty;

                foreach (var myVacation in myVacations)
                {
                    if (depVacation.HasIntersection(myVacation))
                    {
                        if (!string.IsNullOrEmpty(intersectionDates))
                        {
                            intersectionDates = $"{intersectionDates}, ";
                        }

                        intersectionDates = $"{intersectionDates}{myVacation.GetDates()}";
                    }
                }

                if (!string.IsNullOrEmpty(intersectionDates))
                {
                    result.Append($" пересекается с {intersectionDates}");
                }

                result.Append("\r\n");
            }

            return result.ToString();
        }

        static void Main(string[] args)
        {
            List<Vacation> myVacations = new List<Vacation>();

            int vacationsYear = 2020;
            int vacationDuration = 2 * 7 - 1;

            myVacations.Add(new Vacation(1, vacationsYear, vacationDuration));

            myVacations.Add(myVacations.Last().GetNonintersectVacation(vacationsYear, vacationDuration)); 

            List<Vacation> depVacations = new List<Vacation>();

            for (int i = 0; i < 500; i++)
            {
                depVacations.Add(new Vacation(i + 2, vacationsYear, vacationDuration));
                depVacations.Add(depVacations.Last().GetNonintersectVacation(vacationsYear, vacationDuration));
            }

            File.WriteAllText("result.txt", GetVacationsIntersectionReport(myVacations, depVacations));

        }
    }
}
