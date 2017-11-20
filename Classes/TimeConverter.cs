using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BerlinClock.Enums;

namespace BerlinClock
{
    public class TimeConverter : ITimeConverter
    {
        private const int HourRowLength = 4;
        private const int TopMinutesRowLength = 11;
        private const int BottomMinutesRowLength = 4;
        private const char TimeSeparator = ':';

        public string convertTime(string aTime)
        {
            var timeParts = Validate(aTime);

            var result = string.Join(Environment.NewLine, GetClockRows(timeParts));

            return result;
        }

        private static int[] Validate(string time)
        {
            var rows = time.Split(new[] { TimeSeparator }, StringSplitOptions.RemoveEmptyEntries);
            if (rows.Length != 3)
            {
                rows = new[] {default(string), default(string), default(string)};
            }

            int hours;
            int minutes;
            int seconds;

            var hourCorrect = int.TryParse(rows[0], out hours);
            if (hourCorrect)
            {
                if (hours < 0 || hours > 24)
                {
                    hours = default(int);
                }
            }

            var minutesCorrect = int.TryParse(rows[1], out minutes);
            if (minutesCorrect)
            {
                if (minutes < 0 || minutes > 59)
                {
                    minutes = default(int);
                }
            }

            var secondsCorrect = int.TryParse(rows[2], out seconds);
            if (secondsCorrect)
            {
                if (seconds < 0 || seconds > 59)
                {
                    seconds = default(int);
                }
            }

            return new[] { hours, minutes, seconds };
        }

        private string[] GetClockRows(IReadOnlyList<int> timeParts)
        {
            var hours = timeParts[0];
            var minutes = timeParts[1];
            var seconds = timeParts[2];

            return new[] {
                GetSeconds(seconds),
                GetTopHours(hours),
                GetBottomHours(hours),
                GetTopMinutes(minutes),
                GetBottomMinutes(minutes)
            };
        }

        protected string GetSeconds(int number)
        {
            return number % 2 == 0
                ? LampState.Yellow
                : LampState.Off;
        }

        protected string GetTopHours(int number)
        {
            return GetRow(HourRowLength, GetTopRowNumberOfOnSigns(number), LampState.Red);
        }

        protected string GetBottomHours(int number)
        {
            return GetRow(HourRowLength, GetBottomRowNumberOfOnSigns(number), LampState.Red);
        }

        protected string GetTopMinutes(int number)
        {
            var lampOldState = LampState.Yellow + LampState.Yellow + LampState.Yellow;
            var lampNewState = LampState.Yellow + LampState.Yellow + LampState.Red;

            return GetRow(TopMinutesRowLength, GetTopRowNumberOfOnSigns(number), LampState.Yellow)
                        .Replace(lampOldState, lampNewState);
        }

        protected string GetBottomMinutes(int number)
        {
            return GetRow(BottomMinutesRowLength, GetBottomRowNumberOfOnSigns(number), LampState.Yellow);
        }

        private static int GetTopRowNumberOfOnSigns(int number)
        {
            return number / 5;
        }

        private static int GetBottomRowNumberOfOnSigns(int number)
        {
            return number % 5;
        }

        private static string GetRow(int allSignsNumber, int onSignsNumber, string onSign)
        {
            var result = new StringBuilder();

            AddSign(onSignsNumber, onSign, result);
            AddSign(allSignsNumber - onSignsNumber, LampState.Off, result);

            return result.ToString();
        }

        private static void AddSign(int signsNumber, string onSign, StringBuilder result)
        {
            for (var i = 0; i < signsNumber; i++)
            {
                result.Append(onSign);
            }
        }
    }
}
