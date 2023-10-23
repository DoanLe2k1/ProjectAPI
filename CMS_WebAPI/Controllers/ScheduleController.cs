using CMS_WebAPI.Service;
using Microsoft.AspNetCore.Mvc;

namespace CMS_WebAPI.Controllers
{
    [ApiController]
    [Route("api/schedule")]
    public class ScheduleController : Controller
    {
        private readonly IScheduleService _scheduleService;

        public ScheduleController(IScheduleService scheduleService)
        {
            _scheduleService = scheduleService;
        }

        [HttpPost]
        public IActionResult AddSubjectToSchedule(int studentId, int subjectId)
        {
            try
            {
                _scheduleService.AddSubjectToSchedule(studentId, subjectId);
                return Ok("Subject added to schedule successfully.");
            }
            catch (Exception ex)
            {
                // Log the exception for debugging purposes
                Console.WriteLine(ex.ToString());

                // Check if there is an inner exception and return its message
                var errorMessage = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
                return BadRequest(errorMessage);
            }
        }

        [HttpGet("{studentId}")]
        public IActionResult GetSchedule(int studentId)
        {
            var schedule = _scheduleService.GetSchedule(studentId);
            return Ok(schedule);
        }
    }
}
