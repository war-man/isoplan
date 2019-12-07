using AutoMapper;
using IsoPlan.Data.DTOs;
using IsoPlan.Data.Entities;

namespace IsoPlan.Helpers
{
    public class SimpleMappings : Profile
    {
        public SimpleMappings()
        {
            // user mapping
            CreateMap<User, UserDto>();
            CreateMap<UserDto, User>();

            // employee mapping
            CreateMap<Employee, EmployeeDTO>();
            CreateMap<EmployeeDTO, Employee>().ForMember(e => e.Files, opt => opt.Ignore());

            // job mapping
            CreateMap<Job, JobDTO>();
            CreateMap<JobDTO, Job>();

            // job item mapping
            CreateMap<JobItem, JobItemDTO>();
            CreateMap<JobItemDTO, JobItem>();

            // schedule week mapping
            CreateMap<ScheduleWeek, ScheduleWeekDTO>();
            CreateMap<ScheduleWeekDTO, ScheduleWeek>();

            // schedule mapping
            CreateMap<Schedule, ScheduleDTO>()
                .ForMember(dest => dest.EmployeeName, 
                           opts => opts.MapFrom(src => src.Employee.FirstName + " " + src.Employee.LastName));
            CreateMap<ScheduleDTO, Schedule>();
        }
    }
}
