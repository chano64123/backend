using BackendPrueba.Models;
using Microsoft.EntityFrameworkCore;

namespace BackendPrueba.Data {
    public class ApplicationDBContext : DbContext {
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options) : base(options) {

        }

        //se agregan los modelos de cada clase
        public DbSet<Client> clients { get; set; }
        public DbSet<User> users { get; set; }
    }
}
