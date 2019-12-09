using System.Collections.Generic;

namespace IsoPlan.Data.Entities
{
    public class ScheduleWeek
    {
        public string Name { get; set; }
        public List<Schedule> Date1 { get; set; }
        public List<Schedule> Date2 { get; set; }
        public List<Schedule> Date3 { get; set; }
        public List<Schedule> Date4 { get; set; }
        public List<Schedule> Date5 { get; set; }
        public List<Schedule> Date6 { get; set; }
    }
}
