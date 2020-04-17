namespace db_ORM
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class opendata_context : DbContext
    {
        public opendata_context()
            : base("name=opendata_context")
        {
        }

        public virtual DbSet<branch> branches { get; set; }
        public virtual DbSet<inventory> inventories { get; set; }
        public virtual DbSet<number_of_inventory_in_branch> number_of_inventory_in_branch { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<branch>()
                .HasMany(e => e.number_of_inventory_in_branch)
                .WithRequired(e => e.branch)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<inventory>()
                .HasMany(e => e.number_of_inventory_in_branch)
                .WithRequired(e => e.inventory)
                .WillCascadeOnDelete(false);
        }
    }
}
