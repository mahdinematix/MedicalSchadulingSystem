namespace MS.Domain.Exceptions;

public class TimeCannotInThePastException : Exception
{
    public TimeCannotInThePastException() : base("Selected time should not less than today!")
    {
    }
}
