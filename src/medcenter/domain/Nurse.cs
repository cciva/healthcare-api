using System;

namespace MedCenter
{
    public class Nurse : Person
    {
        public override UserKind Kind 
        {
            get { return UserKind.Nurse; }
        }
    }
}