using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace p22.database;

public partial class Pr22Context : DbContext
{
    public Pr22Context()
    {
    }

    public Pr22Context(DbContextOptions<Pr22Context> options)
        : base(options)
    {
    }

    public virtual DbSet<Filtr> Filtrs { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<Клиенты> Клиентыs { get; set; }

    public virtual DbSet<ТуристическиеТуры> ТуристическиеТурыs { get; set; }

    public virtual DbSet<Фирмы> Фирмыs { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=LAB17-13\\SQLEXPRESS; Database=pr22; User=ИСП-31; Password=1234567890; Encrypt=false");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Filtr>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("filtr");

            entity.Property(e => e.ДатаОтъезда).HasColumnName("Дата отъезда");
            entity.Property(e => e.СтоимостьТура)
                .HasColumnType("money")
                .HasColumnName("Стоимость тура");
            entity.Property(e => e.Страна).HasMaxLength(50);
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.Idrole);

            entity.ToTable("Role");

            entity.Property(e => e.Idrole)
                .ValueGeneratedNever()
                .HasColumnName("IDRole");
            entity.Property(e => e.Role1)
                .HasMaxLength(50)
                .HasColumnName("Role");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Iduser);

            entity.ToTable("User");

            entity.Property(e => e.Iduser)
                .ValueGeneratedNever()
                .HasColumnName("IDUser");
            entity.Property(e => e.Idrole).HasColumnName("IDRole");
            entity.Property(e => e.Name).HasMaxLength(50);
            entity.Property(e => e.Patronymic).HasMaxLength(50);
            entity.Property(e => e.Surname).HasMaxLength(50);

            entity.HasOne(d => d.IdroleNavigation).WithMany(p => p.Users)
                .HasForeignKey(d => d.Idrole)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_User_Role");
        });

        modelBuilder.Entity<Клиенты>(entity =>
        {
            entity.HasKey(e => e.ТабельныйНомер);

            entity.ToTable("Клиенты");

            entity.Property(e => e.ТабельныйНомер).HasColumnName("Табельный номер");
            entity.Property(e => e.Фамилия).HasMaxLength(50);
        });

        modelBuilder.Entity<ТуристическиеТуры>(entity =>
        {
            entity.HasKey(e => new { e.ДатаОтъезда, e.Клиент });

            entity.ToTable("ТуристическиеТуры");

            entity.Property(e => e.ДатаОтъезда).HasColumnName("Дата отъезда");
            entity.Property(e => e.СтоимостьТура)
                .HasColumnType("money")
                .HasColumnName("Стоимость тура");
            entity.Property(e => e.Страна).HasMaxLength(50);

            entity.HasOne(d => d.КлиентNavigation).WithMany(p => p.ТуристическиеТурыs)
                .HasForeignKey(d => d.Клиент)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ТуристическиеТуры_Клиенты");

            entity.HasOne(d => d.ФирмаNavigation).WithMany(p => p.ТуристическиеТурыs)
                .HasForeignKey(d => d.Фирма)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ТуристическиеТуры_Фирмы");
        });

        modelBuilder.Entity<Фирмы>(entity =>
        {
            entity.HasKey(e => e.Код);

            entity.ToTable("Фирмы");

            entity.Property(e => e.ФамилияДиректора)
                .HasMaxLength(50)
                .HasColumnName("Фамилия директора");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
