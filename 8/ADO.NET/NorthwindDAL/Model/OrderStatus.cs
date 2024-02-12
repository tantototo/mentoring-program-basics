namespace NorthwindDAL.Model;

public enum OrderStatus
{
    /// <summary>
    /// OrderDate == NULL
    /// </summary>
    New = 1,

    /// <summary>
    /// ShippedDate == NULL
    /// </summary>
    InProgress = 2,

    /// <summary>
    /// ShippedDate != NULL
    /// </summary>
    Resolve = 3,
}