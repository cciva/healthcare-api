using System;

namespace MedCenter.V1
{
    public class Nurse : Person
    {
        public override UserKind Kind 
        {
            get { return UserKind.Nurse; }
        }
    }
}