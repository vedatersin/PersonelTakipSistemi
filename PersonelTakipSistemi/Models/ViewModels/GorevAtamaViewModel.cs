using System.Collections.Generic;

namespace PersonelTakipSistemi.Models.ViewModels
{
    public class GorevAtamaViewModel
    {
        public int GorevId { get; set; }
        
        // Incoming IDs
        public List<int> TeskilatIds { get; set; } = new List<int>();
        public List<int> KoordinatorlukIds { get; set; } = new List<int>();
        public List<int> KomisyonIds { get; set; } = new List<int>();
        public List<int> PersonelIds { get; set; } = new List<int>();
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
