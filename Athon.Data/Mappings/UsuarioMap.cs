using Athon.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Athon.Data.Mappings
{
    public class UsuarioMap: IEntityTypeConfiguration<Usuario>
    {

        public void Configure(EntityTypeBuilder<Usuario> builder)
        {
            builder.Property(x => x.IdUsuario).IsRequired();

            builder.Property(x => x.Nome).HasMaxLength(150).IsRequired();

            builder.Property(x => x.DataCadastro).IsRequired();
            
            builder.Property(x => x.NomeLogin).HasMaxLength(80).IsRequired();
            
            builder.Property(x => x.Email).HasMaxLength(80).IsRequired();
            
            builder.Property(x => x.Cpf).HasMaxLength(150).IsRequired();
            
            builder.Property(x => x.DataNascimento).IsRequired();
            
            builder.Property(x => x.Telefone).HasMaxLength(30).IsRequired();
            
            builder.Property(x => x.Celular).HasMaxLength(30).IsRequired();
            
            builder.Property(x => x.Ativo).IsRequired();

        }

    }
}
