using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PersonelTakipSistemi.Models
{
    public class GorevAtamaPersonel
    {
        public int GorevId { get; set; }
        public Gorev Gorev { get; set; } = null!;

        public int PersonelId { get; set; }
        public Personel Personel { get; set; } = null!;

        public int GorevTuruId { get; set; } // Görev Rolü
        [ForeignKey("GorevTuruId")]
        public GorevTuru? GorevTuru { get; set; }
    }
}
