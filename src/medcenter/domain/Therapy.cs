namespace MedCenter
{
    public class Therapy
    {
        public string TherapyId { get;set; }
        public string PatientId { get;set; }
        public string DoctorId { get;set; }
        public TimePeriod Effective { get;set; }
        public string[] Medications { get;set; }
    }
}