using System;
using System.Collections.Generic;
using ManageEmployees.Entities;
using Microsoft.EntityFrameworkCore;

namespace ManageEmployees.Infrastructures.Database;

public partial class ManageEmployeeDbContext : DbContext
{
    public ManageEmployeeDbContext()
    {
    }

    public ManageEmployeeDbContext(DbContextOptions<ManageEmployeeDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Attendance> Attendances { get; set; }

    public virtual DbSet<Department> Departments { get; set; }

    public virtual DbSet<Employee> Employees { get; set; }

    public virtual DbSet<Leaverequest> Leaverequests { get; set; }

    public virtual DbSet<Statusleaverequest> Statusleaverequests { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseNpgsql("Host=localhost;Database=ManageEmployees;Username=postgres;Password=root");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Attendance>(entity =>
        {
            entity.HasKey(e => e.Attendanceid).HasName("attendance_pkey");

            entity.ToTable("attendance");

            entity.Property(e => e.Attendanceid).HasColumnName("attendanceid");
            entity.Property(e => e.Arrivaldate).HasColumnName("arrivaldate");
            entity.Property(e => e.Departuredate).HasColumnName("departuredate");
            entity.Property(e => e.Employeeid).HasColumnName("employeeid");

            entity.HasOne(d => d.Employee).WithMany(p => p.Attendances)
                .HasForeignKey(d => d.Employeeid)
                .HasConstraintName("fk_attendance_employeeid");
        });

        modelBuilder.Entity<Department>(entity =>
        {
            entity.HasKey(e => e.Departmentid).HasName("department_pkey");

            entity.ToTable("department");

            entity.HasIndex(e => e.Name, "department_name_key").IsUnique();

            entity.Property(e => e.Departmentid).HasColumnName("departmentid");
            entity.Property(e => e.Address)
                .HasMaxLength(255)
                .HasColumnName("address");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .HasColumnName("name");
        });

        modelBuilder.Entity<Employee>(entity =>
        {
            entity.HasKey(e => e.Employeeid).HasName("employees_pkey");

            entity.ToTable("employees");

            entity.HasIndex(e => e.Email, "employees_email_key").IsUnique();

            entity.Property(e => e.Employeeid).HasColumnName("employeeid");
            entity.Property(e => e.Birthday).HasColumnName("birthday");
            entity.Property(e => e.Email)
                .HasMaxLength(255)
                .HasColumnName("email");
            entity.Property(e => e.Firstname)
                .HasMaxLength(255)
                .HasColumnName("firstname");
            entity.Property(e => e.Lastname)
                .HasMaxLength(255)
                .HasColumnName("lastname");
            entity.Property(e => e.Phonenumber)
                .HasMaxLength(20)
                .HasColumnName("phonenumber");
            entity.Property(e => e.Position)
                .HasMaxLength(255)
                .HasColumnName("position");

            entity.HasMany(d => d.Departments).WithMany(p => p.Employees)
                .UsingEntity<Dictionary<string, object>>(
                    "Employeesdepartement",
                    r => r.HasOne<Department>().WithMany()
                        .HasForeignKey("Departmentid")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("fk_employeesdepartement_departmentid"),
                    l => l.HasOne<Employee>().WithMany()
                        .HasForeignKey("Employeeid")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("fk_employeesdepartement_employeeid"),
                    j =>
                    {
                        j.HasKey("Employeeid", "Departmentid").HasName("employeesdepartement_pkey");
                        j.ToTable("employeesdepartement");
                        j.IndexerProperty<int>("Employeeid").HasColumnName("employeeid");
                        j.IndexerProperty<int>("Departmentid").HasColumnName("departmentid");
                    });
        });

        modelBuilder.Entity<Leaverequest>(entity =>
        {
            entity.HasKey(e => e.Leaverequestid).HasName("leaverequest_pkey");

            entity.ToTable("leaverequest");

            entity.Property(e => e.Leaverequestid).HasColumnName("leaverequestid");
            entity.Property(e => e.Employeeid).HasColumnName("employeeid");
            entity.Property(e => e.Enddate)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("enddate");
            entity.Property(e => e.Requestdate)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("requestdate");
            entity.Property(e => e.Startdate)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("startdate");
            entity.Property(e => e.Statusleaverequestid).HasColumnName("statusleaverequestid");

            entity.HasOne(d => d.Employee).WithMany(p => p.Leaverequests)
                .HasForeignKey(d => d.Employeeid)
                .HasConstraintName("fk_leaverequest_employeeid");

            entity.HasOne(d => d.Statusleaverequest).WithMany(p => p.Leaverequests)
                .HasForeignKey(d => d.Statusleaverequestid)
                .HasConstraintName("leaverequest_statusleaverequestid_fkey");
        });

        modelBuilder.Entity<Statusleaverequest>(entity =>
        {
            entity.HasKey(e => e.Statusleaverequestid).HasName("statusleaverequest_pkey");

            entity.ToTable("statusleaverequest");

            entity.Property(e => e.Statusleaverequestid).HasColumnName("statusleaverequestid");
            entity.Property(e => e.Statuslabel)
                .HasMaxLength(50)
                .HasColumnName("statuslabel");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
