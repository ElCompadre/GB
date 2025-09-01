namespace GB.Domain.Models;

public class GrossisteBrasserie
{
    public int GrossisteId { get; set; }
    public int BrasserieId { get; set; }
    public int Quantite { get; set; }
    
    public virtual Grossiste Grossiste { get; set; } = null!;
    public virtual Brasserie Brasserie { get; set; } = null!;
}