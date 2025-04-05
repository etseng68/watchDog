using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace MixWeb.Models
{
    public partial class MixWebContext : DbContext
    {
        public MixWebContext()
        {
        }

        public MixWebContext(DbContextOptions<MixWebContext> options)
            : base(options)
        {
        }

        public virtual DbSet<MaccessLog> MaccessLogs { get; set; } = null!;
        public virtual DbSet<Maccount> Maccounts { get; set; } = null!;
        public virtual DbSet<Mdomain> Mdomains { get; set; } = null!;
        public virtual DbSet<MdomainLog> MdomainLogs { get; set; } = null!;
        public virtual DbSet<MgetLog> MgetLogs { get; set; } = null!;
        public virtual DbSet<Morg> Morgs { get; set; } = null!;
        public virtual DbSet<MorgLog> MorgLogs { get; set; } = null!;
        public virtual DbSet<Mpage> Mpages { get; set; } = null!;
        public virtual DbSet<Mpermission> Mpermissions { get; set; } = null!;
        public virtual DbSet<Mwatch> Mwatches { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<MaccessLog>(entity =>
            {
                entity.ToTable("MAccessLog");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.LogTime)
                    .HasColumnType("datetime")
                    .HasColumnName("logTime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Note).HasColumnName("note");

                entity.Property(e => e.UserId).HasColumnName("userId");

                entity.Property(e => e.UserIp)
                    .HasMaxLength(50)
                    .HasColumnName("userIp");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.MaccessLogs)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_MAccessLog_MAccount");
            });

            modelBuilder.Entity<Maccount>(entity =>
            {
                entity.ToTable("MAccount");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.CreateAt)
                    .HasColumnType("datetime")
                    .HasColumnName("create_at")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Disable).HasColumnName("disable");

                entity.Property(e => e.Email)
                    .HasMaxLength(50)
                    .HasColumnName("email");

                entity.Property(e => e.ModifyAt)
                    .HasColumnType("datetime")
                    .HasColumnName("modify_at")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .HasColumnName("name");

                entity.Property(e => e.Note).HasColumnName("note");

                entity.Property(e => e.Org)
                    .HasMaxLength(20)
                    .HasColumnName("org");

                entity.Property(e => e.Password)
                    .HasMaxLength(50)
                    .HasColumnName("password");

                entity.Property(e => e.Role).HasColumnName("role");

                entity.HasOne(d => d.OrgNavigation)
                    .WithMany(p => p.Maccounts)
                    .HasPrincipalKey(p => p.Org)
                    .HasForeignKey(d => d.Org)
                    .HasConstraintName("FK_MAccount_MOrg");

                entity.HasOne(d => d.RoleNavigation)
                    .WithMany(p => p.Maccounts)
                    .HasForeignKey(d => d.Role)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_MAccount_MPermission");
            });

            modelBuilder.Entity<Mdomain>(entity =>
            {
                entity.ToTable("MDomain");

                entity.HasIndex(e => e.Wurl, "IX_MDomain")
                    .IsUnique();

                entity.Property(e => e.CreateAt)
                    .HasColumnType("datetime")
                    .HasColumnName("Create_at")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Essl)
                    .HasColumnName("ESsl")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.Etime).HasColumnName("ETime");

                entity.Property(e => e.ModifyAt)
                    .HasColumnType("datetime")
                    .HasColumnName("Modify_at")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Org).HasMaxLength(20);

                entity.Property(e => e.Rtime)
                    .HasColumnName("RTime")
                    .HasDefaultValueSql("((2))");

                entity.Property(e => e.Wact)
                    .HasMaxLength(10)
                    .HasColumnName("WAct")
                    .HasDefaultValueSql("(N'GET')");

                entity.Property(e => e.Wmemo)
                    .HasMaxLength(255)
                    .HasColumnName("WMemo");

                entity.Property(e => e.Wname)
                    .HasMaxLength(50)
                    .HasColumnName("WName");

                entity.Property(e => e.Wrun)
                    .IsRequired()
                    .HasColumnName("WRun")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.Wssl)
                    .HasColumnName("WSsl")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.Wurl)
                    .HasMaxLength(300)
                    .HasColumnName("WUrl");

                entity.HasOne(d => d.OrgNavigation)
                    .WithMany(p => p.Mdomains)
                    .HasPrincipalKey(p => p.Org)
                    .HasForeignKey(d => d.Org)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_MDomain_MOrg");
            });

            modelBuilder.Entity<MdomainLog>(entity =>
            {
                entity.ToTable("MDomainLog");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.CreateAt)
                    .HasColumnType("datetime")
                    .HasColumnName("createAt");

                entity.Property(e => e.DomainId).HasColumnName("domainId");

                entity.Property(e => e.Note).HasColumnName("note");

                entity.Property(e => e.UrserId).HasColumnName("urserId");

                entity.HasOne(d => d.Domain)
                    .WithMany(p => p.MdomainLogs)
                    .HasForeignKey(d => d.DomainId)
                    .HasConstraintName("FK_MDomainLog_MDomain");

                entity.HasOne(d => d.Urser)
                    .WithMany(p => p.MdomainLogs)
                    .HasForeignKey(d => d.UrserId)
                    .HasConstraintName("FK_MDomainLog_MAccount");
            });

            modelBuilder.Entity<MgetLog>(entity =>
            {
                entity.ToTable("MGetLog");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.CreateAt)
                    .HasColumnType("datetime")
                    .HasColumnName("createAt")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.ModiyAt)
                    .HasColumnType("datetime")
                    .HasColumnName("modiyAt")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Note).HasColumnName("note");
            });

            modelBuilder.Entity<Morg>(entity =>
            {
                entity.ToTable("MOrg");

                entity.HasIndex(e => e.Org, "IX_MOrg")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Disable).HasColumnName("disable");

                entity.Property(e => e.Note)
                    .HasMaxLength(50)
                    .HasColumnName("note");

                entity.Property(e => e.Org)
                    .HasMaxLength(20)
                    .HasColumnName("org");

                entity.Property(e => e.Tgbot).HasColumnName("tgbot");
            });

            modelBuilder.Entity<MorgLog>(entity =>
            {
                entity.ToTable("MOrgLog");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.CreateAt)
                    .HasColumnType("datetime")
                    .HasColumnName("createAt")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Note).HasColumnName("note");

                entity.Property(e => e.OrgId).HasColumnName("orgId");

                entity.Property(e => e.UserId).HasColumnName("userId");

                entity.HasOne(d => d.Org)
                    .WithMany(p => p.MorgLogs)
                    .HasForeignKey(d => d.OrgId)
                    .HasConstraintName("FK_MOrgLog_MOrg");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.MorgLogs)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK_MOrgLog_MAccount");
            });

            modelBuilder.Entity<Mpage>(entity =>
            {
                entity.ToTable("MPage");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Main)
                    .HasMaxLength(50)
                    .HasColumnName("main");

                entity.Property(e => e.Note).HasColumnName("note");

                entity.Property(e => e.Path)
                    .HasMaxLength(100)
                    .HasColumnName("path");

                entity.Property(e => e.Role).HasColumnName("role");

                entity.Property(e => e.Sub)
                    .HasMaxLength(50)
                    .HasColumnName("sub");
            });

            modelBuilder.Entity<Mpermission>(entity =>
            {
                entity.ToTable("MPermission");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Disable).HasColumnName("disable");

                entity.Property(e => e.Note)
                    .HasMaxLength(50)
                    .HasColumnName("note");

                entity.Property(e => e.Role)
                    .HasMaxLength(10)
                    .HasColumnName("role");
            });

            modelBuilder.Entity<Mwatch>(entity =>
            {
                entity.ToTable("MWatch");

                entity.HasIndex(e => e.Org, "IX_MWatch_Org");

                entity.HasIndex(e => e.Status, "IX_MWatch_Status");

                entity.HasIndex(e => e.Url, "IX_MWatch_Url");

                entity.HasIndex(e => e.Wtime, "IX_MWatch_WTime");

                entity.Property(e => e.CreateAt)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Org).HasMaxLength(20);

                entity.Property(e => e.Status).HasMaxLength(100);

                entity.Property(e => e.Url).HasMaxLength(50);

                entity.Property(e => e.WdateTime)
                    .HasColumnType("datetime")
                    .HasColumnName("WDateTime");

                entity.Property(e => e.Wtime)
                    .HasMaxLength(50)
                    .HasColumnName("WTime");

                entity.HasOne(d => d.Domain)
                    .WithMany(p => p.Mwatches)
                    .HasForeignKey(d => d.DomainId)
                    .HasConstraintName("FK_MWatch_MDomain");

                entity.HasOne(d => d.Get)
                    .WithMany(p => p.Mwatches)
                    .HasForeignKey(d => d.GetId)
                    .HasConstraintName("FK_MWatch_MGetLog");

                entity.HasOne(d => d.OrgNavigation)
                    .WithMany(p => p.Mwatches)
                    .HasForeignKey(d => d.OrgId)
                    .HasConstraintName("FK_MWatch_MOrg");
            });

            modelBuilder.Entity<Mdomain>()
                .HasMany(md => md.MdomainLogs)
                .WithOne(ml => ml.Domain)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Mdomain>()
                .HasMany(md => md.Mwatches)
                .WithOne(mw => mw.Domain)
                .OnDelete(DeleteBehavior.Cascade);

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
