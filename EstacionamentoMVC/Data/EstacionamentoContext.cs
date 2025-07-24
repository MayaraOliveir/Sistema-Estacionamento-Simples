using Microsoft.EntityFrameworkCore;
using EstacionamentoMVC.Models;

namespace EstacionamentoMVC.Data
{
    public class EstacionamentoContext : DbContext
    {
        public EstacionamentoContext(DbContextOptions<EstacionamentoContext> options)
            : base(options)
        {
        }

        // DbSet que representa a tabela de veículos no banco
        public DbSet<Veiculo> Veiculos { get; set; }
    }
}
