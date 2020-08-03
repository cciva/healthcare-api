using System;

namespace MedCenter
{
    public class Patient : Person
    {
        public override UserKind Kind 
        {
            get { return UserKind.Patient; }
        }
    }
}