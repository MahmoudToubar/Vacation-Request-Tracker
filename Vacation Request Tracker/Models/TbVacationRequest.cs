using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Vacation_Request_Tracker.Helper;

namespace Vacation_Request_Tracker.Models
{
    public class TbVacationRequest
    {
        [Key]
        public Guid RequestId { get; set; }

        [Required]
        [RegularExpression(@"^[a-zA-Z\s]+$", ErrorMessage = "Only text characters are allowed.")]
        public string EmployeeName { get; set; }

        [Required]
        [RegularExpression(@"^[a-zA-Z\s]+$", ErrorMessage = "Only text characters are allowed.")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Please select a department")]
        public string Department { get; set; }

        public DateTime SubmissionDate { get; set; } = DateTime.Now; 

        [Required(ErrorMessage = "Vacation Date From is required")]
        [DataType(DataType.Date)]
        [FutureDate(ErrorMessage = "Vacation Date From must be in the future or today.")]
        public DateTime VacationDateFrom { get; set; }

        [Required(ErrorMessage = "Vacation Date To is required")]
        [DataType(DataType.Date)]
        [GreaterThan("VacationDateFrom", ErrorMessage = "Vacation Date To must be after Vacation Date From.")]
        public DateTime VacationDateTo { get; set; }

        [NotMapped]
        public DateTime ReturningDate => CalculateReturningDate();

        [NotMapped]
        public int TotalDaysRequested
        {
            get
            {
                int totalDays = 0;
                DateTime currentDate = VacationDateFrom;

                while (currentDate <= VacationDateTo)
                {
                    
                    if (currentDate.DayOfWeek != DayOfWeek.Friday && currentDate.DayOfWeek != DayOfWeek.Saturday)
                    {
                        totalDays++;
                    }

                    
                    currentDate = currentDate.AddDays(1);
                }

                return totalDays;
            }
        }

        [MaxLength(300)]
        public string Notes { get; set; }


        private DateTime CalculateReturningDate()
        {
            
            var nextWorkingDay = VacationDateTo.AddDays(1);
            while (nextWorkingDay.DayOfWeek == DayOfWeek.Friday || nextWorkingDay.DayOfWeek == DayOfWeek.Saturday)
            {
                nextWorkingDay = nextWorkingDay.AddDays(1);
            }
            return nextWorkingDay;
        }
    }
}
