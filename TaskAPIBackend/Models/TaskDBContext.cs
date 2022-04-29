using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace TaskAPI.Models
{
    public partial class TaskDBContext : DbContext
    {
        public TaskDBContext()
        {
        }

        public TaskDBContext(DbContextOptions<TaskDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Customers> Customers { get; set; }
        public virtual DbSet<Msgs> Msgs { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Data Source=.;Initial Catalog=TaskDB;Integrated Security=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Customers>(entity =>
            {
                entity.HasKey(e => e.CustomerId);

                entity.Property(e => e.CustomerId)
                    .HasColumnName("Customer_ID")
                    .ValueGeneratedNever();

                entity.Property(e => e.CustName)
                    .HasColumnName("Cust_name")
                    .HasMaxLength(50);

                entity.Property(e => e.CustPhone)
                    .HasColumnName("Cust_phone")
                    .HasMaxLength(14)
                    .IsFixedLength();
            });

            modelBuilder.Entity<Msgs>(entity =>
            {
                entity.HasKey(e => new { e.CustId, e.MsgSubject });

                entity.Property(e => e.CustId).HasColumnName("Cust_ID");

                entity.Property(e => e.MsgSubject)
                    .HasColumnName("Msg_subject")
                    .HasMaxLength(10)
                    .IsFixedLength();

                entity.Property(e => e.MsgBody).HasColumnName("Msg_Body");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
