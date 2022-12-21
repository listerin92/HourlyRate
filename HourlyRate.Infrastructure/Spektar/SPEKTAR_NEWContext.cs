using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using HourlyRate.Infrastructure.Spektar.Models;

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

        public virtual DbSet<order___> order___ { get; set; } = null!;
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

            modelBuilder.Entity<order___>(entity =>
            {
                entity.HasKey(e => e.ord__ref)
                    .HasName("id_ord_1");

                entity.HasIndex(e => new { e.ord__rpn, e.ord__ref }, "id_ord_2")
                    .IsUnique()
                    .HasFillFactor(90);

                entity.HasIndex(e => new { e.open____, e.type_ord }, "id_ord_3")
                    .HasFillFactor(90);

                entity.HasIndex(e => e.off__ref, "id_ord_4")
                    .HasFillFactor(90);

                entity.HasIndex(e => e.bsbn_kla, "id_ord_5")
                    .HasFillFactor(90);

                entity.HasIndex(e => new { e.kla__ref, e.open____, e.kred_dat }, "id_ord_6")
                    .HasFillFactor(90);

                entity.HasIndex(e => e.best_dat, "id_ord_7")
                    .HasFillFactor(90);

                entity.HasIndex(e => new { e.toe_tmp_, e.type_ord, e.type__oa }, "id_ord_8")
                    .HasFillFactor(90);

                entity.HasIndex(e => e.ktrk_ref, "id_ord_9")
                    .HasFillFactor(90);

                entity.Property(e => e.ord__ref)
                    .HasMaxLength(6)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.afw__srt)
                    .HasMaxLength(1)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.annul___)
                    .HasMaxLength(1)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.antw__01)
                    .HasMaxLength(6)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.antw__02)
                    .HasMaxLength(6)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.antw__03)
                    .HasMaxLength(6)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.antw__04)
                    .HasMaxLength(6)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.antw__05)
                    .HasMaxLength(6)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.antw__06)
                    .HasMaxLength(6)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.antw__07)
                    .HasMaxLength(6)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.antw__08)
                    .HasMaxLength(6)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.antw__09)
                    .HasMaxLength(6)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.antw__10)
                    .HasMaxLength(6)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.antw__11)
                    .HasMaxLength(6)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.antw__12)
                    .HasMaxLength(6)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.antw__13)
                    .HasMaxLength(6)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.antw__14)
                    .HasMaxLength(6)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.antw__15)
                    .HasMaxLength(6)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.antw__16)
                    .HasMaxLength(6)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.antw__17)
                    .HasMaxLength(6)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.antw__18)
                    .HasMaxLength(6)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.antw__19)
                    .HasMaxLength(6)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.antw__20)
                    .HasMaxLength(6)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.antw__21)
                    .HasMaxLength(6)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.antw__22)
                    .HasMaxLength(6)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.antw__23)
                    .HasMaxLength(6)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.antw__24)
                    .HasMaxLength(6)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.antw__25)
                    .HasMaxLength(6)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.antw__26)
                    .HasMaxLength(6)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.antw__27)
                    .HasMaxLength(6)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.antw__28)
                    .HasMaxLength(6)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.antw__29)
                    .HasMaxLength(6)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.antw__30)
                    .HasMaxLength(6)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.arek_ref)
                    .HasMaxLength(6)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.best_dat)
                    .HasColumnType("date")
                    .HasDefaultValueSql("(CONVERT([date],'19800101',(112)))");

                entity.Property(e => e.blok_atl)
                    .HasMaxLength(1)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.blok_dat)
                    .HasColumnType("date")
                    .HasDefaultValueSql("(CONVERT([date],'19800101',(112)))");

                entity.Property(e => e.blok_usr)
                    .HasMaxLength(25)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.bloktxt1)
                    .HasMaxLength(40)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.bloktxt2)
                    .HasMaxLength(40)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.boa___ok)
                    .HasMaxLength(1)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.boa__com)
                    .HasMaxLength(40)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.boa__dat)
                    .HasColumnType("date")
                    .HasDefaultValueSql("(CONVERT([date],'19800101',(112)))");

                entity.Property(e => e.boa__usr)
                    .HasMaxLength(25)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.bom___ok)
                    .HasMaxLength(1)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.bom__com)
                    .HasMaxLength(40)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.bom__dat)
                    .HasColumnType("date")
                    .HasDefaultValueSql("(CONVERT([date],'19800101',(112)))");

                entity.Property(e => e.bom__usr)
                    .HasMaxLength(25)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.bon__ref)
                    .HasMaxLength(6)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.bsbn_kla)
                    .HasMaxLength(25)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.btw_____)
                    .HasMaxLength(1)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.cde___ap)
                    .HasMaxLength(16)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.dat01___)
                    .HasColumnType("date")
                    .HasDefaultValueSql("(CONVERT([date],'19800101',(112)))");

                entity.Property(e => e.dat02___)
                    .HasColumnType("date")
                    .HasDefaultValueSql("(CONVERT([date],'19800101',(112)))");

                entity.Property(e => e.dat03___)
                    .HasColumnType("date")
                    .HasDefaultValueSql("(CONVERT([date],'19800101',(112)))");

                entity.Property(e => e.dat04___)
                    .HasColumnType("date")
                    .HasDefaultValueSql("(CONVERT([date],'19800101',(112)))");

                entity.Property(e => e.dat05___)
                    .HasColumnType("date")
                    .HasDefaultValueSql("(CONVERT([date],'19800101',(112)))");

                entity.Property(e => e.dat_open)
                    .HasColumnType("date")
                    .HasDefaultValueSql("(CONVERT([date],'19800101',(112)))");

                entity.Property(e => e.dat_tmp_)
                    .HasColumnType("date")
                    .HasDefaultValueSql("(CONVERT([date],'19800101',(112)))");

                entity.Property(e => e.datannul)
                    .HasColumnType("date")
                    .HasDefaultValueSql("(CONVERT([date],'19800101',(112)))");

                entity.Property(e => e.dgbk_ref)
                    .HasMaxLength(3)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.dossier_)
                    .HasMaxLength(2)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.ean___nr)
                    .HasMaxLength(25)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.edi__idf)
                    .HasMaxLength(14)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.extratxt)
                    .HasMaxLength(40)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.fac__typ)
                    .HasMaxLength(1)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.fac__wyz)
                    .HasMaxLength(1)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.faktur_1)
                    .HasMaxLength(30)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.faktur_2)
                    .HasMaxLength(30)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.fiat_prd)
                    .HasMaxLength(1)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.fin___ok)
                    .HasMaxLength(1)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.fin__com)
                    .HasMaxLength(100)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.fin__dat)
                    .HasColumnType("date")
                    .HasDefaultValueSql("(CONVERT([date],'19800101',(112)))");

                entity.Property(e => e.fin__usr)
                    .HasMaxLength(25)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.fkla_ref)
                    .HasMaxLength(6)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.fknp_ref)
                    .HasMaxLength(3)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.flknpref)
                    .HasMaxLength(3)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.flms_ref)
                    .HasMaxLength(3)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.flok_ref)
                    .HasMaxLength(6)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.fmd__ref)
                    .HasMaxLength(6)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.fsc__ref)
                    .HasMaxLength(20)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.goed_afw)
                    .HasColumnType("date")
                    .HasDefaultValueSql("(CONVERT([date],'19800101',(112)))");

                entity.Property(e => e.goed_drk)
                    .HasColumnType("date")
                    .HasDefaultValueSql("(CONVERT([date],'19800101',(112)))");

                entity.Property(e => e.goed_lev)
                    .HasColumnType("date")
                    .HasDefaultValueSql("(CONVERT([date],'19800101',(112)))");

                entity.Property(e => e.goeduafw)
                    .HasMaxLength(6)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.goedudrk)
                    .HasMaxLength(6)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.goedulev)
                    .HasMaxLength(6)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.grdb_weg)
                    .HasMaxLength(1)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.groepeer)
                    .HasMaxLength(1)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.hfl__weg)
                    .HasMaxLength(1)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.int_cont)
                    .HasMaxLength(25)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.jobnr_vw)
                    .HasMaxLength(10)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.jobnrher)
                    .HasMaxLength(10)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.jobnrvdl)
                    .HasMaxLength(10)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.kalkulat)
                    .HasMaxLength(25)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.kla__ref)
                    .HasMaxLength(6)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.kla__rpn)
                    .HasMaxLength(10)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.klassemt)
                    .HasMaxLength(20)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.klgr_ref)
                    .HasMaxLength(12)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.knp__ref)
                    .HasMaxLength(3)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.knplkref)
                    .HasMaxLength(3)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.kom__ref)
                    .HasMaxLength(3)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.kpn__srt)
                    .HasMaxLength(1)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.kred_dat)
                    .HasColumnType("date")
                    .HasDefaultValueSql("(CONVERT([date],'19800101',(112)))");

                entity.Property(e => e.krit___1)
                    .HasMaxLength(20)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.krit___2)
                    .HasMaxLength(20)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.ksrt_ref)
                    .HasMaxLength(3)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.ktrk_ref)
                    .HasMaxLength(6)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.lev__ref)
                    .HasMaxLength(6)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.levb_weg)
                    .HasMaxLength(1)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.leverdat)
                    .HasColumnType("date")
                    .HasDefaultValueSql("(CONVERT([date],'19800101',(112)))");

                entity.Property(e => e.leverkod)
                    .HasMaxLength(1)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.leverkom)
                    .HasMaxLength(4000)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.leveruur)
                    .HasMaxLength(6)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.lok__ref)
                    .HasMaxLength(6)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.lotnrkla)
                    .HasMaxLength(50)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.matb_weg)
                    .HasMaxLength(1)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.munt_ref)
                    .HasMaxLength(3)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.oab__weg)
                    .HasMaxLength(1)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.off__dat)
                    .HasColumnType("date")
                    .HasDefaultValueSql("(CONVERT([date],'19800101',(112)))");

                entity.Property(e => e.off__ref)
                    .HasMaxLength(6)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.offa_ref)
                    .HasMaxLength(6)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.ofk__weg)
                    .HasMaxLength(1)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.omsaant_)
                    .HasMaxLength(20)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.omschr__)
                    .HasMaxLength(50)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.open____)
                    .HasMaxLength(1)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.oplagtxt)
                    .HasMaxLength(40)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.opvolgen)
                    .HasMaxLength(1)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.ord__rpn)
                    .HasMaxLength(10)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.ord_begl)
                    .HasMaxLength(25)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.pdkn_weg)
                    .HasMaxLength(1)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.plan_weg)
                    .HasMaxLength(1)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.pln_dt_0)
                    .HasMaxLength(6)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.pln_dt_1)
                    .HasMaxLength(6)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.pln_dt_2)
                    .HasMaxLength(6)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.pln_dt_3)
                    .HasMaxLength(6)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.plts_kod)
                    .HasMaxLength(1)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.plwy_ref)
                    .HasMaxLength(3)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.pmd__ref)
                    .HasMaxLength(15)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.pntn_weg)
                    .HasMaxLength(1)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.prd__ref)
                    .HasMaxLength(10)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.prkl_ref)
                    .HasMaxLength(12)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.prodwijz)
                    .HasMaxLength(1)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.prys_srt)
                    .HasMaxLength(1)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.refbykla)
                    .HasMaxLength(40)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.res__weg)
                    .HasMaxLength(1)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.rowid).ValueGeneratedOnAdd();

                entity.Property(e => e.spec_lev)
                    .HasMaxLength(25)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.stbw_weg)
                    .HasMaxLength(1)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.toe_tmp_)
                    .HasMaxLength(1)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.trn__ref)
                    .HasMaxLength(3)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.trnt_ref)
                    .HasMaxLength(3)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.tstval01)
                    .HasMaxLength(4)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.tstval02)
                    .HasMaxLength(4)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.tstval03)
                    .HasMaxLength(4)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.tstval04)
                    .HasMaxLength(4)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.tstval05)
                    .HasMaxLength(4)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.tstval06)
                    .HasMaxLength(4)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.tstval07)
                    .HasMaxLength(4)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.tstval08)
                    .HasMaxLength(4)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.tstval09)
                    .HasMaxLength(4)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.tstval10)
                    .HasMaxLength(4)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.tstval11)
                    .HasMaxLength(4)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.tstval12)
                    .HasMaxLength(4)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.tstval13)
                    .HasMaxLength(4)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.tstval14)
                    .HasMaxLength(4)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.tstval15)
                    .HasMaxLength(4)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.tstval16)
                    .HasMaxLength(4)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.tstval17)
                    .HasMaxLength(4)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.tstval18)
                    .HasMaxLength(4)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.tstval19)
                    .HasMaxLength(4)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.tstval20)
                    .HasMaxLength(4)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.type__oa)
                    .HasMaxLength(1)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.type_ord)
                    .HasMaxLength(1)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.type_prd)
                    .HasMaxLength(1)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.vorigord)
                    .HasMaxLength(6)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.vraag_01)
                    .HasMaxLength(6)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.vraag_02)
                    .HasMaxLength(6)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.vraag_03)
                    .HasMaxLength(6)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.vraag_04)
                    .HasMaxLength(6)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.vraag_05)
                    .HasMaxLength(6)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.vraag_06)
                    .HasMaxLength(6)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.vraag_07)
                    .HasMaxLength(6)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.vraag_08)
                    .HasMaxLength(6)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.vraag_09)
                    .HasMaxLength(6)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.vraag_10)
                    .HasMaxLength(6)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.vrij_dat)
                    .HasColumnType("date")
                    .HasDefaultValueSql("(CONVERT([date],'19800101',(112)))");

                entity.Property(e => e.vrt__ref)
                    .HasMaxLength(6)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.wkb__weg)
                    .HasMaxLength(1)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.wp___ref)
                    .HasMaxLength(4)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.wpafwref)
                    .HasMaxLength(4)
                    .HasDefaultValueSql("('')");
            });

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
