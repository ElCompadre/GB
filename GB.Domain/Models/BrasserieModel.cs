namespace GB.Domain.Models;

public class BrasserieModel
{
    public int Id { get; init; }

    public string Nom { get; init; } = string.Empty;

    public ICollection<BiereModel> Bieres { get; init; } = new List<BiereModel>();
    public ICollection<GrossisteBiereModel> GrossisteBrasseries { get; init; } = new List<GrossisteBiereModel>();
}