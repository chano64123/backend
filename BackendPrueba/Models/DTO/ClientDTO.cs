namespace BackendPrueba.Models.DTO {
    public class ClientDTO {
        public int id { get; set; }
        public string? dni { get; set; }
        public string? nombres { get; set; }
        public string? apellidoPaterno { get; set; }
        public string? apellidoMaterno { get; set; }
        public string? telefono { get; set; }
        public string? correo { get; set; }
    }
}