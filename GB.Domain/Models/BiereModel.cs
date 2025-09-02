namespace GB.Domain.Models;

public class BiereModel
{
    public int Id { get; init; }

    public string Nom { get; init; } = string.Empty;

    public decimal DegresAlcool { get; init; }

    public decimal Prix { get; init; }

    public int BrasserieId { get; init; }

    public BrasserieModel Brasserie { get; init; } = null!;
    public ICollection<GrossisteBiereModel> GrossisteBieres { get; init; } = new List<GrossisteBiereModel>();
}