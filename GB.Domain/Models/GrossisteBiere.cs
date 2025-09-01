namespace GB.Domain.Models;

public class GrossisteBiere
{
    public int GrossisteId { get; set; }
    public int BiereId { get; set; }
    public int Quantite { get; set; }
    
    public virtual Grossiste Grossiste { get; set; } = null!;
    public virtual Biere Biere { get; set; } = null!;
}