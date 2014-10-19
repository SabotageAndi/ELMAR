using System;

namespace net.the_engineers.elmar.everywhere.Dto
{
    public class AlarmClockDto
    {
        public int Id { get; set; }
        public DateTimeOffset Date { get; set; }
        public Time Time { get; set; }
        public bool Overwritten { get; set; }

        public bool Disabled { get; set; }
    }
}