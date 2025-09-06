namespace GB.Domain.Models;

public class QuoteRequestDTO
{
    public int GrossisteId { get; set; }
    public ICollection<QuoteRequestItemDTO> Items { get; init; }
}

public class QuoteRequestItemDTO
{
    public int BiereId { get; init; }
    public int Quantite { get; init; }
}