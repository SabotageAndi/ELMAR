using System;

namespace net.the_engineers.elmar.everywhere
{
    //[JsonConverter(typeof(TimeJsonConverter))]
    public class Time
    {
        public Time()
        {
        }

        public Time(string value)
        {
            var splitted = value.Split(':');

            Hours = Convert.ToInt32(splitted[0]);
            Minutes = Convert.ToInt32(splitted[1]);

            if (splitted.Length == 3)
                Seconds = Convert.ToInt32(splitted[2]);
        }

        public Time(int secondsTillMidnight)
        {
            Hours = secondsTillMidnight/(60*60);
            Minutes = (secondsTillMidnight - (Hours*60*60))/60;
            Seconds = (secondsTillMidnight - (Hours*60*60) - Minutes*60);
        }

        public Time(int hour, int minute, int second)
        {
            Hours = hour;
            Minutes = minute;
            Seconds = second;
        }

        public static Time MinValue
        {
            get { return new Time(0,0,0);}
        }

        public int Hours { get; set; }
        public int Minutes { get; set; }
        public int Seconds { get; set; }

        public int SecondsAfterMidnight
        {
            get { return Hours*3600 + Minutes*60 + Seconds; }
        }

        public override string ToString()
        {
            return String.Format("{0:00}:{1:00}:{2:00}", Hours, Minutes, Seconds);
        }

        public static Time Parse(string value)
        {
            return new Time(value);
        }

        public bool Equals(Time other)
        {
            return Hours == other.Hours && Minutes == other.Minutes && Seconds == other.Seconds;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            return obj is Time && Equals((Time) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int hashCode = Hours;
                hashCode = (hashCode*397) ^ Minutes;
                hashCode = (hashCode*397) ^ Seconds;
                return hashCode;
            }
        }

        public static bool operator ==(Time left, Time right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(Time left, Time right)
        {
            return !left.Equals(right);
        }

        public static bool operator <(Time left, Time right)
        {
            return left.SecondsAfterMidnight < right.SecondsAfterMidnight;
        }

        public static bool operator >(Time left, Time right)
        {
            return left.SecondsAfterMidnight > right.SecondsAfterMidnight;
        }

        public static bool operator <=(Time left, Time right)
        {
            return left.SecondsAfterMidnight <= right.SecondsAfterMidnight;
        }

        public static bool operator >=(Time left, Time right)
        {
            return left.SecondsAfterMidnight >= right.SecondsAfterMidnight;
        }

        public DateTime SetTimeOfDateTime(DateTime dateTime)
        {
            return new DateTime(dateTime.Year, dateTime.Month, dateTime.Day, Hours, Minutes, Seconds);
        }

        public DateTimeOffset SetTimeOfDateTime(DateTimeOffset dateTime)
        {
            return new DateTimeOffset(dateTime.Year, dateTime.Month, dateTime.Day, Hours, Minutes, Seconds, dateTime.Offset);
        }
    }
}