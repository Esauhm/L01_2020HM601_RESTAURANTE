using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer;
namespace L01_2020HM601.Models
{
    public class restauranteContext : DbContext
    {
        public restauranteContext(DbContextOptions<restauranteContext> options) : base(options)
        {

        }

        public DbSet<Clientes> clientes { get; set; }

        public DbSet<pedidos> pedidos { get; set; }

        public DbSet<platos> platos { get; set; }



    }
}
