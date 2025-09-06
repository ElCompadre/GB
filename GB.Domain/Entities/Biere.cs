using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GB.Domain.Entities;

public class Biere
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; init; }

    [Required] [MaxLength(100)] public string Nom { get; init; } = string.Empty;

    [Column(TypeName ="decimal(10,2)")]
    public decimal DegresAlcool { get; init; }

    [Column(TypeName ="decimal(10,2)")]
    public decimal Prix { get; init; }

    public int BrasserieId { get; init; }

    public virtual Brasserie Brasserie { get; init; } = null!;
    public virtual ICollection<GrossisteBiere> GrossisteBieres { get; init; } = new List<GrossisteBiere>();
}