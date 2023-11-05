using System.ComponentModel.DataAnnotations;

namespace APIProject.DTO
{
    public class GoogleCalendarDTO
    {   
        public string?  Id { get; set; }

        [Required]
        public string Summary { get; set; }

        public string Description { get; set; }

        public string Location { get; set; }

        [Required]
        public DateTime Start { get; set; }

        [Required]
        public DateTime End { get; set; }

       
        public string? Attachment { get; set; }

    }
}
