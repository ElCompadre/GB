namespace GB.Domain.Models;

public class BiereDTO
{
    public int? Id { get; init; }

    public string Nom { get; init; } = string.Empty;

    public decimal DegresAlcool { get; init; }

    public decimal Prix { get; init; }

    public int? BrasserieId { get; init; }
    public BrasserieDTO? Brasserie { get; init; }
    public ICollection<GrossisteBiereDTO>? GrossisteBieres { get; init; } = new List<GrossisteBiereDTO>();
}