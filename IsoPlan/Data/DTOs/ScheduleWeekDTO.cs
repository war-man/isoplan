using System.Collections.Generic;

namespace IsoPlan.Data.DTOs
{
    public class ScheduleWeekDTO
    {
        public string Name { get; set; }
        public List<ScheduleDTO> Date1 { get; set; }
        public List<ScheduleDTO> Date2 { get; set; }
        public List<ScheduleDTO> Date3 { get; set; }
        public List<ScheduleDTO> Date4 { get; set; }
        public List<ScheduleDTO> Date5 { get; set; }
        public List<ScheduleDTO> Date6 { get; set; }
    }
}
