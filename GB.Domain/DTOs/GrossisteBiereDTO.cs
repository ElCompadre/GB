using GB.Domain.Models;

public class GrossisteBiereDTO
{
    public int GrossisteId { get; init; }
    public int BiereId { get; init; }
    public int Quantite { get; init; }
    
    public GrossisteDTO Grossiste { get; init; } = null!;
    public BiereDTO Biere { get; init; } = null!;
}