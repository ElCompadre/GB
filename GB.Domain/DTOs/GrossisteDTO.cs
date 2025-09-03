namespace GB.Domain.Models;

public class GrossisteDTO
{
    public int? Id { get; init; }

    public string Nom { get; init; } = string.Empty;
    public virtual ICollection<GrossisteBiereDTO>? GrossisteBieres { get; init; } = new List<GrossisteBiereDTO>();
}