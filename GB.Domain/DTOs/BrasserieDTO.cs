namespace GB.Domain.Models;

public class BrasserieDTO
{
    public int? Id { get; init; }

    public string Nom { get; init; } = string.Empty;

    public ICollection<BiereDTO>? Bieres { get; init; } = new List<BiereDTO>();
}