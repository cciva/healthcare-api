using System;
using Contracts;
using MediatR;

namespace MedCenter
{
    public class BuyMedicinesCommand : IRequest<PharmacyInvoice>
    {
        public string TherapyId { get; set; }
    }
}