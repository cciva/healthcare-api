using System.Collections.Generic;
using MediatR;

namespace MedCenter
{
    public class TherapyById : IRequest<Therapy>
    {
        public string Id { get; set; }
    }

    public interface ITherapiesByPatient : IRequest<IEnumerable<Therapy>>
    {
        string PatientId { get; set; }
    }

    public interface ITherapiesByDoctor : IRequest<IEnumerable<Therapy>>
    {
        string DoctorId { get; set; }
    }

    public class TherapiesQuery : ITherapiesByPatient, 
                                  ITherapiesByDoctor, 
                                  IRequest<IEnumerable<Therapy>>
    {
        public string DoctorId { get; set; }
        public string PatientId { get; set; }
    }
}