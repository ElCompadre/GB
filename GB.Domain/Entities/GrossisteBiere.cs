namespace GB.Domain.Entities;

public class GrossisteBiere
{
    public int GrossisteId { get; init; }
    public int BiereId { get; init; }
    public int? Stock { get; set; }
    
    public virtual Grossiste Grossiste { get; init; } = null!;
    public virtual Biere Biere { get; init; } = null!;
}