using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace AHTG.Hospital.ObjectModel.Context
{
    public partial class HospitalContext : DbContext
    {
        /// <summary>
        /// Data store sequence name used for generation of all primary keys in this 
        /// DbContext implementation.
        /// </summary>
        const string PRIMARY_KEY_SEQ_NAME = "PrimaryKey_Seq";

        #region public

        public DbSet<Entities.Hospital> Hospitals { get; set; }

        public HospitalContext(DbContextOptions options)
            :base(options)
        {
        }

        #endregion public

        #region protected

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Entities.Hospital>();
            
            //base.Database.EnsureCreated();

#if !IN_MEMORY_CONTEXT

            //
            // set up sequence for primary keys
            //
            modelBuilder.HasSequence<int>(PRIMARY_KEY_SEQ_NAME)
                .StartsAt(1000)
                .IncrementsBy(1);

            modelBuilder.Entity<Entities.Hospital>()
                .Property(_ => _.Id)
                .HasDefaultValue($"NEXT VALUE FOR {PRIMARY_KEY_SEQ_NAME};");
#endif
        }

        #endregion protected
    }
}
