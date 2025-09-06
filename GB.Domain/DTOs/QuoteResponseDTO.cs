namespace GB.Domain.Models;

public class QuoteResponseDTO
{
    public string Nom { get; init; }
    public decimal PrixTotal { get; set; }
    public ICollection<QuoteItemResponseDTO> Items { get; init; } =  new List<QuoteItemResponseDTO>();
}

public class QuoteItemResponseDTO
{
    public string Nom { get; init; }
    public decimal Prix { get; init; }
    public int Quantite { get; init; }
}