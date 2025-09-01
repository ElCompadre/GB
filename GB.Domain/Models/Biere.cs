using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GB.Domain.Models;

public class Biere
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [Required] [MaxLength(100)] public string Nom { get; set; } = string.Empty;

    [Column(TypeName ="decimal(10,3)")]
    public decimal DegresAlcool { get; set; }

    [Column(TypeName ="decimal(10,3)")]
    public decimal Prix { get; set; }

    public int BrasserieId { get; set; }

    public virtual Brasserie Brasserie { get; set; } = null!;
    public virtual ICollection<GrossisteBiere> GrossisteBieres { get; set; } = new List<GrossisteBiere>();
}