using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Study_TBF_Stats.Models;

public partial class StudyTbfSupContext : DbContext
{
    public StudyTbfSupContext()
    {
    }

    public StudyTbfSupContext(DbContextOptions<StudyTbfSupContext> options)
        : base(options)
    {
    }

    public virtual DbSet<TbProject> TbProjects { get; set; }

    public virtual DbSet<TbTimeSheet> TbTimeSheets { get; set; }

    public virtual DbSet<TbUser> TbUsers { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("name=DB");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<TbProject>(entity =>
        {
            entity.Property(e => e.ProjectId).ValueGeneratedNever();
        });

        modelBuilder.Entity<TbTimeSheet>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedNever();

            entity.HasOne(d => d.Project).WithMany(p => p.TbTimeSheets).HasConstraintName("FK_tbTimeSheet_tbProject");

            entity.HasOne(d => d.User).WithMany(p => p.TbTimeSheets).HasConstraintName("FK_tbTimeSheet_tbUser");
        });

        modelBuilder.Entity<TbUser>(entity =>
        {
            entity.Property(e => e.UserId).ValueGeneratedNever();
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
