using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HourlyRate.Infrastructure.Spektar.Models
{
    public class order___
    {
        public string ord__ref { get; set; } = null!;
        public ordrub__ ordrub__ { get; set; } = null!;

        public string ord__rpn { get; set; } = null!;
        public string jobnr_vw { get; set; } = null!;
        public DateTime best_dat { get; set; }
        public string kla__ref { get; set; } = null!;
        public string kla__rpn { get; set; } = null!;
        public string klgr_ref { get; set; } = null!;
        public string knp__ref { get; set; } = null!;
        public string vrt__ref { get; set; } = null!;
        public double kommiss_ { get; set; }
        public string lev__ref { get; set; } = null!;
        public double kommiss2 { get; set; }
        public string ktrk_ref { get; set; } = null!;
        public string prd__ref { get; set; } = null!;
        public string kpn__srt { get; set; } = null!;
        public string afw__srt { get; set; } = null!;
        public string type_ord { get; set; } = null!;
        public string prkl_ref { get; set; } = null!;
        public string ean___nr { get; set; } = null!;
        public string omschr__ { get; set; } = null!;
        public double gesl_bre { get; set; }
        public double gesl_len { get; set; }
        public double rug_____ { get; set; }
        public double vouw____ { get; set; }
        public double vouw___2 { get; set; }
        public double open_bre { get; set; }
        public double open_len { get; set; }
        public double oplage__ { get; set; }
        public double vglopl__ { get; set; }
        public string wpafwref { get; set; } = null!;
        public string plts_kod { get; set; } = null!;
        public string lok__ref { get; set; } = null!;
        public string knplkref { get; set; } = null!;
        public string trn__ref { get; set; } = null!;
        public string trnt_ref { get; set; } = null!;
        public DateTime leverdat { get; set; }
        public string leveruur { get; set; } = null!;
        public string leverkod { get; set; } = null!;
        public string leverkom { get; set; } = null!;
        public string fsc__ref { get; set; } = null!;
        public string bsbn_kla { get; set; } = null!;
        public string lotnrkla { get; set; } = null!;
        public string refbykla { get; set; } = null!;
        public string flms_ref { get; set; } = null!;
        public string edi__idf { get; set; } = null!;
        public string spec_lev { get; set; } = null!;
        public string offa_ref { get; set; } = null!;
        public string bon__ref { get; set; } = null!;
        public string off__ref { get; set; } = null!;
        public DateTime off__dat { get; set; }
        public string vorigord { get; set; } = null!;
        public double voorz_bd { get; set; }
        public string krit___1 { get; set; } = null!;
        public string krit___2 { get; set; } = null!;
        public string kom__ref { get; set; } = null!;
        public string type_prd { get; set; } = null!;
        public string type__oa { get; set; } = null!;
        public string klassemt { get; set; } = null!;
        public DateTime vrij_dat { get; set; }
        public DateTime goed_drk { get; set; }
        public string goedudrk { get; set; } = null!;
        public DateTime goed_afw { get; set; }
        public string goeduafw { get; set; } = null!;
        public DateTime goed_lev { get; set; }
        public string goedulev { get; set; } = null!;
        public string plwy_ref { get; set; } = null!;
        public string pmd__ref { get; set; } = null!;
        public string pln_dt_0 { get; set; } = null!;
        public string pln_dt_1 { get; set; } = null!;
        public string pln_dt_2 { get; set; } = null!;
        public string pln_dt_3 { get; set; } = null!;
        public int pln_dd_0 { get; set; }
        public int pln_dd_1 { get; set; }
        public int pln_dd_2 { get; set; }
        public int pln_dd_3 { get; set; }
        public int levertyd { get; set; }
        public string fiat_prd { get; set; } = null!;
        public string blok_atl { get; set; } = null!;
        public string bloktxt1 { get; set; } = null!;
        public string bloktxt2 { get; set; } = null!;
        public DateTime blok_dat { get; set; }
        public string blok_usr { get; set; } = null!;
        public string bom___ok { get; set; } = null!;
        public DateTime bom__dat { get; set; }
        public string bom__usr { get; set; } = null!;
        public string bom__com { get; set; } = null!;
        public string boa___ok { get; set; } = null!;
        public DateTime boa__dat { get; set; }
        public string boa__usr { get; set; } = null!;
        public string boa__com { get; set; } = null!;
        public string toe_tmp_ { get; set; } = null!;
        public DateTime dat_tmp_ { get; set; }
        public double afgewrkt { get; set; }
        public string prodwijz { get; set; } = null!;
        public int bon__cnt { get; set; }
        public string ord_begl { get; set; } = null!;
        public string kalkulat { get; set; } = null!;
        public string int_cont { get; set; } = null!;
        public string faktur_1 { get; set; } = null!;
        public string faktur_2 { get; set; } = null!;
        public string oab__weg { get; set; } = null!;
        public string grdb_weg { get; set; } = null!;
        public string stbw_weg { get; set; } = null!;
        public string pntn_weg { get; set; } = null!;
        public string pdkn_weg { get; set; } = null!;
        public string plan_weg { get; set; } = null!;
        public string matb_weg { get; set; } = null!;
        public string levb_weg { get; set; } = null!;
        public string ofk__weg { get; set; } = null!;
        public string res__weg { get; set; } = null!;
        public string hfl__weg { get; set; } = null!;
        public string wkb__weg { get; set; } = null!;
        public string groepeer { get; set; } = null!;
        public string munt_ref { get; set; } = null!;
        public int aant_dec { get; set; }
        public double koers___ { get; set; }
        public string prys_srt { get; set; } = null!;
        public double oplag_vm { get; set; }
        public double oplag_bm { get; set; }
        public double oplag_om { get; set; }
        public double extra_vm { get; set; }
        public double extra_bm { get; set; }
        public double extra_om { get; set; }
        public string btw_____ { get; set; } = null!;
        public string arek_ref { get; set; } = null!;
        public string wp___ref { get; set; } = null!;
        public string ksrt_ref { get; set; } = null!;
        public string omsaant_ { get; set; } = null!;
        public string dgbk_ref { get; set; } = null!;
        public string dossier_ { get; set; } = null!;
        public string oplagtxt { get; set; } = null!;
        public string extratxt { get; set; } = null!;
        public string fkla_ref { get; set; } = null!;
        public string fknp_ref { get; set; } = null!;
        public string fmd__ref { get; set; } = null!;
        public string fac__typ { get; set; } = null!;
        public string fac__wyz { get; set; } = null!;
        public int n1______ { get; set; }
        public int n2______ { get; set; }
        public int n3______ { get; set; }
        public int n4______ { get; set; }
        public int n5______ { get; set; }
        public int n6______ { get; set; }
        public int n7______ { get; set; }
        public int n8______ { get; set; }
        public int n9______ { get; set; }
        public int n10_____ { get; set; }
        public int n11_____ { get; set; }
        public int n12_____ { get; set; }
        public int n13_____ { get; set; }
        public int n14_____ { get; set; }
        public int n15_____ { get; set; }
        public int n16_____ { get; set; }
        public DateTime dat01___ { get; set; }
        public DateTime dat02___ { get; set; }
        public DateTime dat03___ { get; set; }
        public DateTime dat04___ { get; set; }
        public DateTime dat05___ { get; set; }
        public string open____ { get; set; } = null!;
        public DateTime dat_open { get; set; }
        public string annul___ { get; set; } = null!;
        public DateTime datannul { get; set; }
        public string jobnrher { get; set; } = null!;
        public string jobnrvdl { get; set; } = null!;
        public string fin___ok { get; set; } = null!;
        public string fin__com { get; set; } = null!;
        public DateTime fin__dat { get; set; }
        public string fin__usr { get; set; } = null!;
        public DateTime kred_dat { get; set; }
        public string opvolgen { get; set; } = null!;
        public string cde___ap { get; set; } = null!;
        public string tstval01 { get; set; } = null!;
        public string tstval02 { get; set; } = null!;
        public string tstval03 { get; set; } = null!;
        public string tstval04 { get; set; } = null!;
        public string tstval05 { get; set; } = null!;
        public string tstval06 { get; set; } = null!;
        public string tstval07 { get; set; } = null!;
        public string tstval08 { get; set; } = null!;
        public string tstval09 { get; set; } = null!;
        public string tstval10 { get; set; } = null!;
        public string vraag_01 { get; set; } = null!;
        public string vraag_02 { get; set; } = null!;
        public string vraag_03 { get; set; } = null!;
        public string vraag_04 { get; set; } = null!;
        public string vraag_05 { get; set; } = null!;
        public string vraag_06 { get; set; } = null!;
        public string vraag_07 { get; set; } = null!;
        public string vraag_08 { get; set; } = null!;
        public string vraag_09 { get; set; } = null!;
        public string vraag_10 { get; set; } = null!;
        public string antw__01 { get; set; } = null!;
        public string antw__02 { get; set; } = null!;
        public string antw__03 { get; set; } = null!;
        public string antw__04 { get; set; } = null!;
        public string antw__05 { get; set; } = null!;
        public string antw__06 { get; set; } = null!;
        public string antw__07 { get; set; } = null!;
        public string antw__08 { get; set; } = null!;
        public string antw__09 { get; set; } = null!;
        public string antw__10 { get; set; } = null!;
        public string antw__11 { get; set; } = null!;
        public string antw__12 { get; set; } = null!;
        public string antw__13 { get; set; } = null!;
        public string antw__14 { get; set; } = null!;
        public string antw__15 { get; set; } = null!;
        public string antw__16 { get; set; } = null!;
        public string antw__17 { get; set; } = null!;
        public string antw__18 { get; set; } = null!;
        public string antw__19 { get; set; } = null!;
        public string antw__20 { get; set; } = null!;
        public string antw__21 { get; set; } = null!;
        public string antw__22 { get; set; } = null!;
        public string antw__23 { get; set; } = null!;
        public string antw__24 { get; set; } = null!;
        public string antw__25 { get; set; } = null!;
        public string antw__26 { get; set; } = null!;
        public string antw__27 { get; set; } = null!;
        public string antw__28 { get; set; } = null!;
        public string antw__29 { get; set; } = null!;
        public string antw__30 { get; set; } = null!;
        public string flok_ref { get; set; } = null!;
        public string flknpref { get; set; } = null!;
        public int rowid { get; set; }
        public string tstval11 { get; set; } = null!;
        public string tstval12 { get; set; } = null!;
        public string tstval13 { get; set; } = null!;
        public string tstval14 { get; set; } = null!;
        public string tstval15 { get; set; } = null!;
        public string tstval16 { get; set; } = null!;
        public string tstval17 { get; set; } = null!;
        public string tstval18 { get; set; } = null!;
        public string tstval19 { get; set; } = null!;
        public string tstval20 { get; set; } = null!;

    }
}
