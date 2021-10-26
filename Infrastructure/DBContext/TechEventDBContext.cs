using Microsoft.EntityFrameworkCore;

#nullable disable

namespace GraphQLAPI.Infrastructure.DBContext
{
    public partial class TechEventDBContext : DbContext
    {
        public TechEventDBContext()
        {
        }

        public TechEventDBContext(DbContextOptions<TechEventDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<EventParticipant> EventParticipants { get; set; }
        public virtual DbSet<Participant> Participants { get; set; }
        public virtual DbSet<TechEventInfo> TechEventInfos { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=localhost\\SQLEXPRESS;Database=TechEventDB;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<EventParticipant>(entity =>
            {
                entity.HasOne(d => d.Event)
                    .WithMany(p => p.EventParticipants)
                    .HasForeignKey(d => d.EventId)
                    .OnDelete(DeleteBehavior.ClientSetNull);

                entity.HasOne(d => d.Participant)
                    .WithMany(p => p.EventParticipants)
                    .HasForeignKey(d => d.ParticipantId);
            });

            modelBuilder.Entity<Participant>(entity =>
            {
                entity.ToTable("Participant");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.ParticipantName)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Phone)
                    .HasMaxLength(20)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<TechEventInfo>(entity =>
            {
                entity.HasKey(e => e.EventId);

                entity.ToTable("TechEventInfo");

                entity.Property(e => e.EventDate).HasColumnType("date");

                entity.Property(e => e.EventName)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Speaker)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
