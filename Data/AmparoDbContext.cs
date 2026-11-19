using Amparo.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace Amparo.Api.Data;

public class AmparoDbContext(DbContextOptions<AmparoDbContext> options) : DbContext(options)
{
    public DbSet<Cidadao> Cidadaos => Set<Cidadao>();
    public DbSet<Cuidador> Cuidadores => Set<Cuidador>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema(null);

        modelBuilder.Entity<Cidadao>(entity =>
        {
            entity.ToTable("AMPARO_API_CIDADAO");
            entity.HasKey(item => item.Id).HasName("PK_AMPARO_API_CIDADAO");
            entity.Property(item => item.Id).HasColumnName("ID").UseIdentityColumn();
            entity.Property(item => item.Nome).HasColumnName("NOME").HasMaxLength(150).IsRequired();
            entity.Property(item => item.DataNascimento).HasColumnName("DATA_NASCIMENTO").HasColumnType("DATE").IsRequired();
            entity.Property(item => item.Telefone).HasColumnName("TELEFONE").HasMaxLength(20).IsRequired();
            entity.Property(item => item.Endereco).HasColumnName("ENDERECO").HasMaxLength(300).IsRequired();
            entity.Property(item => item.Condicao).HasColumnName("CONDICAO").HasMaxLength(300);
            entity.Property(item => item.Ativo).HasColumnName("ATIVO").HasConversion<int>().HasDefaultValue(true).IsRequired();
        });

        modelBuilder.Entity<Cuidador>(entity =>
        {
            entity.ToTable("AMPARO_API_CUIDADOR");
            entity.HasKey(item => item.Id).HasName("PK_AMPARO_API_CUIDADOR");
            entity.Property(item => item.Id).HasColumnName("ID").UseIdentityColumn();
            entity.Property(item => item.Nome).HasColumnName("NOME").HasMaxLength(150).IsRequired();
            entity.Property(item => item.Telefone).HasColumnName("TELEFONE").HasMaxLength(20).IsRequired();
            entity.Property(item => item.Email).HasColumnName("EMAIL").HasMaxLength(150).IsRequired();
            entity.Property(item => item.Parentesco).HasColumnName("PARENTESCO").HasMaxLength(50).IsRequired();
            entity.Property(item => item.Prioridade).HasColumnName("PRIORIDADE").IsRequired();
            entity.Property(item => item.CidadaoId).HasColumnName("CIDADAO_ID").IsRequired();
            entity.HasIndex(item => item.CidadaoId).HasDatabaseName("IX_AMP_API_CUID_CID");
            entity.HasOne(item => item.Cidadao)
                .WithMany(item => item.Cuidadores)
                .HasForeignKey(item => item.CidadaoId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("FK_AMP_API_CUID_CID");
        });
    }
}
