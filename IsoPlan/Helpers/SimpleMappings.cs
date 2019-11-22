using AutoMapper;
using IsoPlan.Data.DTOs;
using IsoPlan.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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

            // construction site mapping
            CreateMap<Job, JobDTO>();
            CreateMap<JobDTO, Job>();

        }
    }
}
