using System;
using MediatR;

namespace MedCenter
{
    public class CreateTherapyCommand : IRequest<Therapy>
    {
        public Therapy Therapy { get; set; }
    }
}