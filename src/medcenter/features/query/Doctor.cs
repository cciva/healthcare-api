using MediatR;

namespace MedCenter
{
    public class DoctorById : IRequest<Doctor>
    {
        public string Id { get; set; }
    }
}