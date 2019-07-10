using System;

namespace TekoTest
{
    class Vacation
    {
        public int UserId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        private static Random _random;

        static Vacation()
        {
            _random = new Random();
        }

        /// <summary>
        /// Init random vacation period
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="year"></param>
        /// <param name="duration"></param>
        public Vacation(int userId, int year, int duration)
        {
            UserId = userId;

            int yearDays = 365;
            int validVacationStart = yearDays - duration;
            StartDate = (new DateTime(year, 01, 01)).AddDays(_random.Next(validVacationStart));
            EndDate = StartDate.AddDays(duration);
        }

        public bool HasIntersection(Vacation vacation)
        {
            return StartDate <= vacation.StartDate && vacation.StartDate <= EndDate
                   || StartDate <= vacation.EndDate && vacation.EndDate <= EndDate;
        }

        /// <summary>
        /// Get nonintersect random vacation
        /// </summary>
        /// <param name="year"></param>
        /// <param name="duration"></param>
        /// <returns></returns>
        public Vacation GetNonintersectVacation(int year, int duration)
        {
            Vacation vacation = new Vacation(UserId, year, duration);

            while (this.HasIntersection(vacation))
            {
                vacation = new Vacation(UserId, year, duration);
            }

            return vacation;
        }

        public string GetDates()
        {
            return $"{StartDate.ToShortDateString()}-{EndDate.ToShortDateString()}";
        }
    }
}
