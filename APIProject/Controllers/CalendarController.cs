using APIProject.Data;
using APIProject.DTO;
using APIProject.Helper;
using APIProject.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace APIProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CalendarController : ControllerBase
    {
        private readonly CalendarContx _context;
        public CalendarController(CalendarContx ctx)
        {
            _context = ctx;
        }

        [HttpPost]
        public async Task<IActionResult> CreateGoogleCalendar([FromBody] GoogleCalendarDTO request)
        {
            // Check if the event is on a Friday or Saturday
            if (request.Start.DayOfWeek == DayOfWeek.Friday || request.Start.DayOfWeek == DayOfWeek.Saturday)
            {
                return BadRequest("Events cannot be created on Friday or Saturday.");
            }

            // Check if the event's start time is in the past
            if (request.Start < DateTime.Now)
            {
                return BadRequest("Events cannot be created in the past.");
            }

            // Additional input validation
            if (string.IsNullOrWhiteSpace(request.Summary) || string.IsNullOrWhiteSpace(request.Location))
            {
                return BadRequest("Summary and Location are required fields.");
            }

            // Use Google Calendar Helper to create the event
            var result = await new GoogleCalendarHelper().CreateGoogleCalendar(request);
            var googleCalendar = new GoogleCalendar
            {   eventID = result.Id,
                Summary = result.Summary,
                Description = result.Description,
                Location = result.Location,
                Start = result.Start.DateTime ?? DateTime.MinValue,
                End = result.End.DateTime ?? DateTime.MinValue,
                Attachment = request.Attachment
            };
            _context.googleCalendars.Add(googleCalendar);
            await _context.SaveChangesAsync();

            return Created(string.Empty, result);
        }


        //let`s create get method
        [HttpGet]
        public async Task<IActionResult> GetGoogleCalendar(string eventId)
        {
            var result = await new GoogleCalendarHelper().GetGoogleCalendarEvent(eventId);
            if (result == null)
            {
                   return NotFound("there is no event with this id");
            }
            return Ok(result);
        }

        [HttpGet("get_list")]
        public IActionResult GetGoogleCalendars()
        {
            var result =  new GoogleCalendarHelper().GetGoogleCalendarEvents();
            if (result.Count == 0)
            {
             return NotFound("there is no event in this date range");
            }
            return Ok(result);
        }
        [HttpGet("get_list_by_date")]
        public IActionResult GetGoogleCalendarsByDate(DateTime start, DateTime end)
        {
            var result = new GoogleCalendarHelper().GetEventsatDateRange(start, end);
            //var paggnation = result.Skip(0).Take(3);
            //return Ok(paggnation);
            if (result.Count == 0) {
             return NotFound("there is no event in this date range");
            }
            return Ok(result);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteGoogleCalendar(string eventId)
        {
            var result = await new GoogleCalendarHelper().DeleteEvent(eventId);
            if (result )
            {
                return Ok("Event deleted succesfully");
            }       
            return BadRequest("it`s wrong");
        }

        [HttpGet("search_By_Crtiria")]
        public IActionResult search_by_anyc(string search)
        {
            var result = new GoogleCalendarHelper().SearchEvent(search);
            if (result.Count == 0)
            {
                return NotFound("there is no event with this criteria");
            }
            return Ok(result);
        }   
         


    }
}
