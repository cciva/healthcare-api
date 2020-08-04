namespace MedCenter
{
    public enum UserKind
    {
        Patient,
        Nurse,
        Doctor,
        Operator,
        Administrator
    }

    public enum DonationType
    {
        Blood,
        Organs,
        Other
    }

    // application environment:
    // - dev
    // - testing
    // - staging
    // - production
    public enum AppEnv
    {
        Dev,
        Testing,
        Staging,
        Production
    }
}