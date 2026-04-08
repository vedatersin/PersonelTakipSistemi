namespace PersonelTakipSistemi.Services
{
    public class PersonelAssignmentResult
    {
        public bool Success { get; set; }
        public int HttpStatusCode { get; set; } = 200;
        public string? Message { get; set; }
        public string? Warning { get; set; }

        public static PersonelAssignmentResult SuccessResult(string? message = null) => new()
        {
            Success = true,
            Message = message
        };

        public static PersonelAssignmentResult BadRequest(string message) => new()
        {
            Success = false,
            HttpStatusCode = 400,
            Message = message
        };

        public static PersonelAssignmentResult WarningResult(string warning) => new()
        {
            Success = false,
            Warning = warning
        };
    }
}
