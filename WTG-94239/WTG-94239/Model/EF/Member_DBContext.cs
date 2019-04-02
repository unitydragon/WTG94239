using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace WTG_94239.Model.EF
{
    public partial class Member_DBContext : DbContext
    {
        public Member_DBContext()
        {
        }

        public Member_DBContext(DbContextOptions<Member_DBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Account> Account { get; set; }
        public virtual DbSet<MemberInfo> MemberInfo { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseMySql("Server=35.240.150.30;port=3306;Database=Member_DB;uid=root;pwd=xiao-berg-project;Character Set=utf8;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Account>(entity =>
            {
                entity.HasKey(e => new { e.Id, e.Account1 })
                    .HasName("PRIMARY");

                entity.Property(e => e.Id).HasColumnType("int(11)");

                entity.Property(e => e.Account1)
                    .HasColumnName("Account")
                    .HasColumnType("varchar(45)");

                entity.Property(e => e.Md5password)
                    .IsRequired()
                    .HasColumnName("MD5Password")
                    .HasColumnType("varchar(45)");

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasColumnType("varchar(45)");

                entity.HasOne(d => d.IdNavigation)
                    .WithMany(p => p.Account)
                    .HasForeignKey(d => d.Id)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Account1_1Member");
            });

            modelBuilder.Entity<MemberInfo>(entity =>
            {
                entity.Property(e => e.Id).HasColumnType("int(11)");

                entity.Property(e => e.EMail)
                    .IsRequired()
                    .HasColumnName("E-mail")
                    .HasColumnType("varchar(200)");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnType("varchar(45)");
            });
        }
    }
}
