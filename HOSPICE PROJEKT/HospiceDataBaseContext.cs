using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace HOSPICE_PROJEKT
{
    public partial class HospiceDataBaseContext : DbContext
    {
        public HospiceDataBaseContext()
        {
        }

        public HospiceDataBaseContext(DbContextOptions<HospiceDataBaseContext> options)
            : base(options)
        {
        }

        public virtual DbSet<HospiceRoom> HospiceRooms { get; set; } = null!;
        public virtual DbSet<PatientsAdressDatum> PatientsAdressData { get; set; } = null!;
        public virtual DbSet<PatientsCondition> PatientsConditions { get; set; } = null!;
        public virtual DbSet<PatientsDeathDate> PatientsDeathDates { get; set; } = null!;
        public virtual DbSet<PatientsFee> PatientsFees { get; set; } = null!;
        public virtual DbSet<PatientsMedication> PatientsMedications { get; set; } = null!;
        public virtual DbSet<PatientsPersonalDatum> PatientsPersonalData { get; set; } = null!;
        public virtual DbSet<VisitorsDatum> VisitorsData { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Data Source=DESKTOP-26AVSHR;Initial Catalog=HospiceDataBase;Integrated Security=True");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<HospiceRoom>(entity =>
            {
                entity.HasKey(e => e.BedId);

                entity.ToTable("Hospice_rooms");

                entity.HasIndex(e => e.PatientId, "UQ__Hospice___C1A88B58EF769AA2")
                    .IsUnique();

                entity.Property(e => e.BedId)
                    .ValueGeneratedNever()
                    .HasColumnName("Bed_ID");

                entity.Property(e => e.PatientId).HasColumnName("Patient_ID");

                entity.Property(e => e.RoomNr).HasColumnName("Room_nr");

                entity.HasOne(d => d.Patient)
                    .WithOne(p => p.HospiceRoom)
                    .HasForeignKey<HospiceRoom>(d => d.PatientId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Hospice_rooms");
            });

            modelBuilder.Entity<PatientsAdressDatum>(entity =>
            {
                entity.HasKey(e => e.PatientId);

                entity.ToTable("Patients_adress_data");

                entity.Property(e => e.PatientId)
                    .ValueGeneratedNever()
                    .HasColumnName("Patient_ID");

                entity.Property(e => e.FlatNr).HasColumnName("Flat_nr");

                entity.Property(e => e.Locality).HasMaxLength(30);

                entity.Property(e => e.Street).HasMaxLength(30);

                entity.Property(e => e.StreetNr).HasColumnName("Street_nr");

                entity.Property(e => e.ZipCode)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("Zip_code")
                    .IsFixedLength();
            });

            modelBuilder.Entity<PatientsCondition>(entity =>
            {
                entity.HasKey(e => e.TestId);

                entity.ToTable("Patients_condition");

                entity.Property(e => e.TestId).HasColumnName("Test_ID");

                entity.Property(e => e.Condition).HasMaxLength(40);

                entity.Property(e => e.PatientId).HasColumnName("Patient_ID");

                entity.HasOne(d => d.Patient)
                    .WithMany(p => p.PatientsConditions)
                    .HasForeignKey(d => d.PatientId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Patients_condition");
            });

            modelBuilder.Entity<PatientsDeathDate>(entity =>
            {
                entity.HasKey(e => e.PatientId)
                    .HasName("PK__Death_date");

                entity.ToTable("Patients_death_date");

                entity.Property(e => e.PatientId)
                    .ValueGeneratedNever()
                    .HasColumnName("Patient_ID");

                entity.Property(e => e.CauseOfDeath)
                    .HasMaxLength(40)
                    .HasColumnName("Cause_of_death");

                entity.Property(e => e.DeathDate)
                    .HasColumnType("date")
                    .HasColumnName("Death_date");
            });

            modelBuilder.Entity<PatientsFee>(entity =>
            {
                entity.HasKey(e => e.FeeId);

                entity.ToTable("Patients_Fees");

                entity.Property(e => e.FeeId).HasColumnName("Fee_ID");

                entity.Property(e => e.FeeAmount)
                    .HasColumnType("money")
                    .HasColumnName("Fee_amount");

                entity.Property(e => e.PatientId).HasColumnName("Patient_ID");

                entity.HasOne(d => d.Patient)
                    .WithMany(p => p.PatientsFees)
                    .HasForeignKey(d => d.PatientId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Patients_Fees");
            });

            modelBuilder.Entity<PatientsMedication>(entity =>
            {
                entity.HasKey(e => e.TreatmentId);

                entity.ToTable("Patients_medications");

                entity.Property(e => e.TreatmentId).HasColumnName("Treatment_ID");

                entity.Property(e => e.DosageOfMed)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("Dosage_of_med")
                    .IsFixedLength();

                entity.Property(e => e.MedicationId)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("Medication_ID")
                    .IsFixedLength();

                entity.Property(e => e.PatientId).HasColumnName("Patient_ID");

                entity.HasOne(d => d.Patient)
                    .WithMany(p => p.PatientsMedications)
                    .HasForeignKey(d => d.PatientId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Patients_medications");
            });

            modelBuilder.Entity<PatientsPersonalDatum>(entity =>
            {
                entity.HasKey(e => e.PatientId);

                entity.ToTable("Patients_personal_data");

                entity.Property(e => e.PatientId).HasColumnName("Patient_ID");

                entity.Property(e => e.Name).HasMaxLength(20);

                entity.Property(e => e.Pesel)
                    .HasMaxLength(11)
                    .IsUnicode(false)
                    .HasColumnName("PESEL")
                    .IsFixedLength();

                entity.Property(e => e.PhoneNumber)
                    .HasMaxLength(15)
                    .IsUnicode(false)
                    .HasColumnName("Phone_number")
                    .IsFixedLength();

                entity.Property(e => e.Surname).HasMaxLength(20);
            });

            modelBuilder.Entity<VisitorsDatum>(entity =>
            {
                entity.HasKey(e => e.VisitId);

                entity.ToTable("Visitors_data");

                entity.Property(e => e.VisitId).HasColumnName("Visit_ID");

                entity.Property(e => e.DegOfKinship)
                    .HasMaxLength(15)
                    .HasColumnName("Deg_of_kinship");

                entity.Property(e => e.Name).HasMaxLength(20);

                entity.Property(e => e.PatientId).HasColumnName("Patient_ID");

                entity.Property(e => e.PhoneNumber)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("Phone_number")
                    .IsFixedLength();

                entity.Property(e => e.Surname).HasMaxLength(20);

                entity.HasOne(d => d.Patient)
                    .WithMany(p => p.VisitorsData)
                    .HasForeignKey(d => d.PatientId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Visitors_data");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
