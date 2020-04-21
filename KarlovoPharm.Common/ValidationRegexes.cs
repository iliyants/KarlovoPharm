namespace KarlovoPharm.Common
{
    public class ValidationRegexes
    {
        public const string PhoneRegex = @"^\s* (?:\+?(\d{1,3}))?[-. (]* (\d{3})[-. )]* (\d{3})[-. ]* (\d{4})(?: * x(\d+))?\s*$";
    }
}
