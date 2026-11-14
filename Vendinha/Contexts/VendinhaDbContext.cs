using Microsoft.EntityFrameworkCore;
using Vendinha.Models;

namespace Vendinha.Contexts
{
    public class VendinhaDbContext : DbContext
    {
        // Usada pelo console e pelo desktop (a API usa o appsettings.json).
        // Pode ser sobrescrita pela variável de ambiente VENDINHA_CONNECTION.
        public static string ConnectionStringPadrao =>
            Environment.GetEnvironmentVariable("VENDINHA_CONNECTION") ??
            "Server=localhost;Port=5432;Database=vendinhas;User Id=postgres;Password=158575;";

        private readonly string connectionString;

        public VendinhaDbContext(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public VendinhaDbContext(DbContextOptions<VendinhaDbContext> options) : base(options)
        {
        }

        public DbSet<Cliente> Clientes => Set<Cliente>();
        public DbSet<Divida> Dividas => Set<Divida>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // mapeamento
            var entityCliente = modelBuilder.Entity<Cliente>();
            entityCliente.ToTable("clientes");
            entityCliente.HasKey(c => c.Id);
            entityCliente.Property(c => c.Id).HasColumnName("id");
            entityCliente.Property(c => c.NomeCompleto).HasColumnName("nomecompleto").HasMaxLength(150);
            entityCliente.Property(c => c.Cpf).HasColumnName("cpf").HasMaxLength(11);
            entityCliente.Property(c => c.DataNascimento).HasColumnName("datanascimento").HasColumnType("date");
            entityCliente.Property(c => c.Email).HasColumnName("email").HasMaxLength(150);
            entityCliente.Ignore(c => c.Idade);
            entityCliente.HasIndex(c => c.Cpf).IsUnique().HasDatabaseName("ux_clientes_cpf");

            var entityDivida = modelBuilder.Entity<Divida>();
            entityDivida.ToTable("dividas");
            entityDivida.HasKey(d => d.Id);
            entityDivida.Property(d => d.Id).HasColumnName("id");
            entityDivida.Property(d => d.ClienteId).HasColumnName("clienteid");
            entityDivida.Property(d => d.Valor).HasColumnName("valor").HasColumnType("numeric(12,2)");
            entityDivida.Property(d => d.Paga).HasColumnName("paga");
            entityDivida.Property(d => d.DataCriacao).HasColumnName("datacriacao").HasColumnType("timestamptz");
            entityDivida.Property(d => d.DataPagamento).HasColumnName("datapagamento").HasColumnType("timestamptz");

            // Regra: um cliente só pode ter uma dívida em aberto
            entityDivida.HasIndex(d => d.ClienteId)
                .IsUnique()
                .HasFilter("paga = false")
                .HasDatabaseName("ux_dividas_cliente_aberta");

            // Relacionamento 1 cliente : N dívidas
            entityCliente.HasMany(c => c.Dividas)
                .WithOne(d => d.Cliente)
                .HasForeignKey(d => d.ClienteId)
                .OnDelete(DeleteBehavior.Cascade);

            base.OnModelCreating(modelBuilder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!string.IsNullOrEmpty(connectionString))
            {
                optionsBuilder.UseNpgsql(connectionString);
            }
            base.OnConfiguring(optionsBuilder);
        }
    }
}
