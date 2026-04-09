using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PersonelTakipSistemi.Models
{
    public class GorevAtamaKomisyon
    {
        public int GorevId { get; set; }
        public Gorev Gorev { get; set; } = null!;

        public int KomisyonId { get; set; }
        public Komisyon Komisyon { get; set; } = null!;

        public int GorevTuruId { get; set; } // Görev Rolü
        [ForeignKey("GorevTuruId")]
        public GorevTuru? GorevTuru { get; set; }
    }
}
