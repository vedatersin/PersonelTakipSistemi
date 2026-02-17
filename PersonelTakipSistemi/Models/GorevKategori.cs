using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PersonelTakipSistemi.Models
{
    public class GorevKategori
    {
        [Key]
        public int GorevKategoriId { get; set; }

        [Required]
        [StringLength(150)]
        public string Ad { get; set; } = null!;

        [StringLength(500)]
        public string? Aciklama { get; set; }

        [StringLength(7)]
        public string? Renk { get; set; }

        public bool IsActive { get; set; } = true;

        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime? UpdatedAt { get; set; }

        public ICollection<Gorev> Gorevler { get; set; } = new List<Gorev>();
    }
}
