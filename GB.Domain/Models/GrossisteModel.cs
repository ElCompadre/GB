namespace GB.Domain.Models;

public class GrossisteModel
{
    public int Id { get; init; }

    public string Nom { get; init; } = string.Empty;
    
    public ICollection<GrossisteBiereModel> GrossisteBieres { get; init; } = new List<GrossisteBiereModel>();
}