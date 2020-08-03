namespace MedCenter
{
    public class Doctor : Person
    {
        public override UserKind Kind
        {
            get { return UserKind.Doctor; }
        }
    }
}