using System;

namespace MedCenter.V1
{
    public class Patient : Person
    {
        public override UserKind Kind 
        {
            get { return UserKind.Patient; }
        }
    }
}