namespace BackendPrueba.Models.DTO {
    public class ResponseDTO {
        public bool success { get; set; }
        public object? result { get; set; }
        public string? displayMessage { get; set; }
        public List<string>? ErrorMessage { get; set; }
    }
}
