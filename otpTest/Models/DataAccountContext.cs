using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace otpTest.Models
{
    public partial class DataAccountContext : DbContext
    {
        public DataAccountContext()
            : base("name=DataAccountContext")
        {
        }

        public virtual DbSet<DataAccount> DataAccounts { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<DataAccount>()
                .Property(e => e.UID)
                .IsFixedLength();

            modelBuilder.Entity<DataAccount>()
                .Property(e => e.MatKhau)
                .IsFixedLength();
        }
    }
}
