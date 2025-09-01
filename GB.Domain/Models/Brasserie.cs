using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GB.Domain.Models;

public class Brasserie
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [Required] [MaxLength(100)] public string Nom { get; set; } = string.Empty;

    public virtual ICollection<Biere> Bieres { get; set; } = new List<Biere>();
    public virtual ICollection<GrossisteBrasserie> GrossisteBrasseries { get; set; } = new List<GrossisteBrasserie>();
}