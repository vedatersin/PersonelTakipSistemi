using System.ComponentModel.DataAnnotations;

namespace PersonelTakipSistemi.Models
{
    public class DaireBaskanligi
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(200)]
        public string Ad { get; set; }

        public bool IsActive { get; set; } = true;
        
        public ICollection<Teskilat> Teskilatlar { get; set; } = new List<Teskilat>();
    }
}
