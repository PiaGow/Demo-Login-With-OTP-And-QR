using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace otpTest
{
    public partial class Model1 : DbContext
    {
        public Model1()
            : base("name=ModelAccount")
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
