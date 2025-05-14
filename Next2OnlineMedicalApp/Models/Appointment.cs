using System.ComponentModel.DataAnnotations;

namespace Next2OnlineMedicalApp.Models
{

    public class Appointment
    {
        [Key]
        public int AppointmentId { get; set; }  

        [Required(ErrorMessage = "PatientId is required.")]
        public int PatientId { get; set; }  

        [Required(ErrorMessage = "DoctorId is required.")]
        public int DoctorId { get; set; } 

        [Required(ErrorMessage = "Appointment Date is required.")]
        public DateTime AppointmentDate { get; set; }
        public Doctor? Doctor { get; set; }

        public AppointmentStatus Status { get; set; }   
		public enum AppointmentStatus
        {
            Pending,   
            Confirmed,  
            Completed,  
            Canceled   
        }



    }

}
