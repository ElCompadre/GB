using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GB.Domain.Models;

public class Grossiste
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [Required] 
    [MaxLength(100)]
    public string Nom { get; set; } = string.Empty;
    public virtual ICollection<GrossisteBiere> GrossisteBieres { get; set; } = new List<GrossisteBiere>();
}