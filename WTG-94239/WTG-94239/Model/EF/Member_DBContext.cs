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
                optionsBuilder.UseMySql("Server=35.201.161.146;Database=Member_DB;uid=root;pwd=xiao-berg-project");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Account>(entity =>
            {
                entity.Property(e => e.Id).HasColumnType("int(11)");

                entity.Property(e => e.Account1)
                    .IsRequired()
                    .HasColumnName("Account")
                    .HasColumnType("varchar(45)");

                entity.Property(e => e.IsBanned)
                    .IsRequired()
                    .HasColumnType("bit(1)")
                    .HasDefaultValueSql("'b\\'0\\''");

                entity.Property(e => e.Md5password)
                    .IsRequired()
                    .HasColumnName("MD5Password")
                    .HasColumnType("varchar(45)");

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasColumnType("varchar(45)");

                entity.Property(e => e.SiteUserName)
                    .IsRequired()
                    .HasColumnType("varchar(45)");
            });

            modelBuilder.Entity<MemberInfo>(entity =>
            {
                entity.Property(e => e.Id).HasColumnType("int(11)");

                entity.Property(e => e.EMail)
                    .IsRequired()
                    .HasColumnName("E-mail")
                    .HasColumnType("varchar(200)");

                entity.Property(e => e.TrueName).HasColumnType("varchar(45)");

                entity.HasOne(d => d.IdNavigation)
                    .WithOne(p => p.MemberInfo)
                    .HasForeignKey<MemberInfo>(d => d.Id)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("ID");
            });
        }
    }
}
