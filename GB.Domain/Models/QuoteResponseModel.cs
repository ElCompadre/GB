namespace GB.Domain.Models;

public class QuoteResponseModel
{
    public string Nom { get; set; }
    public decimal TotalHTVA { get; set; }
    public ICollection<QuoteItemResponseModel> Items { get; set; } = new List<QuoteItemResponseModel>();
}

public class QuoteItemResponseModel
{
    public string Nom { get; set; }
    public decimal Prix { get; set; }
    public int Quantite { get; set; }
}