using GB.Domain.Models;

public class GrossisteBiereDTO
{
    public int GrossisteId { get; init; }
    public int BiereId { get; init; }
    public int? Stock { get; init; }
    
    public GrossisteDTO? Grossiste { get; init; }
    public BiereDTO? Biere { get; init; }
}