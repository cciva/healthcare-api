using System;

namespace MedCenter
{
    public abstract class Person
    {
        public string Id { get;set; }
        public string FirstName { get;set; }
        public string LastName { get;set; }
        public DateTime DateOfBirth { get;set; }
        public abstract UserKind Kind { get; }
    }
}