using System;

namespace MedCenter
{
    public class ExamRq
    {
        public string DoctorId { get;set; }
        public DateTime TimeScheduled { get;set; }
        public Address At { get;set; }
    }

    public class ExamInfo : ExamRq
    {

    }
}