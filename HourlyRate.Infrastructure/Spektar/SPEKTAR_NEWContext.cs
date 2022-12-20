using HourlyRate.Infrastructure.Spektar.Models;
using Microsoft.EntityFrameworkCore;

namespace HourlyRate.Infrastructure.Spektar
{
    public partial class SPEKTAR_NEWContext : DbContext
    {
        public SPEKTAR_NEWContext()
        {
        }

        public SPEKTAR_NEWContext(DbContextOptions<SPEKTAR_NEWContext> options)
            : base(options)
        {
        }

        public virtual DbSet<ordrub__> ordrub__ { get; set; } = null!;
        public virtual DbSet<rubrik__> rubrik__ { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=LUZTERIN-PC;Database=SPEKTAR_NEW;Integrated Security=True");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.UseCollation("Latin1_General_CI_AS");

            modelBuilder.Entity<ordrub__>(entity =>
            {
                entity.HasKey(e => new { e.produktf, e.ord__ref, e.suborder, e.rbk__ref, e.gord_ref })
                    .HasName("ordrub_2");

                entity.HasIndex(e => e.ord__ref, "ordrub_3")
                    .HasFillFactor(90);

                entity.HasIndex(e => new { e.ord__ref, e.rbk__ref }, "ordrub_4")
                    .HasFillFactor(90);

                entity.HasIndex(e => new { e.produktf, e.ord__ref, e.rbk__ref, e.suborder, e.gord_ref }, "ordrubi7")
                    .IsUnique()
                    .HasFillFactor(90);

                entity.Property(e => e.produktf)
                    .HasMaxLength(1)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.ord__ref)
                    .HasMaxLength(6)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.suborder)
                    .HasMaxLength(3)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.rbk__ref)
                    .HasMaxLength(6)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.gord_ref)
                    .HasMaxLength(6)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.hlpgrpnk)
                    .HasMaxLength(1)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.rowid).ValueGeneratedOnAdd();
            });

            modelBuilder.Entity<rubrik__>(entity =>
            {
                entity.HasKey(e => e.rbk__ref)
                    .HasName("ref__rbk");

                entity.Property(e => e.rbk__ref)
                    .HasMaxLength(6)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.addhour_)
                    .HasMaxLength(1)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.geblokk_)
                    .HasMaxLength(1)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.grst__yn)
                    .HasMaxLength(1)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.kst__typ)
                    .HasMaxLength(1)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.oms_aant)
                    .HasMaxLength(15)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.oms_rbk_)
                    .HasMaxLength(100)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.pdok__yn)
                    .HasMaxLength(1)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.pr_ppu__)
                    .HasMaxLength(1)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.prest_yn)
                    .HasMaxLength(1)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.rbk__typ)
                    .HasMaxLength(1)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.rbk__vdt)
                    .HasMaxLength(30)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.rbk_hvdt)
                    .HasMaxLength(30)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.rbk_vdt3)
                    .HasMaxLength(30)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.rbk_vdt4)
                    .HasMaxLength(30)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.rowid).ValueGeneratedOnAdd();

                entity.Property(e => e.sto__ref)
                    .HasMaxLength(3)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.vkrbkref)
                    .HasMaxLength(2)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.voctonac)
                    .HasMaxLength(1)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.werkd_yn)
                    .HasMaxLength(1)
                    .HasDefaultValueSql("('')");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
