using System;

namespace TaskManager.Core.Models
{
    public class CronDate
    {
        public string Minutes { get; set; }
        public string Hours { get; set; }
        public string Days { get; set; }
        public string Months { get; set; }
        public string Weekdays { get; set; }

        public bool Matches(DateTime dateTime)
        {
            if (!MatchesMonth(dateTime)) return false;
            if (!MatchesDay(dateTime)) return false;
            if (!MatchesHour(dateTime)) return false;
            if (!MatchesMinute(dateTime)) return false;

            return true;
        }

        private bool MatchesMonth(DateTime dateTime)
        {
            return CronStringContainsValue(Months, dateTime.Month);
        }

        private bool MatchesHour(DateTime dateTime)
        {
            return CronStringContainsValue(Hours, dateTime.Hour);
        }

        private bool MatchesMinute(DateTime dateTime)
        {
            return CronStringContainsValue(Minutes, dateTime.Minute);
        }

        private bool MatchesDay(DateTime dateTime)
        {
            var dayCronValues = GetCronValuesAndRanges(Days);
            var weekdayCronValues = GetCronValuesAndRanges(Weekdays);

            if (dayCronValues.Contains("*"))
            {
                return CronStringContainsValue(Weekdays, (int)dateTime.DayOfWeek);
            } 
            else if (weekdayCronValues.Contains("*"))
            {
                return CronStringContainsValue(Days, dateTime.Day);
            }

            var dayNumberValue = dayCronValues.FirstOrDefault((dayCronValue) =>
            {
                int day = Convert.ToInt32(dayCronValue);
                if (day == dateTime.Day) return true;
                return false;
            });
            if (dayNumberValue is not null) return true;

            var weekdayNumberValue = weekdayCronValues.FirstOrDefault((weekdayCronValue) =>
            {
                int weekDay = Convert.ToInt32(weekdayCronValue);
                if (weekDay == (int)dateTime.DayOfWeek) return true;
                return false;
            });
            if (weekdayNumberValue is not null) return true;

            return false;
        }

        public bool CronStringContainsValue(string cronString, int value)
        {
            var cronStringValues = GetCronValuesAndRanges(cronString);

            if (cronStringValues.Contains("*")) return true;

            foreach (var cronStringValue in cronStringValues)
            {
                int convertedCronInt = ConvertToInt(cronStringValue);

                if (value == convertedCronInt) return true;
            }

            return false;
        }

        public int ConvertToInt(string value)
        {
            int convertedValue;
            try
            {
                convertedValue = int.Parse(value);
            }
            catch (FormatException ex)
            {
                throw new FormatException("Incorrect cron format", ex);
            }

            return convertedValue;
        }

        private string[] GetCronValuesAndRanges(string value)
        {
            return value.Split(',');
        }
    }
}
