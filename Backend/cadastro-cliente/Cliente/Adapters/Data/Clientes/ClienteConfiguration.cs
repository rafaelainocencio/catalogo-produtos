using Domain.Cliente;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.Clientes
{
    public class ClienteConfiguration : IEntityTypeConfiguration<Cliente>
    {
        public void Configure(EntityTypeBuilder<Cliente> builder)
        {
            builder.HasKey(c => c.Id);
            builder.Property(c => c.Nome).HasMaxLength(100).IsRequired();
            builder.Property(c => c.Email).HasMaxLength(100).IsRequired();
            builder.Property(builder => builder.Desativado).HasDefaultValue(false);
            
            builder.OwnsOne(c => c.Documento)
                .Property(d => d.Numero);

            builder.OwnsOne(c => c.Documento)
                .Property(d => d.Tipo);
        }
    }
}
