using APIProject.Models;
using Microsoft.EntityFrameworkCore;

namespace APIProject.Data
{
    public class CalendarContx : DbContext
    {

        public CalendarContx(DbContextOptions<CalendarContx> options) : base(options)
        {

        }
       
        public DbSet<GoogleCalendar> googleCalendars { get; set; }  
    }
}
