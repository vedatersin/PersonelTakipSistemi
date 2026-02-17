using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PersonelTakipSistemi.Models
{
    public class GorevDurumGecmisi
    {
        [Key]
        public int Id { get; set; }

        public int GorevId { get; set; }
        [ForeignKey("GorevId")]
        public virtual Gorev Gorev { get; set; }

        public int GorevDurumId { get; set; }
        [ForeignKey("GorevDurumId")]
        public virtual GorevDurum GorevDurum { get; set; }

        public string? Aciklama { get; set; }

        public DateTime Tarih { get; set; } = DateTime.Now;

        public int? IslemYapanPersonelId { get; set; }
        [ForeignKey("IslemYapanPersonelId")]
        public virtual Personel? IslemYapanPersonel { get; set; }
    }
}
