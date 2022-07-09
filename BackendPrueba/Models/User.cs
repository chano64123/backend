using System.ComponentModel.DataAnnotations;

namespace BackendPrueba.Models {
    public class User {
        [Key]
        public int id { get; set; }
        [Required]
        public string? userName { get; set; }
        [Required]
        public byte[]? passwordHash { get; set; }
        [Required]
        public byte[]? passwordSalt{ get; set; }
        public string? token{ get; set; }
    }
}