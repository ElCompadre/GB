using System.ComponentModel.DataAnnotations;

namespace GB.Domain.Models;

public class QuoteRequestModel
{
    [Required(ErrorMessage ="La commande ne peut pas être vide")]
    public ICollection<QuoteRequestItemModel>  Items { get; set; }
}

public class QuoteRequestItemModel
{
    public required int BiereId { get; set; }
    public required int Quantite { get; set; }
}