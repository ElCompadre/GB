namespace GB.Domain.Models;

public class GrossisteBiereModel
{
    public int GrossisteId { get; init; }
    public int BiereId { get; init; }
    public int Quantite { get; init; }
    
    public GrossisteModel Grossiste { get; init; } = null!;
    public BiereModel Biere { get; init; } = null!;
}