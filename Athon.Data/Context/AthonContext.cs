using Athon.Data.Mappings;
using Athon.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Athon.Data.Context
{
    public class AthonContext : DbContext
    {
        public AthonContext(DbContextOptions<AthonContext> option)
            : base(option) { }

        #region "DBSETS"

        public DbSet<Usuario> Usuarios { get; set; }

        #endregion


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UsuarioMap());

            base.OnModelCreating(modelBuilder);
        
        }
    }

}
