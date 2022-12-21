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

SELECT	 
		 --o.ord__ref AS JonNumber
		r.rbk__ref AS CostCategoryId
		--,r.rbk_vdt3 AS 'Cost Category Type'
		,r.oms_rbk_ AS 'Cost Category'
		,SUM(o.duur____) AS duration
		,SUM(o.lonen___) AS Wages
		,SUM(o.machines) AS Machines
		,SUM(o.overhead) AS Overhead
		,SUM(o.lonen___ + o.machines + o.overhead) AS Total
		--,o.aantal__ AS Quantity
		--,o.papier__ AS Paper
		--,o.grdvrb__ AS MaterialCost
		--,o.aant_pap AS QuantityPaper
		--,o.aant_grd AS QuantityMaterial
		
FROM ordrub__ AS o
JOIN rubrik__ AS r ON o.rbk__ref = r.rbk__ref
JOIN order___ AS ord ON ord.ord__ref = o.ord__ref
--JOIN v4kkst__ AS v ON o.rbk__ref = v.rbk__ref
--where ord__ref = '064275' 
WHERE ord.dat_open >= '2022/12/01' AND ord.open____ = 'N' AND(ordrub__.lonen___ != 0 AND ordrub__.machines !=0 AND ordrub__.overhead !=0)
GROUP BY r.oms_rbk_, r.rbk__ref
ORDER BY CostCategoryId