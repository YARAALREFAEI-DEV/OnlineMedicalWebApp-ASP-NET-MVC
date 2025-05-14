using System.ComponentModel.DataAnnotations;

namespace Next2OnlineMedicalApp.Models
{
    public class MedicalRecord
    {
        [Key]
        public int RecordId { get; set; }  

        [Required(ErrorMessage = "PatientId is required.")]
        public int PatientId { get; set; }  
        [Required(ErrorMessage = "DoctorId is required.")]
        public int DoctorId { get; set; }  

        [Required(ErrorMessage = "Visit Date is required.")]
        public DateTime VisitDate { get; set; }  


        [Required(ErrorMessage = "Diagnosis is required.")]
        [DataType(DataType.MultilineText)]
        public string Diagnosis { get; set; }  =string.Empty;

        [Required(ErrorMessage = "Prescription is required.")]
        [DataType(DataType.MultilineText)]
        public string Prescription { get; set; } = string.Empty;

       
    }

}
