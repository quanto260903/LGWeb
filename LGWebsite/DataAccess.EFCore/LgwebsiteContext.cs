using System;
using System.Collections.Generic;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.EFCore;

public partial class LgwebsiteContext : DbContext
{
    public LgwebsiteContext()
    {
    }

    public LgwebsiteContext(DbContextOptions<LgwebsiteContext> options)
        : base(options)
    {
    }

    public virtual DbSet<BlogsCategory> BlogsCategories { get; set; }

    public virtual DbSet<Category> Categories { get; set; }

    public virtual DbSet<Contact> Contacts { get; set; }

    public virtual DbSet<Menu> Menus { get; set; }

    public virtual DbSet<Slide> Slides { get; set; }

    public virtual DbSet<SystemConfiguration> SystemConfigurations { get; set; }

    public virtual DbSet<Upload> Uploads { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<Video> Videos { get; set; }

    public virtual DbSet<WebConfiguration> WebConfigurations { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("server=TOMINHQUAN\\SQLEXPRESS01;database=LGWebsite;uid=sa;pwd=tmq260903;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<BlogsCategory>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__BlogsCat__3214EC07E69899DE");

            entity.ToTable("BlogsCategory");

            entity.Property(e => e.Color)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.ImageUrl).HasMaxLength(255);
            entity.Property(e => e.Name).HasMaxLength(255);
        });

        modelBuilder.Entity<Category>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Categori__3214EC07A0E304F4");

            entity.Property(e => e.ImageUrl).HasMaxLength(255);
            entity.Property(e => e.Name).HasMaxLength(255);
        });

        modelBuilder.Entity<Contact>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Contacts__3214EC07709354BC");

            entity.Property(e => e.Email).HasMaxLength(255);
            entity.Property(e => e.Message).HasMaxLength(500);
            entity.Property(e => e.Name).HasMaxLength(255);
        });

        modelBuilder.Entity<Menu>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Menu__3214EC075445BB12");

            entity.ToTable("Menu");

            entity.Property(e => e.Icon).HasMaxLength(255);
            entity.Property(e => e.ImageUrl).HasMaxLength(255);
            entity.Property(e => e.Name).HasMaxLength(255);
        });

        modelBuilder.Entity<Slide>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Slides__3214EC07117E5ED2");

            entity.Property(e => e.ImageUrl).HasMaxLength(255);
            entity.Property(e => e.Url).HasMaxLength(500);
        });

        modelBuilder.Entity<SystemConfiguration>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__System_C__3214EC0710CAFBDF");

            entity.ToTable("System_Configuration");

            entity.Property(e => e.ConfigKey).HasMaxLength(255);
            entity.Property(e => e.Description).HasMaxLength(500);
        });

        modelBuilder.Entity<Upload>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Upload__3214EC073DD4A0EA");

            entity.ToTable("Upload");

            entity.Property(e => e.ImageIconUrl).HasMaxLength(500);
            entity.Property(e => e.ImageThumbUrl).HasMaxLength(500);
            entity.Property(e => e.ImageUrl).HasMaxLength(500);
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Users__3214EC07581E351F");

            entity.Property(e => e.Email).HasMaxLength(255);
            entity.Property(e => e.PassWord).HasMaxLength(255);
            entity.Property(e => e.UserName).HasMaxLength(255);
        });

        modelBuilder.Entity<Video>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Videos__3214EC07AF69A40F");
        });

        modelBuilder.Entity<WebConfiguration>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Web_Conf__3214EC070E0F2AD6");

            entity.ToTable("Web_Configuration");

            entity.Property(e => e.ConfigKey).HasMaxLength(255);
            entity.Property(e => e.Description).HasMaxLength(500);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
