using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GB.Domain.Models;

public class Biere
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [Required] [MaxLength(100)] public string Nom { get; set; } = string.Empty;

    public decimal DegresAlcool { get; set; }

    public decimal Prix { get; set; }

    public int BrasserieId { get; set; }

    public virtual Brasserie Brasserie { get; set; } = null!;
}