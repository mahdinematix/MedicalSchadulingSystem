namespace MS.Domain.Common;

public class BaseEntity<TKey>
{
    public TKey Id { get; private set; }
    //public DateTime CreationDate { get; private set; }
    //public DateTime LastModifiedDate { get; private set; }
}