namespace MedCenter.V1
{
    public class Doctor : Person
    {
        public override UserKind Kind
        {
            get { return UserKind.Doctor; }
        }
    }
}