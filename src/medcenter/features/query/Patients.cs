using MediatR;

namespace MedCenter
{
    public class PatientById : IRequest<Patient>
    {
        public string Id { get; set; }
    }
}