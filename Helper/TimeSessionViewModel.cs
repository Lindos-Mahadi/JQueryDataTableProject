using System.ComponentModel.DataAnnotations;
using System;

namespace JqueryDataTableProject.Helper
{
    public class TimeSessionViewModel
    {
        [Required]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Start Time")]
        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd HH:mm}", ApplyFormatInEditMode = true)]
        public DateTime StartTime { get; set; }

        [Required]
        [Display(Name = "End Time")]
        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd HH:mm}", ApplyFormatInEditMode = true)]
        public DateTime EndTime { get; set; }

        //[Required]
        [Display(Name = "Time Zone")]
        public string UserTimeZoneId { get; set; }

        // Other properties as needed...
    }

}
