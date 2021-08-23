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

        /// <summary>
        /// Hospitals from the data store
        /// </summary>
        public DbSet<Entities.Hospital> Hospitals { get; set; }

        public HospitalContext(DbContextOptions options) :base(options) {}

        #endregion public

        #region protected

        /// <summary>
        /// override to build the model using fluent api
        /// </summary>
        /// <param name="modelBuilder">the builder</param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // add the hospital entity
            modelBuilder.Entity<Entities.Hospital>();
            
            //
            // set up sequence for primary keys
            //
            modelBuilder.HasSequence<int>(PRIMARY_KEY_SEQ_NAME)
                .StartsAt(1000)
                .IncrementsBy(1);

            modelBuilder.Entity<Entities.Hospital>()
                .Property(_ => _.Id)
                .HasDefaultValueSql($"NEXT VALUE FOR {PRIMARY_KEY_SEQ_NAME}");

                //.HasDefaultValueSql($"NEXT VALUE FOR {PRIMARY_KEY_SEQ_NAME};");
                /*Grrrrrrrrrrrrrrrrrrrrr freaking semi-colon right there     ^. 4 hours of me griping about SQLExpress. */ 
        }

        #endregion protected
    }
}
