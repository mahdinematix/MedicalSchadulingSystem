namespace MS.Domain.Exceptions;

public class WorkEndTimeCannotLessThanWorkStartTimeException : Exception
{
    public WorkEndTimeCannotLessThanWorkStartTimeException() : base("work end time should not be less than work start time!")
    {

    }
}
