using System;
using System.ComponentModel.DataAnnotations;
using System.Net.Mail;
using System.Text.Json.Serialization;

namespace APIProject.Models
{
    public class GoogleCalendar
    {
        //create calendar id  make eit int and make it auto increment
        [Key]
        public int id { get; set; }

        public string?  eventID { get; set; }
        
     

        [Required]
        public string Summary { get; set; }

        public string Description { get; set; }

        public string Location { get; set; }

        [Required]
        public DateTime Start { get; set; }

        [Required]
        public DateTime End { get; set; }

      
        public string? Attachment  { get; set; }

    }

}
