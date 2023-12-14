using System;

namespace JqueryDataTableProject.Helper
{
    public class TimeZoneHelper
    {
        // Convert user input to UTC and store in the database
        public static DateTime SaveUserInput(DateTime userInput, string userTimeZoneId)
        {
            TimeZoneInfo userTimeZone = TimeZoneInfo.FindSystemTimeZoneById(userTimeZoneId);
            DateTime utcDateTime = TimeZoneInfo.ConvertTimeToUtc(userInput, userTimeZone);
            // Save utcDateTime to the database
            return utcDateTime;
        }

        // Retrieve UTC time from the database and convert to user's local time for display
        public static DateTime GetUserLocalTime(DateTime utcDateTime, string userTimeZoneId)
        {
            TimeZoneInfo userTimeZone = TimeZoneInfo.FindSystemTimeZoneById(userTimeZoneId);
            DateTime userLocalTime = TimeZoneInfo.ConvertTimeFromUtc(utcDateTime, userTimeZone);
            return userLocalTime;
        }
    }
}
