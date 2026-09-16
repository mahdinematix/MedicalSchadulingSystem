namespace MS.Domain.Exceptions;

public class DateCannotInThePastException : Exception
{
    public DateCannotInThePastException():base("Selected date should not less than today!")
    {
    }
}
