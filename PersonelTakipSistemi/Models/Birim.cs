using System.ComponentModel.DataAnnotations;

namespace PersonelTakipSistemi.Models
{
    public class Birim
    {
        [Key]
        public int BirimId { get; set; }

        [Required]
        [StringLength(150)]
        public string Ad { get; set; } = null!;
        
        // Navigation collection if needed later, but keeping minimal as requested
        public ICollection<Gorev> Gorevler { get; set; } = new List<Gorev>();
    }
}
