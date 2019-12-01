using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IsoPlan.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace IsoPlan.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SchedulesController : ControllerBase
    {
        private readonly IScheduleService _scheduleService;

        public SchedulesController(IScheduleService scheduleService)
        {
            _scheduleService = scheduleService;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var toRet = _scheduleService.GetAll(DateTime.Now.Date, DateTime.Now.Date.AddDays(7));
            return Ok(toRet);
        }
    }
}