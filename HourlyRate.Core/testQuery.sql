SELECT	*
		--,order___.*
		--,ordcum__.*
		--,klabas__.vrzm_kla
		--,klabas__.naam____
		--,ordcum__.prostand
		--,ordcum__.aantal__
		--,ordcum__.volledig
		--,ordcum__.lev__dat
		--,ordcum__.typ__rtb
		--,ordcum__.dok__dat
		--,ordcum__.gefakt__
		--,ordcum__.gefaktv6
		--,ordcum__.inventv6
		--,ordcum__.dgbk_ref
		--,ordcum__.bkj__ref
		--,ordcum__.fak__ref
		--,ordcum__.facafgv6
		--,ordcum__.aant_prd
FROM order___

JOIN ordcum__ ON order___.ord__ref = ordcum__.ord__ref
JOIN ordrub__ ON ordrub__.ord__ref = order___.ord__ref
JOIN rubrik__ ON ordrub__.rbk__ref = rubrik__.rbk__ref
JOIN klabas__ ON order___.kla__ref = klabas__.kla__ref
WHERE order___.dat_open >= '2022/12/01' AND order___.open____ = 'N' AND order___.ord__ref = 064275

select	 r.rbk__ref
		,r.oms_rbk_
		,r.rbk_vdt3
		,o.ord__ref
		,o.duur____
		,o.lonen___
		,o.machines
		,o.overhead
		,o.aantal__
		,o.papier__
		,o.grdvrb__
		,o.aant_pap
		,o.aant_grd
FROM ordrub__ AS o
JOIN rubrik__ AS r ON o.rbk__ref = r.rbk__ref
where ord__ref = '064275' 

select	 
		 o.ord__ref
		,r.rbk__ref
		,r.oms_rbk_
		,r.rbk_vdt3
		,o.duur____
		,o.lonen___
		,o.machines
		,o.overhead
		,o.aantal__
		,o.papier__
		,o.grdvrb__
		,o.aant_pap
		,o.aant_grd
		,v.*
FROM ordrub__ AS o
JOIN rubrik__ AS r ON o.rbk__ref = r.rbk__ref
JOIN v4kkst__ AS v ON o.rbk__ref = v.rbk__ref
where ord__ref = '064275' 