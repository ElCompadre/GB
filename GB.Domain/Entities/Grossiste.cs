using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GB.Domain.Entities;

public class Grossiste
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; init; }

    [Required] 
    [MaxLength(100)]
    public string Nom { get; init; } = string.Empty;
    public virtual ICollection<GrossisteBiere> GrossisteBieres { get; init; } = new List<GrossisteBiere>();
}