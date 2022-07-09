using System.ComponentModel.DataAnnotations;

namespace BackendPrueba.Models {
    public class Client {
        [Key]
        public int id { get; set; }
        [Required]
        public string? dni { get; set; }
        [Required]
        public string? nombres { get; set; }
        [Required]
        public string? apellidoPaterno{ get; set; }
        [Required]
        public string? apellidoMaterno { get; set; }
        [Required]
        public string? telefono { get; set; }
        [Required]
        public string? correo { get; set; }
    }
}
