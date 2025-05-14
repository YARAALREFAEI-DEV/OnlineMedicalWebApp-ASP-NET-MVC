using System.ComponentModel.DataAnnotations;

namespace Next2OnlineMedicalApp.Models
{
    public class Doctor
    {
        [Key]
        public int DoctorId { get; set; }  

        [Required(ErrorMessage = "First Name is required.")]
        public string FirstName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Last Name is required.")]
        public string LastName { get; set; } = string.Empty;
        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Invalid Email Address.")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "Phone number is required.")]
        public string PhoneNumber { get; set; } = string.Empty;

        [Required(ErrorMessage = "Address is required.")]
        public string Address { get; set; }  =string.Empty;
        [DataType(DataType.Password)]
        public string Password { get; set; }=string.Empty;
        

        public Specialization specialization {  get; set; } 
		public enum Specialization
        {
            General,
            Neurologist,
            Dermatologist,
            Psychiatrist
        }

        public string FullNameWithSpecialization => $"{FirstName} {LastName} - {specialization}";

    }

}
