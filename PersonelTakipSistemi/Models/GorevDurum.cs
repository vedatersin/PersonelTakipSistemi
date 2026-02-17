using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PersonelTakipSistemi.Models
{
    public class GorevDurum
    {
        [Key]
        public int GorevDurumId { get; set; }

        [Required]
        [StringLength(100)]
        public string Ad { get; set; } = null!;

        public int Sira { get; set; }

        // Bootstrap badge class (e.g., bg-warning, bg-primary)
        [StringLength(50)]
        public string? RenkSinifi { get; set; } 

        [StringLength(7)]
        public string? Renk { get; set; } 

        public ICollection<Gorev> Gorevler { get; set; } = new List<Gorev>();
    }
}
