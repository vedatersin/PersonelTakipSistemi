using System.Collections.Generic;

namespace PersonelTakipSistemi.Models.ViewModels
{
    public class GorevAtamaItemViewModel
    {
        public int Id { get; set; }
        public int GorevTuruId { get; set; }
    }

    public class GorevAtamaViewModel
    {
        public int GorevId { get; set; }
         
        // Incoming Assignments with Role Info (Görev Rolü == GorevTuru)
        public List<GorevAtamaItemViewModel> Teskilatlar { get; set; } = new();
        public List<GorevAtamaItemViewModel> Koordinatorlukler { get; set; } = new();
        public List<GorevAtamaItemViewModel> Komisyonlar { get; set; } = new();
        public List<GorevAtamaItemViewModel> Personeller { get; set; } = new();
    }

    public class GorevAtamaResultViewModel
    {
        public int GorevId { get; set; }
        public int GorevDurumId { get; set; }
        public string? DurumAciklamasi { get; set; }

        public List<IdNamePair> Teskilatlar { get; set; } = new List<IdNamePair>();
        public List<IdNamePair> Koordinatorlukler { get; set; } = new List<IdNamePair>();
        public List<IdNamePair> Komisyonlar { get; set; } = new List<IdNamePair>();
        public List<IdNamePair> Personeller { get; set; } = new List<IdNamePair>();
    }

    public class IdNamePair
    {
        public int Id { get; set; }
        public string Name { get; set; } = "";
        public string? Type { get; set; } // "Merkez", "Taşra" etc for badges

        // Optional: assignment role info (Görev Rolü)
        public int? GorevTuruId { get; set; }
        public string? GorevTuruAd { get; set; }
    }

    public class GorevDurumUpdateViewModel
    {
        public int GorevId { get; set; }
        public int DurumId { get; set; }
        public string? Aciklama { get; set; }
    }

    public class GorevCommandResult
    {
        public bool Success { get; set; }
        public int HttpStatusCode { get; set; } = 200;
        public string? Message { get; set; }

        public static GorevCommandResult SuccessResult(string? message = null) => new()
        {
            Success = true,
            Message = message
        };

        public static GorevCommandResult BadRequest(string message) => new()
        {
            Success = false,
            HttpStatusCode = 400,
            Message = message
        };

        public static GorevCommandResult NotFound(string? message = null) => new()
        {
            Success = false,
            HttpStatusCode = 404,
            Message = message
        };

        public static GorevCommandResult ApplicationFailure(string message) => new()
        {
            Success = false,
            HttpStatusCode = 200,
            Message = message
        };
    }

    public class GorevSearchEntityResult
    {
        public int Id { get; set; }
        public string Text { get; set; } = string.Empty;
        public bool Disabled { get; set; }
    }

    public class GorevStatusHistoryResult
    {
        public int Id { get; set; }
        public string Tarih { get; set; } = string.Empty;
        public string Personel { get; set; } = string.Empty;
        public string PersonelAvatar { get; set; } = string.Empty;
        public string Durum { get; set; } = string.Empty;
        public string DurumRenk { get; set; } = string.Empty;
        public string? Aciklama { get; set; }
    }
}
