
update "BuffonPosto" set "bPostoBB" = '1' where "nuFilial" in ('7', '12', '32', '37', '38', '56', '57', '59', '61', '80', '90', '91');

TRUNCATE TABLE public."BuffonDistPostoComb" RESTART IDENTITY;

-------------------------------------------
-- IPIRANGA (1)
-------------------------------------------

-- ------------------------------------------------
-- IPIRANGA / RGRANDE
-- 113
INSERT INTO public."BuffonDistPostoComb" ("fk_id_scrape_posto", "fk_id_scrape_base", "fk_id_scrape_dist", "bEtanol", "bGasComum", "bGasAdit", "bDieselS500", "bDieselS500Aditivado", "bDieselS10", "bDieselS10Adit", "bGasolinaPremium") SELECT 131,4,1,'0','1','1','1','0','1','0','0';
---- 21
INSERT INTO public."BuffonDistPostoComb" ("fk_id_scrape_posto", "fk_id_scrape_base", "fk_id_scrape_dist", "bEtanol", "bGasComum", "bGasAdit", "bDieselS500", "bDieselS500Aditivado", "bDieselS10", "bDieselS10Adit", "bGasolinaPremium") SELECT 18,4,1,'0','1','1','1','0','1','0','0';
---- 29
INSERT INTO public."BuffonDistPostoComb" ("fk_id_scrape_posto", "fk_id_scrape_base", "fk_id_scrape_dist", "bEtanol", "bGasComum", "bGasAdit", "bDieselS500", "bDieselS500Aditivado", "bDieselS10", "bDieselS10Adit", "bGasolinaPremium") SELECT 22,4,1,'1','1','1','1','0','1','1','0';
---- 40
INSERT INTO public."BuffonDistPostoComb" ("fk_id_scrape_posto", "fk_id_scrape_base", "fk_id_scrape_dist", "bEtanol", "bGasComum", "bGasAdit", "bDieselS500", "bDieselS500Aditivado", "bDieselS10", "bDieselS10Adit", "bGasolinaPremium") SELECT 27,4,1,'1','1','1','1','0','1','0','0';
---- 54;
INSERT INTO public."BuffonDistPostoComb" ("fk_id_scrape_posto", "fk_id_scrape_base", "fk_id_scrape_dist", "bEtanol", "bGasComum", "bGasAdit", "bDieselS500", "bDieselS500Aditivado", "bDieselS10", "bDieselS10Adit", "bGasolinaPremium") SELECT 32,4,1,'1','1','1','1','0','1','0','0';
---- 63
INSERT INTO public."BuffonDistPostoComb" ("fk_id_scrape_posto", "fk_id_scrape_base", "fk_id_scrape_dist", "bEtanol", "bGasComum", "bGasAdit", "bDieselS500", "bDieselS500Aditivado", "bDieselS10", "bDieselS10Adit", "bGasolinaPremium") SELECT 33,4,1,'0','1','1','1','0','1','0','0';
---- 67
INSERT INTO public."BuffonDistPostoComb" ("fk_id_scrape_posto", "fk_id_scrape_base", "fk_id_scrape_dist", "bEtanol", "bGasComum", "bGasAdit", "bDieselS500", "bDieselS500Aditivado", "bDieselS10", "bDieselS10Adit", "bGasolinaPremium") SELECT 34,4,1,'1','1','1','1','0','1','1','0';
---- 68
INSERT INTO public."BuffonDistPostoComb" ("fk_id_scrape_posto", "fk_id_scrape_base", "fk_id_scrape_dist", "bEtanol", "bGasComum", "bGasAdit", "bDieselS500", "bDieselS500Aditivado", "bDieselS10", "bDieselS10Adit", "bGasolinaPremium") SELECT 35,4,1,'1','1','1','0','0','1','1','0';
---- 69
INSERT INTO public."BuffonDistPostoComb" ("fk_id_scrape_posto", "fk_id_scrape_base", "fk_id_scrape_dist", "bEtanol", "bGasComum", "bGasAdit", "bDieselS500", "bDieselS500Aditivado", "bDieselS10", "bDieselS10Adit", "bGasolinaPremium") SELECT 36,4,1,'0','1','1','0','0','1','0','0';
---- BB
---- 7
INSERT INTO public."BuffonDistPostoComb" ("fk_id_scrape_posto", "fk_id_scrape_base", "fk_id_scrape_dist", "bEtanol", "bGasComum", "bGasAdit", "bDieselS500", "bDieselS500Aditivado", "bDieselS10", "bDieselS10Adit", "bGasolinaPremium") SELECT 1,4,1,'1','1','1','0','0','0','0','0';
---- 12
INSERT INTO public."BuffonDistPostoComb" ("fk_id_scrape_posto", "fk_id_scrape_base", "fk_id_scrape_dist", "bEtanol", "bGasComum", "bGasAdit", "bDieselS500", "bDieselS500Aditivado", "bDieselS10", "bDieselS10Adit", "bGasolinaPremium") SELECT 2,4,1,'0','1','1','0','0','0','0','0';
---- 59
INSERT INTO public."BuffonDistPostoComb" ("fk_id_scrape_posto", "fk_id_scrape_base", "fk_id_scrape_dist", "bEtanol", "bGasComum", "bGasAdit", "bDieselS500", "bDieselS500Aditivado", "bDieselS10", "bDieselS10Adit", "bGasolinaPremium") SELECT 7,4,1,'0','1','1','1','0','1','0','0';
---- 91
INSERT INTO public."BuffonDistPostoComb" ("fk_id_scrape_posto", "fk_id_scrape_base", "fk_id_scrape_dist", "bEtanol", "bGasComum", "bGasAdit", "bDieselS500", "bDieselS500Aditivado", "bDieselS10", "bDieselS10Adit", "bGasolinaPremium") SELECT 11,4,1,'0','1','1','1','0','1','0','0';

---- ------------------------------------------------
---- IPIRANGA / PASSO FUNDO
---- 84
INSERT INTO public."BuffonDistPostoComb" ("fk_id_scrape_posto", "fk_id_scrape_base", "fk_id_scrape_dist", "bEtanol", "bGasComum", "bGasAdit", "bDieselS500", "bDieselS500Aditivado", "bDieselS10", "bDieselS10Adit", "bGasolinaPremium") SELECT 41,5,1,'0','1','1','1','0','1','0','0';
---- 25 
INSERT INTO public."BuffonDistPostoComb" ("fk_id_scrape_posto", "fk_id_scrape_base", "fk_id_scrape_dist", "bEtanol", "bGasComum", "bGasAdit", "bDieselS500", "bDieselS500Aditivado", "bDieselS10", "bDieselS10Adit", "bGasolinaPremium") SELECT 20,5,1,'1','1','1','1','0','1','0','0';
---- 28 
INSERT INTO public."BuffonDistPostoComb" ("fk_id_scrape_posto", "fk_id_scrape_base", "fk_id_scrape_dist", "bEtanol", "bGasComum", "bGasAdit", "bDieselS500", "bDieselS500Aditivado", "bDieselS10", "bDieselS10Adit", "bGasolinaPremium") SELECT 21,5,1,'1','1','1','1','0','1','0','0';
---- 74 
INSERT INTO public."BuffonDistPostoComb" ("fk_id_scrape_posto", "fk_id_scrape_base", "fk_id_scrape_dist", "bEtanol", "bGasComum", "bGasAdit", "bDieselS500", "bDieselS500Aditivado", "bDieselS10", "bDieselS10Adit", "bGasolinaPremium") SELECT 39,5,1,'0','1','1','1','0','1','0','0';
---- 76 
INSERT INTO public."BuffonDistPostoComb" ("fk_id_scrape_posto", "fk_id_scrape_base", "fk_id_scrape_dist", "bEtanol", "bGasComum", "bGasAdit", "bDieselS500", "bDieselS500Aditivado", "bDieselS10", "bDieselS10Adit", "bGasolinaPremium") SELECT 40,5,1,'0','1','1','1','0','1','0','0';
---- 103 
INSERT INTO public."BuffonDistPostoComb" ("fk_id_scrape_posto", "fk_id_scrape_base", "fk_id_scrape_dist", "bEtanol", "bGasComum", "bGasAdit", "bDieselS500", "bDieselS500Aditivado", "bDieselS10", "bDieselS10Adit", "bGasolinaPremium") SELECT 47,5,1,'0','1','1','1','0','1','0','0';

---- ------------------------------------------------
---- IPIRANGA / STA CAT
---- 88 
INSERT INTO public."BuffonDistPostoComb" ("fk_id_scrape_posto", "fk_id_scrape_base", "fk_id_scrape_dist", "bEtanol", "bGasComum", "bGasAdit", "bDieselS500", "bDieselS500Aditivado", "bDieselS10", "bDieselS10Adit", "bGasolinaPremium") SELECT 42,6,1,'0','1','1','1','0','1','0','0';

---- ------------------------------------------------
---- IPIRANGA / ARAUCARIA
---- 88 
INSERT INTO public."BuffonDistPostoComb" ("fk_id_scrape_posto", "fk_id_scrape_base", "fk_id_scrape_dist", "bEtanol", "bGasComum", "bGasAdit", "bDieselS500", "bDieselS500Aditivado", "bDieselS10", "bDieselS10Adit", "bGasolinaPremium") SELECT 42,1,1,'0','1','1','1','0','1','0','0';

---- ------------------------------------------------
---- IPIRANGA / STA MARIA
---- 23 
INSERT INTO public."BuffonDistPostoComb" ("fk_id_scrape_posto", "fk_id_scrape_base", "fk_id_scrape_dist", "bEtanol", "bGasComum", "bGasAdit", "bDieselS500", "bDieselS500Aditivado", "bDieselS10", "bDieselS10Adit", "bGasolinaPremium") SELECT 19,2,1,'0','1','1','1','0','1','0','0';
---- 31
INSERT INTO public."BuffonDistPostoComb" ("fk_id_scrape_posto", "fk_id_scrape_base", "fk_id_scrape_dist", "bEtanol", "bGasComum", "bGasAdit", "bDieselS500", "bDieselS500Aditivado", "bDieselS10", "bDieselS10Adit", "bGasolinaPremium") SELECT 23,2,1,'1','1','1','1','0','1','0','0';
---- 92 
INSERT INTO public."BuffonDistPostoComb" ("fk_id_scrape_posto", "fk_id_scrape_base", "fk_id_scrape_dist", "bEtanol", "bGasComum", "bGasAdit", "bDieselS500", "bDieselS500Aditivado", "bDieselS10", "bDieselS10Adit", "bGasolinaPremium") SELECT 43,2,1,'0','1','1','1','0','1','0','0';
---- 93 
INSERT INTO public."BuffonDistPostoComb" ("fk_id_scrape_posto", "fk_id_scrape_base", "fk_id_scrape_dist", "bEtanol", "bGasComum", "bGasAdit", "bDieselS500", "bDieselS500Aditivado", "bDieselS10", "bDieselS10Adit", "bGasolinaPremium") SELECT 44,2,1,'0','1','1','1','0','1','0','0';
---- 94 
INSERT INTO public."BuffonDistPostoComb" ("fk_id_scrape_posto", "fk_id_scrape_base", "fk_id_scrape_dist", "bEtanol", "bGasComum", "bGasAdit", "bDieselS500", "bDieselS500Aditivado", "bDieselS10", "bDieselS10Adit", "bGasolinaPremium") SELECT 45,2,1,'0','1','1','1','0','1','0','0';

---- BB
---- 32
INSERT INTO public."BuffonDistPostoComb" ("fk_id_scrape_posto", "fk_id_scrape_base", "fk_id_scrape_dist", "bEtanol", "bGasComum", "bGasAdit", "bDieselS500", "bDieselS500Aditivado", "bDieselS10", "bDieselS10Adit", "bGasolinaPremium") SELECT 137,2,1,'1','1','1','1','0','1','0','0';

---- ------------------------------------------------
---- IPIRANGA / CANOAS
---- 3
INSERT INTO public."BuffonDistPostoComb" ("fk_id_scrape_posto", "fk_id_scrape_base", "fk_id_scrape_dist", "bEtanol", "bGasComum", "bGasAdit", "bDieselS500", "bDieselS500Aditivado", "bDieselS10", "bDieselS10Adit", "bGasolinaPremium") SELECT 12,3,1,'1','1','1','1','0','1','0','0';
---- 5
INSERT INTO public."BuffonDistPostoComb" ("fk_id_scrape_posto", "fk_id_scrape_base", "fk_id_scrape_dist", "bEtanol", "bGasComum", "bGasAdit", "bDieselS500", "bDieselS500Aditivado", "bDieselS10", "bDieselS10Adit", "bGasolinaPremium") SELECT 13,3,1,'1','1','1','1','0','1','0','0';
---- 6
INSERT INTO public."BuffonDistPostoComb" ("fk_id_scrape_posto", "fk_id_scrape_base", "fk_id_scrape_dist", "bEtanol", "bGasComum", "bGasAdit", "bDieselS500", "bDieselS500Aditivado", "bDieselS10", "bDieselS10Adit", "bGasolinaPremium") SELECT 14,3,1,'0','1','1','1','0','1','0','0';
---- 9
INSERT INTO public."BuffonDistPostoComb" ("fk_id_scrape_posto", "fk_id_scrape_base", "fk_id_scrape_dist", "bEtanol", "bGasComum", "bGasAdit", "bDieselS500", "bDieselS500Aditivado", "bDieselS10", "bDieselS10Adit", "bGasolinaPremium") SELECT 15,3,1,'0','1','1','1','0','1','0','0';
---- 11
INSERT INTO public."BuffonDistPostoComb" ("fk_id_scrape_posto", "fk_id_scrape_base", "fk_id_scrape_dist", "bEtanol", "bGasComum", "bGasAdit", "bDieselS500", "bDieselS500Aditivado", "bDieselS10", "bDieselS10Adit", "bGasolinaPremium") SELECT 16,3,1,'1','1','1','1','0','1','0','0';
---- 14
INSERT INTO public."BuffonDistPostoComb" ("fk_id_scrape_posto", "fk_id_scrape_base", "fk_id_scrape_dist", "bEtanol", "bGasComum", "bGasAdit", "bDieselS500", "bDieselS500Aditivado", "bDieselS10", "bDieselS10Adit", "bGasolinaPremium") SELECT 17,3,1,'0','1','1','1','0','1','0','0';
---- 33
INSERT INTO public."BuffonDistPostoComb" ("fk_id_scrape_posto", "fk_id_scrape_base", "fk_id_scrape_dist", "bEtanol", "bGasComum", "bGasAdit", "bDieselS500", "bDieselS500Aditivado", "bDieselS10", "bDieselS10Adit", "bGasolinaPremium") SELECT 24,3,1,'1','1','1','1','0','1','0','0';
---- 36
INSERT INTO public."BuffonDistPostoComb" ("fk_id_scrape_posto", "fk_id_scrape_base", "fk_id_scrape_dist", "bEtanol", "bGasComum", "bGasAdit", "bDieselS500", "bDieselS500Aditivado", "bDieselS10", "bDieselS10Adit", "bGasolinaPremium") SELECT 25,3,1,'0','1','1','1','0','1','0','0';
---- 39
INSERT INTO public."BuffonDistPostoComb" ("fk_id_scrape_posto", "fk_id_scrape_base", "fk_id_scrape_dist", "bEtanol", "bGasComum", "bGasAdit", "bDieselS500", "bDieselS500Aditivado", "bDieselS10", "bDieselS10Adit", "bGasolinaPremium") SELECT 26,3,1,'0','1','1','1','0','1','0','0';
---- 41
INSERT INTO public."BuffonDistPostoComb" ("fk_id_scrape_posto", "fk_id_scrape_base", "fk_id_scrape_dist", "bEtanol", "bGasComum", "bGasAdit", "bDieselS500", "bDieselS500Aditivado", "bDieselS10", "bDieselS10Adit", "bGasolinaPremium") SELECT 28,3,1,'1','1','1','0','0','1','0','1';
---- 42
INSERT INTO public."BuffonDistPostoComb" ("fk_id_scrape_posto", "fk_id_scrape_base", "fk_id_scrape_dist", "bEtanol", "bGasComum", "bGasAdit", "bDieselS500", "bDieselS500Aditivado", "bDieselS10", "bDieselS10Adit", "bGasolinaPremium") SELECT 29,3,1,'1','1','1','1','0','1','0','0';
---- 43
INSERT INTO public."BuffonDistPostoComb" ("fk_id_scrape_posto", "fk_id_scrape_base", "fk_id_scrape_dist", "bEtanol", "bGasComum", "bGasAdit", "bDieselS500", "bDieselS500Aditivado", "bDieselS10", "bDieselS10Adit", "bGasolinaPremium") SELECT 30,3,1,'0','1','1','0','0','1','0','0';
---- 44
INSERT INTO public."BuffonDistPostoComb" ("fk_id_scrape_posto", "fk_id_scrape_base", "fk_id_scrape_dist", "bEtanol", "bGasComum", "bGasAdit", "bDieselS500", "bDieselS500Aditivado", "bDieselS10", "bDieselS10Adit", "bGasolinaPremium") SELECT 31,3,1,'1','1','1','1','0','1','0','0';
---- 70
INSERT INTO public."BuffonDistPostoComb" ("fk_id_scrape_posto", "fk_id_scrape_base", "fk_id_scrape_dist", "bEtanol", "bGasComum", "bGasAdit", "bDieselS500", "bDieselS500Aditivado", "bDieselS10", "bDieselS10Adit", "bGasolinaPremium") SELECT 37,3,1,'0','1','1','0','0','1','0','0';
---- 71
INSERT INTO public."BuffonDistPostoComb" ("fk_id_scrape_posto", "fk_id_scrape_base", "fk_id_scrape_dist", "bEtanol", "bGasComum", "bGasAdit", "bDieselS500", "bDieselS500Aditivado", "bDieselS10", "bDieselS10Adit", "bGasolinaPremium") SELECT 38,3,1,'0','1','1','0','0','1','0','0';
---- 99
INSERT INTO public."BuffonDistPostoComb" ("fk_id_scrape_posto", "fk_id_scrape_base", "fk_id_scrape_dist", "bEtanol", "bGasComum", "bGasAdit", "bDieselS500", "bDieselS500Aditivado", "bDieselS10", "bDieselS10Adit", "bGasolinaPremium") SELECT 46,3,1,'1','1','1','0','0','0','1','1';
---- 108
INSERT INTO public."BuffonDistPostoComb" ("fk_id_scrape_posto", "fk_id_scrape_base", "fk_id_scrape_dist", "bEtanol", "bGasComum", "bGasAdit", "bDieselS500", "bDieselS500Aditivado", "bDieselS10", "bDieselS10Adit", "bGasolinaPremium") SELECT 48,3,1,'0','1','1','1','0','1','0','0';
---- 29
INSERT INTO public."BuffonDistPostoComb" ("fk_id_scrape_posto", "fk_id_scrape_base", "fk_id_scrape_dist", "bEtanol", "bGasComum", "bGasAdit", "bDieselS500", "bDieselS500Aditivado", "bDieselS10", "bDieselS10Adit", "bGasolinaPremium") SELECT 22,3,1,'1','1','1','1','0','1','0','1';

---- 21
INSERT INTO public."BuffonDistPostoComb" ("fk_id_scrape_posto", "fk_id_scrape_base", "fk_id_scrape_dist", "bEtanol", "bGasComum", "bGasAdit", "bDieselS500", "bDieselS500Aditivado", "bDieselS10", "bDieselS10Adit", "bGasolinaPremium") SELECT 18,3,1,'1','1','1','1','0','1','0','1';
---- 40
INSERT INTO public."BuffonDistPostoComb" ("fk_id_scrape_posto", "fk_id_scrape_base", "fk_id_scrape_dist", "bEtanol", "bGasComum", "bGasAdit", "bDieselS500", "bDieselS500Aditivado", "bDieselS10", "bDieselS10Adit", "bGasolinaPremium") SELECT 27,3,1,'1','1','1','1','0','1','0','1';
---- 54
INSERT INTO public."BuffonDistPostoComb" ("fk_id_scrape_posto", "fk_id_scrape_base", "fk_id_scrape_dist", "bEtanol", "bGasComum", "bGasAdit", "bDieselS500", "bDieselS500Aditivado", "bDieselS10", "bDieselS10Adit", "bGasolinaPremium") SELECT 32,3,1,'1','1','1','1','0','1','0','1';
---- 63
INSERT INTO public."BuffonDistPostoComb" ("fk_id_scrape_posto", "fk_id_scrape_base", "fk_id_scrape_dist", "bEtanol", "bGasComum", "bGasAdit", "bDieselS500", "bDieselS500Aditivado", "bDieselS10", "bDieselS10Adit", "bGasolinaPremium") SELECT 33,3,1,'1','1','1','1','0','1','0','1';
---- 67
INSERT INTO public."BuffonDistPostoComb" ("fk_id_scrape_posto", "fk_id_scrape_base", "fk_id_scrape_dist", "bEtanol", "bGasComum", "bGasAdit", "bDieselS500", "bDieselS500Aditivado", "bDieselS10", "bDieselS10Adit", "bGasolinaPremium") SELECT 34,3,1,'1','1','1','1','0','1','0','1';
---- 68
INSERT INTO public."BuffonDistPostoComb" ("fk_id_scrape_posto", "fk_id_scrape_base", "fk_id_scrape_dist", "bEtanol", "bGasComum", "bGasAdit", "bDieselS500", "bDieselS500Aditivado", "bDieselS10", "bDieselS10Adit", "bGasolinaPremium") SELECT 35,3,1,'1','1','1','1','0','1','0','1';
---- 69
INSERT INTO public."BuffonDistPostoComb" ("fk_id_scrape_posto", "fk_id_scrape_base", "fk_id_scrape_dist", "bEtanol", "bGasComum", "bGasAdit", "bDieselS500", "bDieselS500Aditivado", "bDieselS10", "bDieselS10Adit", "bGasolinaPremium") SELECT 36,3,1,'1','1','1','1','0','1','0','1';

---- 84
INSERT INTO public."BuffonDistPostoComb" ("fk_id_scrape_posto", "fk_id_scrape_base", "fk_id_scrape_dist", "bEtanol", "bGasComum", "bGasAdit", "bDieselS500", "bDieselS500Aditivado", "bDieselS10", "bDieselS10Adit", "bGasolinaPremium") SELECT 41,3,1,'0','1','1','1','0','1','0','0';
---- 25 
INSERT INTO public."BuffonDistPostoComb" ("fk_id_scrape_posto", "fk_id_scrape_base", "fk_id_scrape_dist", "bEtanol", "bGasComum", "bGasAdit", "bDieselS500", "bDieselS500Aditivado", "bDieselS10", "bDieselS10Adit", "bGasolinaPremium") SELECT 20,3,1,'1','1','1','1','0','1','0','0';
---- 28 
INSERT INTO public."BuffonDistPostoComb" ("fk_id_scrape_posto", "fk_id_scrape_base", "fk_id_scrape_dist", "bEtanol", "bGasComum", "bGasAdit", "bDieselS500", "bDieselS500Aditivado", "bDieselS10", "bDieselS10Adit", "bGasolinaPremium") SELECT 21,3,1,'1','1','1','1','0','1','0','0';
---- 74 
INSERT INTO public."BuffonDistPostoComb" ("fk_id_scrape_posto", "fk_id_scrape_base", "fk_id_scrape_dist", "bEtanol", "bGasComum", "bGasAdit", "bDieselS500", "bDieselS500Aditivado", "bDieselS10", "bDieselS10Adit", "bGasolinaPremium") SELECT 39,3,1,'0','1','1','1','0','1','0','0';
---- 76 
INSERT INTO public."BuffonDistPostoComb" ("fk_id_scrape_posto", "fk_id_scrape_base", "fk_id_scrape_dist", "bEtanol", "bGasComum", "bGasAdit", "bDieselS500", "bDieselS500Aditivado", "bDieselS10", "bDieselS10Adit", "bGasolinaPremium") SELECT 40,3,1,'0','1','1','1','0','1','0','0';
---- 103 
INSERT INTO public."BuffonDistPostoComb" ("fk_id_scrape_posto", "fk_id_scrape_base", "fk_id_scrape_dist", "bEtanol", "bGasComum", "bGasAdit", "bDieselS500", "bDieselS500Aditivado", "bDieselS10", "bDieselS10Adit", "bGasolinaPremium") SELECT 47,3,1,'0','1','1','1','0','1','0','0';

---- 23 
INSERT INTO public."BuffonDistPostoComb" ("fk_id_scrape_posto", "fk_id_scrape_base", "fk_id_scrape_dist", "bEtanol", "bGasComum", "bGasAdit", "bDieselS500", "bDieselS500Aditivado", "bDieselS10", "bDieselS10Adit", "bGasolinaPremium") SELECT 19,3,1,'0','1','1','1','0','1','0','0';
---- 31
INSERT INTO public."BuffonDistPostoComb" ("fk_id_scrape_posto", "fk_id_scrape_base", "fk_id_scrape_dist", "bEtanol", "bGasComum", "bGasAdit", "bDieselS500", "bDieselS500Aditivado", "bDieselS10", "bDieselS10Adit", "bGasolinaPremium") SELECT 23,3,1,'1','1','1','1','0','1','0','0';
---- 92 
INSERT INTO public."BuffonDistPostoComb" ("fk_id_scrape_posto", "fk_id_scrape_base", "fk_id_scrape_dist", "bEtanol", "bGasComum", "bGasAdit", "bDieselS500", "bDieselS500Aditivado", "bDieselS10", "bDieselS10Adit", "bGasolinaPremium") SELECT 43,3,1,'0','1','1','1','0','1','0','0';
---- 93 
INSERT INTO public."BuffonDistPostoComb" ("fk_id_scrape_posto", "fk_id_scrape_base", "fk_id_scrape_dist", "bEtanol", "bGasComum", "bGasAdit", "bDieselS500", "bDieselS500Aditivado", "bDieselS10", "bDieselS10Adit", "bGasolinaPremium") SELECT 44,3,1,'0','1','1','1','0','1','0','0';
---- 94 
INSERT INTO public."BuffonDistPostoComb" ("fk_id_scrape_posto", "fk_id_scrape_base", "fk_id_scrape_dist", "bEtanol", "bGasComum", "bGasAdit", "bDieselS500", "bDieselS500Aditivado", "bDieselS10", "bDieselS10Adit", "bGasolinaPremium") SELECT 45,3,1,'0','1','1','1','0','1','0','0';


----BB
---- 7
INSERT INTO public."BuffonDistPostoComb" ("fk_id_scrape_posto", "fk_id_scrape_base", "fk_id_scrape_dist", "bEtanol", "bGasComum", "bGasAdit", "bDieselS500", "bDieselS500Aditivado", "bDieselS10", "bDieselS10Adit", "bGasolinaPremium") SELECT 1,3,1,'1','1','1','0','0','0','0','0';
---- 12
INSERT INTO public."BuffonDistPostoComb" ("fk_id_scrape_posto", "fk_id_scrape_base", "fk_id_scrape_dist", "bEtanol", "bGasComum", "bGasAdit", "bDieselS500", "bDieselS500Aditivado", "bDieselS10", "bDieselS10Adit", "bGasolinaPremium") SELECT 2,3,1,'0','1','1','0','0','1','0','0';
---- 32
INSERT INTO public."BuffonDistPostoComb" ("fk_id_scrape_posto", "fk_id_scrape_base", "fk_id_scrape_dist", "bEtanol", "bGasComum", "bGasAdit", "bDieselS500", "bDieselS500Aditivado", "bDieselS10", "bDieselS10Adit", "bGasolinaPremium") SELECT 137,3,1,'1','1','1','1','0','1','0','0';
---- 37
INSERT INTO public."BuffonDistPostoComb" ("fk_id_scrape_posto", "fk_id_scrape_base", "fk_id_scrape_dist", "bEtanol", "bGasComum", "bGasAdit", "bDieselS500", "bDieselS500Aditivado", "bDieselS10", "bDieselS10Adit", "bGasolinaPremium") SELECT 3,3,1,'1','1','1','0','0','0','0','0';
---- 38
INSERT INTO public."BuffonDistPostoComb" ("fk_id_scrape_posto", "fk_id_scrape_base", "fk_id_scrape_dist", "bEtanol", "bGasComum", "bGasAdit", "bDieselS500", "bDieselS500Aditivado", "bDieselS10", "bDieselS10Adit", "bGasolinaPremium") SELECT 4,3,1,'1','1','1','0','0','1','0','0';
---- 56
INSERT INTO public."BuffonDistPostoComb" ("fk_id_scrape_posto", "fk_id_scrape_base", "fk_id_scrape_dist", "bEtanol", "bGasComum", "bGasAdit", "bDieselS500", "bDieselS500Aditivado", "bDieselS10", "bDieselS10Adit", "bGasolinaPremium") SELECT 5,3,1,'1','1','1','0','0','1','0','0';
---- 57
INSERT INTO public."BuffonDistPostoComb" ("fk_id_scrape_posto", "fk_id_scrape_base", "fk_id_scrape_dist", "bEtanol", "bGasComum", "bGasAdit", "bDieselS500", "bDieselS500Aditivado", "bDieselS10", "bDieselS10Adit", "bGasolinaPremium") SELECT 6,3,1,'0','1','1','1','0','1','0','0';
---- 59
INSERT INTO public."BuffonDistPostoComb" ("fk_id_scrape_posto", "fk_id_scrape_base", "fk_id_scrape_dist", "bEtanol", "bGasComum", "bGasAdit", "bDieselS500", "bDieselS500Aditivado", "bDieselS10", "bDieselS10Adit", "bGasolinaPremium") SELECT 7,3,1,'0','1','1','1','0','1','0','0';
---- 61
INSERT INTO public."BuffonDistPostoComb" ("fk_id_scrape_posto", "fk_id_scrape_base", "fk_id_scrape_dist", "bEtanol", "bGasComum", "bGasAdit", "bDieselS500", "bDieselS500Aditivado", "bDieselS10", "bDieselS10Adit", "bGasolinaPremium") SELECT 8,3,1,'0','1','1','1','0','1','0','0';
---- 80
INSERT INTO public."BuffonDistPostoComb" ("fk_id_scrape_posto", "fk_id_scrape_base", "fk_id_scrape_dist", "bEtanol", "bGasComum", "bGasAdit", "bDieselS500", "bDieselS500Aditivado", "bDieselS10", "bDieselS10Adit", "bGasolinaPremium") SELECT 9,3,1,'0','1','1','1','0','1','0','0';
---- 90
INSERT INTO public."BuffonDistPostoComb" ("fk_id_scrape_posto", "fk_id_scrape_base", "fk_id_scrape_dist", "bEtanol", "bGasComum", "bGasAdit", "bDieselS500", "bDieselS500Aditivado", "bDieselS10", "bDieselS10Adit", "bGasolinaPremium") SELECT 10,3,1,'0','1','1','1','0','1','0','0';
---- 91
INSERT INTO public."BuffonDistPostoComb" ("fk_id_scrape_posto", "fk_id_scrape_base", "fk_id_scrape_dist", "bEtanol", "bGasComum", "bGasAdit", "bDieselS500", "bDieselS500Aditivado", "bDieselS10", "bDieselS10Adit", "bGasolinaPremium") SELECT 11,3,1,'0','1','1','1','0','1','0','0';
---- 113
INSERT INTO public."BuffonDistPostoComb" ("fk_id_scrape_posto", "fk_id_scrape_base", "fk_id_scrape_dist", "bEtanol", "bGasComum", "bGasAdit", "bDieselS500", "bDieselS500Aditivado", "bDieselS10", "bDieselS10Adit", "bGasolinaPremium") SELECT 131,3,1,'0','1','1','1','0','1','0','0';

---- TRR / CANOAS

---- 7
INSERT INTO public."BuffonDistPostoComb" ("fk_id_scrape_posto", "fk_id_scrape_base", "fk_id_scrape_dist", "bEtanol", "bGasComum", "bGasAdit", "bDieselS500", "bDieselS500Aditivado", "bDieselS10", "bDieselS10Adit", "bGasolinaPremium") SELECT 138,36,6,'1','1','1','0','0','0','0','0';


---------------------------------------------
---- RAIZEN (3)
---------------------------------------------

---- ITAJAI 
---- 15
INSERT INTO public."BuffonDistPostoComb" ("fk_id_scrape_posto", "fk_id_scrape_base", "fk_id_scrape_dist", "bEtanol", "bGasComum", "bGasAdit", "bDieselS500", "bDieselS500Aditivado", "bDieselS10") SELECT 103,20,3,'1','1','1','1','0','1';

---- JARAGUA 
---- 15
INSERT INTO public."BuffonDistPostoComb" ("fk_id_scrape_posto", "fk_id_scrape_base", "fk_id_scrape_dist", "bEtanol", "bGasComum", "bGasAdit", "bDieselS500", "bDieselS500Aditivado", "bDieselS10") SELECT 103,22,3,'1','1','1','1','0','1';

---- ARAUCARIA
---- 15
INSERT INTO public."BuffonDistPostoComb" ("fk_id_scrape_posto", "fk_id_scrape_base", "fk_id_scrape_dist", "bEtanol", "bGasComum", "bGasAdit", "bDieselS500", "bDieselS500Aditivado", "bDieselS10") SELECT 103,21,3,'1','1','1','1','0','1';

---- BRIA
---- 109
INSERT INTO public."BuffonDistPostoComb" ("fk_id_scrape_posto", "fk_id_scrape_base", "fk_id_scrape_dist", "bEtanol", "bGasComum", "bGasAdit", "bDieselS500", "bDieselS500Aditivado", "bDieselS10") SELECT 128,13,3,'1','1','1','1','0','1';

---- CIF
---- 15
INSERT INTO public."BuffonDistPostoComb" ("fk_id_scrape_posto", "fk_id_scrape_base", "fk_id_scrape_dist", "bEtanol", "bGasComum", "bGasAdit", "bDieselS500", "bDieselS500Aditivado", "bDieselS10") SELECT 103,32,3,'1','1','1','1','0','1';

---- CASCAVEL
---- 109
INSERT INTO public."BuffonDistPostoComb" ("fk_id_scrape_posto", "fk_id_scrape_base", "fk_id_scrape_dist", "bEtanol", "bGasComum", "bGasAdit", "bDieselS500", "bDieselS500Aditivado", "bDieselS10") SELECT 128,24,3,'1','1','1','1','0','1';

---- ESTEIO / CANOAS
---- 8
INSERT INTO public."BuffonDistPostoComb" ("fk_id_scrape_posto", "fk_id_scrape_base", "fk_id_scrape_dist", "bEtanol", "bGasComum", "bGasAdit", "bDieselS500", "bDieselS500Aditivado", "bDieselS10") SELECT 101,14,3,'1','1','1','1','0','1';
---- 10
INSERT INTO public."BuffonDistPostoComb" ("fk_id_scrape_posto", "fk_id_scrape_base", "fk_id_scrape_dist", "bEtanol", "bGasComum", "bGasAdit", "bDieselS500", "bDieselS500Aditivado", "bDieselS10") SELECT 102,14,3,'0','1','1','1','0','1';
---- 45
INSERT INTO public."BuffonDistPostoComb" ("fk_id_scrape_posto", "fk_id_scrape_base", "fk_id_scrape_dist", "bEtanol", "bGasComum", "bGasAdit", "bDieselS500", "bDieselS500Aditivado", "bDieselS10") SELECT 104,14,3,'1','1','1','1','0','1';
---- 46
INSERT INTO public."BuffonDistPostoComb" ("fk_id_scrape_posto", "fk_id_scrape_base", "fk_id_scrape_dist", "bEtanol", "bGasComum", "bGasAdit", "bDieselS500", "bDieselS500Aditivado", "bDieselS10") SELECT 105,14,3,'1','1','1','1','0','1';
---- 58
INSERT INTO public."BuffonDistPostoComb" ("fk_id_scrape_posto", "fk_id_scrape_base", "fk_id_scrape_dist", "bEtanol", "bGasComum", "bGasAdit", "bDieselS500", "bDieselS500Aditivado", "bDieselS10") SELECT 106,14,3,'1','1','1','1','0','1';
---- 62
INSERT INTO public."BuffonDistPostoComb" ("fk_id_scrape_posto", "fk_id_scrape_base", "fk_id_scrape_dist", "bEtanol", "bGasComum", "bGasAdit", "bDieselS500", "bDieselS500Aditivado", "bDieselS10") SELECT 107,14,3,'0','1','1','1','0','1';
---- 79
INSERT INTO public."BuffonDistPostoComb" ("fk_id_scrape_posto", "fk_id_scrape_base", "fk_id_scrape_dist", "bEtanol", "bGasComum", "bGasAdit", "bDieselS500", "bDieselS500Aditivado", "bDieselS10") SELECT 111,14,3,'1','1','1','1','0','1';
---- 83
INSERT INTO public."BuffonDistPostoComb" ("fk_id_scrape_posto", "fk_id_scrape_base", "fk_id_scrape_dist", "bEtanol", "bGasComum", "bGasAdit", "bDieselS500", "bDieselS500Aditivado", "bDieselS10") SELECT 112,14,3,'0','1','1','1','0','1';
---- 72
INSERT INTO public."BuffonDistPostoComb" ("fk_id_scrape_posto", "fk_id_scrape_base", "fk_id_scrape_dist", "bEtanol", "bGasComum", "bGasAdit", "bDieselS500", "bDieselS500Aditivado", "bDieselS10") SELECT 108,14,3,'0','1','1','1','0','1';
---- 85
INSERT INTO public."BuffonDistPostoComb" ("fk_id_scrape_posto", "fk_id_scrape_base", "fk_id_scrape_dist", "bEtanol", "bGasComum", "bGasAdit", "bDieselS500", "bDieselS500Aditivado", "bDieselS10") SELECT 113,14,3,'1','1','1','1','0','1';

---- 109
INSERT INTO public."BuffonDistPostoComb" ("fk_id_scrape_posto", "fk_id_scrape_base", "fk_id_scrape_dist", "bEtanol", "bGasComum", "bGasAdit", "bDieselS500", "bDieselS500Aditivado", "bDieselS10") SELECT 128,14,3,'1','1','1','1','0','1';

 
---- BB 
---- 7
INSERT INTO public."BuffonDistPostoComb" ("fk_id_scrape_posto", "fk_id_scrape_base", "fk_id_scrape_dist", "bEtanol", "bGasComum", "bGasAdit", "bDieselS500", "bDieselS500Aditivado", "bDieselS10") SELECT 90,14,3,'1','1','1','0','0','0';
---- 12
INSERT INTO public."BuffonDistPostoComb" ("fk_id_scrape_posto", "fk_id_scrape_base", "fk_id_scrape_dist", "bEtanol", "bGasComum", "bGasAdit", "bDieselS500", "bDieselS500Aditivado", "bDieselS10") SELECT 91,14,3,'1','1','1','0','0','1';
---- 59
INSERT INTO public."BuffonDistPostoComb" ("fk_id_scrape_posto", "fk_id_scrape_base", "fk_id_scrape_dist", "bEtanol", "bGasComum", "bGasAdit", "bDieselS500", "bDieselS500Aditivado", "bDieselS10") SELECT 96,14,3,'1','1','1','1','0','1';
---- 91
INSERT INTO public."BuffonDistPostoComb" ("fk_id_scrape_posto", "fk_id_scrape_base", "fk_id_scrape_dist", "bEtanol", "bGasComum", "bGasAdit", "bDieselS500", "bDieselS500Aditivado", "bDieselS10") SELECT 100,14,3,'0','1','1','1','0','1';
---- 32
INSERT INTO public."BuffonDistPostoComb" ("fk_id_scrape_posto", "fk_id_scrape_base", "fk_id_scrape_dist", "bEtanol", "bGasComum", "bGasAdit", "bDieselS500", "bDieselS500Aditivado", "bDieselS10") SELECT 134,14,3,'1','1','1','1','0','1';
---- 37
INSERT INTO public."BuffonDistPostoComb" ("fk_id_scrape_posto", "fk_id_scrape_base", "fk_id_scrape_dist", "bEtanol", "bGasComum", "bGasAdit", "bDieselS500", "bDieselS500Aditivado", "bDieselS10") SELECT 92,14,3,'1','1','1','0','0','0';
---- 38
INSERT INTO public."BuffonDistPostoComb" ("fk_id_scrape_posto", "fk_id_scrape_base", "fk_id_scrape_dist", "bEtanol", "bGasComum", "bGasAdit", "bDieselS500", "bDieselS500Aditivado", "bDieselS10") SELECT 93,14,3,'1','1','1','0','0','1';
---- 56
INSERT INTO public."BuffonDistPostoComb" ("fk_id_scrape_posto", "fk_id_scrape_base", "fk_id_scrape_dist", "bEtanol", "bGasComum", "bGasAdit", "bDieselS500", "bDieselS500Aditivado", "bDieselS10") SELECT 94,14,3,'1','1','1','1','0','1';
---- 57
INSERT INTO public."BuffonDistPostoComb" ("fk_id_scrape_posto", "fk_id_scrape_base", "fk_id_scrape_dist", "bEtanol", "bGasComum", "bGasAdit", "bDieselS500", "bDieselS500Aditivado", "bDieselS10") SELECT 95,14,3,'0','1','1','1','0','1';
---- 61
INSERT INTO public."BuffonDistPostoComb" ("fk_id_scrape_posto", "fk_id_scrape_base", "fk_id_scrape_dist", "bEtanol", "bGasComum", "bGasAdit", "bDieselS500", "bDieselS500Aditivado", "bDieselS10") SELECT 97,14,3,'0','1','1','1','0','1';
---- 80
INSERT INTO public."BuffonDistPostoComb" ("fk_id_scrape_posto", "fk_id_scrape_base", "fk_id_scrape_dist", "bEtanol", "bGasComum", "bGasAdit", "bDieselS500", "bDieselS500Aditivado", "bDieselS10") SELECT 98,14,3,'0','1','1','1','0','1';
---- 90
INSERT INTO public."BuffonDistPostoComb" ("fk_id_scrape_posto", "fk_id_scrape_base", "fk_id_scrape_dist", "bEtanol", "bGasComum", "bGasAdit", "bDieselS500", "bDieselS500Aditivado", "bDieselS10") SELECT 99,14,3,'0','1','1','1','0','1';

---- TRR / BEST (raizen)

INSERT INTO public."BuffonDistPostoComb" ("fk_id_scrape_posto", "fk_id_scrape_base", "fk_id_scrape_dist", "bEtanol", "bGasComum", "bGasAdit", "bDieselS500", "bDieselS500Aditivado", "bDieselS10", "bDieselS10Adit", "bGasolinaPremium") SELECT 160,45,8,'1','1','1','0','0','0','0','0';

---- RIO GRANDE
---- BB
---- 7
INSERT INTO public."BuffonDistPostoComb" ("fk_id_scrape_posto", "fk_id_scrape_base", "fk_id_scrape_dist", "bEtanol", "bGasComum", "bGasAdit", "bDieselS500", "bDieselS500Aditivado", "bDieselS10") SELECT 90,18,3,'1','1','1','0','0','0';
---- 12
INSERT INTO public."BuffonDistPostoComb" ("fk_id_scrape_posto", "fk_id_scrape_base", "fk_id_scrape_dist", "bEtanol", "bGasComum", "bGasAdit", "bDieselS500", "bDieselS500Aditivado", "bDieselS10") SELECT 91,18,3,'0','1','1','1','0','1';
---- 59
INSERT INTO public."BuffonDistPostoComb" ("fk_id_scrape_posto", "fk_id_scrape_base", "fk_id_scrape_dist", "bEtanol", "bGasComum", "bGasAdit", "bDieselS500", "bDieselS500Aditivado", "bDieselS10") SELECT 96,18,3,'0','1','1','1','0','1';
---- 91
INSERT INTO public."BuffonDistPostoComb" ("fk_id_scrape_posto", "fk_id_scrape_base", "fk_id_scrape_dist", "bEtanol", "bGasComum", "bGasAdit", "bDieselS500", "bDieselS500Aditivado", "bDieselS10") SELECT 100,18,3,'0','1','1','1','0','1';

------------------------------------------------
---- STA MARIA
---- 72
INSERT INTO public."BuffonDistPostoComb" ("fk_id_scrape_posto", "fk_id_scrape_base", "fk_id_scrape_dist", "bEtanol", "bGasComum", "bGasAdit", "bDieselS500", "bDieselS500Aditivado", "bDieselS10") SELECT 108,19,3,'0','1','1','1','0','1';

------------------------------------------------
---- PASSO FUNDO
---- 73
INSERT INTO public."BuffonDistPostoComb" ("fk_id_scrape_posto", "fk_id_scrape_base", "fk_id_scrape_dist", "bEtanol", "bGasComum", "bGasAdit", "bDieselS500", "bDieselS500Aditivado", "bDieselS10") SELECT 109,15,3,'0','1','1','1','0','1';
---- 78
INSERT INTO public."BuffonDistPostoComb" ("fk_id_scrape_posto", "fk_id_scrape_base", "fk_id_scrape_dist", "bEtanol", "bGasComum", "bGasAdit", "bDieselS500", "bDieselS500Aditivado", "bDieselS10") SELECT 110,15,3,'0','1','1','1','0','1';
---- 85
INSERT INTO public."BuffonDistPostoComb" ("fk_id_scrape_posto", "fk_id_scrape_base", "fk_id_scrape_dist", "bEtanol", "bGasComum", "bGasAdit", "bDieselS500", "bDieselS500Aditivado", "bDieselS10") SELECT 113,15,3,'1','1','1','1','1','1';
---- 109
INSERT INTO public."BuffonDistPostoComb" ("fk_id_scrape_posto", "fk_id_scrape_base", "fk_id_scrape_dist", "bEtanol", "bGasComum", "bGasAdit", "bDieselS500", "bDieselS500Aditivado", "bDieselS10") SELECT 128,15,3,'1','1','1','1','0','1';

---------------------------------------------
---- VIBRA (2)
---------------------------------------------

---- BANOAS

---- 13
INSERT INTO public."BuffonDistPostoComb" ("fk_id_scrape_posto", "fk_id_scrape_base", "fk_id_scrape_dist", "bEtanol", "bGasComum", "bGasAdit", "bDieselS500", "bDieselS500Aditivado", "bDieselS10", "bDieselS10Adit", "bGasolinaPremium") SELECT 52,9,2,'0','1','1','1','0','1','0','0';
---- 18
INSERT INTO public."BuffonDistPostoComb" ("fk_id_scrape_posto", "fk_id_scrape_base", "fk_id_scrape_dist", "bEtanol", "bGasComum", "bGasAdit", "bDieselS500", "bDieselS500Aditivado", "bDieselS10", "bDieselS10Adit", "bGasolinaPremium") SELECT 55,9,2,'1','1','1','0','0','1','0','1';
---- 19
INSERT INTO public."BuffonDistPostoComb" ("fk_id_scrape_posto", "fk_id_scrape_base", "fk_id_scrape_dist", "bEtanol", "bGasComum", "bGasAdit", "bDieselS500", "bDieselS500Aditivado", "bDieselS10", "bDieselS10Adit", "bGasolinaPremium") SELECT 49,9,2,'1','1','1','0','0','1','0','1';
---- 26
INSERT INTO public."BuffonDistPostoComb" ("fk_id_scrape_posto", "fk_id_scrape_base", "fk_id_scrape_dist", "bEtanol", "bGasComum", "bGasAdit", "bDieselS500", "bDieselS500Aditivado", "bDieselS10", "bDieselS10Adit", "bGasolinaPremium") SELECT 57,9,2,'1','1','1','1','0','1','0','0';
---- 30
INSERT INTO public."BuffonDistPostoComb" ("fk_id_scrape_posto", "fk_id_scrape_base", "fk_id_scrape_dist", "bEtanol", "bGasComum", "bGasAdit", "bDieselS500", "bDieselS500Aditivado", "bDieselS10", "bDieselS10Adit", "bGasolinaPremium") SELECT 59,9,2,'0','1','1','1','0','1','0','0';
---- 34
INSERT INTO public."BuffonDistPostoComb" ("fk_id_scrape_posto", "fk_id_scrape_base", "fk_id_scrape_dist", "bEtanol", "bGasComum", "bGasAdit", "bDieselS500", "bDieselS500Aditivado", "bDieselS10", "bDieselS10Adit", "bGasolinaPremium") SELECT 56,9,2,'1','1','1','1','0','1','0','1';
---- 35
INSERT INTO public."BuffonDistPostoComb" ("fk_id_scrape_posto", "fk_id_scrape_base", "fk_id_scrape_dist", "bEtanol", "bGasComum", "bGasAdit", "bDieselS500", "bDieselS500Aditivado", "bDieselS10", "bDieselS10Adit", "bGasolinaPremium") SELECT 60,9,2,'1','1','1','1','0','1','0','1';
---- 47
INSERT INTO public."BuffonDistPostoComb" ("fk_id_scrape_posto", "fk_id_scrape_base", "fk_id_scrape_dist", "bEtanol", "bGasComum", "bGasAdit", "bDieselS500", "bDieselS500Aditivado", "bDieselS10", "bDieselS10Adit", "bGasolinaPremium") SELECT 61,9,2,'1','1','1','1','0','1','0','1';
---- 48
INSERT INTO public."BuffonDistPostoComb" ("fk_id_scrape_posto", "fk_id_scrape_base", "fk_id_scrape_dist", "bEtanol", "bGasComum", "bGasAdit", "bDieselS500", "bDieselS500Aditivado", "bDieselS10", "bDieselS10Adit", "bGasolinaPremium") SELECT 62,9,2,'0','1','1','1','0','1','0','0';
---- 50
INSERT INTO public."BuffonDistPostoComb" ("fk_id_scrape_posto", "fk_id_scrape_base", "fk_id_scrape_dist", "bEtanol", "bGasComum", "bGasAdit", "bDieselS500", "bDieselS500Aditivado", "bDieselS10", "bDieselS10Adit", "bGasolinaPremium") SELECT 64,9,2,'1','1','1','0','0','0','0','1';
---- 64
INSERT INTO public."BuffonDistPostoComb" ("fk_id_scrape_posto", "fk_id_scrape_base", "fk_id_scrape_dist", "bEtanol", "bGasComum", "bGasAdit", "bDieselS500", "bDieselS500Aditivado", "bDieselS10", "bDieselS10Adit", "bGasolinaPremium") SELECT 68,9,2,'0','1','1','1','0','1','0','1';
---- 81
INSERT INTO public."BuffonDistPostoComb" ("fk_id_scrape_posto", "fk_id_scrape_base", "fk_id_scrape_dist", "bEtanol", "bGasComum", "bGasAdit", "bDieselS500", "bDieselS500Aditivado", "bDieselS10", "bDieselS10Adit", "bGasolinaPremium") SELECT 72,9,2,'0','1','1','1','0','1','0','0';
---- 82
INSERT INTO public."BuffonDistPostoComb" ("fk_id_scrape_posto", "fk_id_scrape_base", "fk_id_scrape_dist", "bEtanol", "bGasComum", "bGasAdit", "bDieselS500", "bDieselS500Aditivado", "bDieselS10", "bDieselS10Adit", "bGasolinaPremium") SELECT 73,9,2,'1','1','1','1','0','1','0','0';
---- 98
INSERT INTO public."BuffonDistPostoComb" ("fk_id_scrape_posto", "fk_id_scrape_base", "fk_id_scrape_dist", "bEtanol", "bGasComum", "bGasAdit", "bDieselS500", "bDieselS500Aditivado", "bDieselS10", "bDieselS10Adit", "bGasolinaPremium") SELECT 51,9,2,'0','1','1','1','0','1','0','0';
---- 95
INSERT INTO public."BuffonDistPostoComb" ("fk_id_scrape_posto", "fk_id_scrape_base", "fk_id_scrape_dist", "bEtanol", "bGasComum", "bGasAdit", "bDieselS500", "bDieselS500Aditivado", "bDieselS10", "bDieselS10Adit", "bGasolinaPremium") SELECT 76,9,2,'0','1','1','1','0','1','0','0';
---- 52
INSERT INTO public."BuffonDistPostoComb" ("fk_id_scrape_posto", "fk_id_scrape_base", "fk_id_scrape_dist", "bEtanol", "bGasComum", "bGasAdit", "bDieselS500", "bDieselS500Aditivado", "bDieselS10", "bDieselS10Adit", "bGasolinaPremium") SELECT 66,9,2,'1','1','1','1','0','0','0','0';

---- extras
---- 4
INSERT INTO public."BuffonDistPostoComb" ("fk_id_scrape_posto", "fk_id_scrape_base", "fk_id_scrape_dist", "bEtanol", "bGasComum", "bGasAdit", "bDieselS500", "bDieselS500Aditivado", "bDieselS10", "bDieselS10Adit", "bGasolinaPremium") SELECT 53,9,2,'0','1','1','1','0','1','0','0';
---- 53
INSERT INTO public."BuffonDistPostoComb" ("fk_id_scrape_posto", "fk_id_scrape_base", "fk_id_scrape_dist", "bEtanol", "bGasComum", "bGasAdit", "bDieselS500", "bDieselS500Aditivado", "bDieselS10", "bDieselS10Adit", "bGasolinaPremium") SELECT 50,9,2,'0','1','1','1','0','1','0','0';
---- 65
INSERT INTO public."BuffonDistPostoComb" ("fk_id_scrape_posto", "fk_id_scrape_base", "fk_id_scrape_dist", "bEtanol", "bGasComum", "bGasAdit", "bDieselS500", "bDieselS500Aditivado", "bDieselS10", "bDieselS10Adit", "bGasolinaPremium") SELECT 69,9,2,'1','1','1','1','0','1','0','0';

---- extras 2
---- 2 ok
INSERT INTO public."BuffonDistPostoComb" ("fk_id_scrape_posto", "fk_id_scrape_base", "fk_id_scrape_dist", "bEtanol", "bGasComum", "bGasAdit", "bDieselS500", "bDieselS500Aditivado", "bDieselS10", "bDieselS10Adit", "bGasolinaPremium") SELECT 54,9,2,'0','1','1','1','0','1','0','0';
---- 17 ok
INSERT INTO public."BuffonDistPostoComb" ("fk_id_scrape_posto", "fk_id_scrape_base", "fk_id_scrape_dist", "bEtanol", "bGasComum", "bGasAdit", "bDieselS500", "bDieselS500Aditivado", "bDieselS10", "bDieselS10Adit", "bGasolinaPremium") SELECT 53,9,2,'0','1','1','1','0','1','0','0';
---- 22
INSERT INTO public."BuffonDistPostoComb" ("fk_id_scrape_posto", "fk_id_scrape_base", "fk_id_scrape_dist", "bEtanol", "bGasComum", "bGasAdit", "bDieselS500", "bDieselS500Aditivado", "bDieselS10", "bDieselS10Adit", "bGasolinaPremium") SELECT 67,9,2,'0','1','1','1','0','1','0','0';
---- 75
INSERT INTO public."BuffonDistPostoComb" ("fk_id_scrape_posto", "fk_id_scrape_base", "fk_id_scrape_dist", "bEtanol", "bGasComum", "bGasAdit", "bDieselS500", "bDieselS500Aditivado", "bDieselS10", "bDieselS10Adit", "bGasolinaPremium") SELECT 69,9,2,'0','1','1','1','0','1','0','0';
---- 60
INSERT INTO public."BuffonDistPostoComb" ("fk_id_scrape_posto", "fk_id_scrape_base", "fk_id_scrape_dist", "bEtanol", "bGasComum", "bGasAdit", "bDieselS500", "bDieselS500Aditivado", "bDieselS10", "bDieselS10Adit", "bGasolinaPremium") SELECT 73,9,2,'0','1','1','1','0','1','0','0';
---- 4
INSERT INTO public."BuffonDistPostoComb" ("fk_id_scrape_posto", "fk_id_scrape_base", "fk_id_scrape_dist", "bEtanol", "bGasComum", "bGasAdit", "bDieselS500", "bDieselS500Aditivado", "bDieselS10", "bDieselS10Adit", "bGasolinaPremium") SELECT 55,9,2,'0','1','1','1','0','1','0','0';
---- 18
INSERT INTO public."BuffonDistPostoComb" ("fk_id_scrape_posto", "fk_id_scrape_base", "fk_id_scrape_dist", "bEtanol", "bGasComum", "bGasAdit", "bDieselS500", "bDieselS500Aditivado", "bDieselS10", "bDieselS10Adit", "bGasolinaPremium") SELECT 58,9,2,'0','1','1','1','0','1','0','0';
---- 82
INSERT INTO public."BuffonDistPostoComb" ("fk_id_scrape_posto", "fk_id_scrape_base", "fk_id_scrape_dist", "bEtanol", "bGasComum", "bGasAdit", "bDieselS500", "bDieselS500Aditivado", "bDieselS10", "bDieselS10Adit", "bGasolinaPremium") SELECT 63,9,2,'0','1','1','1','0','1','0','0';
---- 53
INSERT INTO public."BuffonDistPostoComb" ("fk_id_scrape_posto", "fk_id_scrape_base", "fk_id_scrape_dist", "bEtanol", "bGasComum", "bGasAdit", "bDieselS500", "bDieselS500Aditivado", "bDieselS10", "bDieselS10Adit", "bGasolinaPremium") SELECT 50,9,2,'0','1','1','1','0','1','0','0';
---- 65
INSERT INTO public."BuffonDistPostoComb" ("fk_id_scrape_posto", "fk_id_scrape_base", "fk_id_scrape_dist", "bEtanol", "bGasComum", "bGasAdit", "bDieselS500", "bDieselS500Aditivado", "bDieselS10", "bDieselS10Adit", "bGasolinaPremium") SELECT 70,9,2,'0','1','1','1','0','1','0','0';
---- 49
INSERT INTO public."BuffonDistPostoComb" ("fk_id_scrape_posto", "fk_id_scrape_base", "fk_id_scrape_dist", "bEtanol", "bGasComum", "bGasAdit", "bDieselS500", "bDieselS500Aditivado", "bDieselS10", "bDieselS10Adit", "bGasolinaPremium") SELECT 77,9,2,'0','1','1','1','0','1','0','0';
---- 51
INSERT INTO public."BuffonDistPostoComb" ("fk_id_scrape_posto", "fk_id_scrape_base", "fk_id_scrape_dist", "bEtanol", "bGasComum", "bGasAdit", "bDieselS500", "bDieselS500Aditivado", "bDieselS10", "bDieselS10Adit", "bGasolinaPremium") SELECT 65,9,2,'0','1','1','1','0','1','0','0';
---- 66
INSERT INTO public."BuffonDistPostoComb" ("fk_id_scrape_posto", "fk_id_scrape_base", "fk_id_scrape_dist", "bEtanol", "bGasComum", "bGasAdit", "bDieselS500", "bDieselS500Aditivado", "bDieselS10", "bDieselS10Adit", "bGasolinaPremium") SELECT 71,9,2,'0','1','1','1','0','1','0','0';


---- BB
---- 7
INSERT INTO public."BuffonDistPostoComb" ("fk_id_scrape_posto", "fk_id_scrape_base", "fk_id_scrape_dist", "bEtanol", "bGasComum", "bGasAdit", "bDieselS500", "bDieselS500Aditivado", "bDieselS10", "bDieselS10Adit", "bGasolinaPremium") SELECT 78,9,2,'1','1','1','0','0','0','0','0';
---- 12
INSERT INTO public."BuffonDistPostoComb" ("fk_id_scrape_posto", "fk_id_scrape_base", "fk_id_scrape_dist", "bEtanol", "bGasComum", "bGasAdit", "bDieselS500", "bDieselS500Aditivado", "bDieselS10", "bDieselS10Adit", "bGasolinaPremium") SELECT 79,9,2,'0','1','1','0','0','1','0','0';
---- 59
INSERT INTO public."BuffonDistPostoComb" ("fk_id_scrape_posto", "fk_id_scrape_base", "fk_id_scrape_dist", "bEtanol", "bGasComum", "bGasAdit", "bDieselS500", "bDieselS500Aditivado", "bDieselS10", "bDieselS10Adit", "bGasolinaPremium") SELECT 83,9,2,'0','1','1','1','0','1','0','0';
---- 91
INSERT INTO public."BuffonDistPostoComb" ("fk_id_scrape_posto", "fk_id_scrape_base", "fk_id_scrape_dist", "bEtanol", "bGasComum", "bGasAdit", "bDieselS500", "bDieselS500Aditivado", "bDieselS10", "bDieselS10Adit", "bGasolinaPremium") SELECT 88,9,2,'0','1','1','1','0','1','0','0';
---- 37
INSERT INTO public."BuffonDistPostoComb" ("fk_id_scrape_posto", "fk_id_scrape_base", "fk_id_scrape_dist", "bEtanol", "bGasComum", "bGasAdit", "bDieselS500", "bDieselS500Aditivado", "bDieselS10", "bDieselS10Adit", "bGasolinaPremium") SELECT 80,9,2,'1','1','1','0','0','0','0','0';
---- 38
INSERT INTO public."BuffonDistPostoComb" ("fk_id_scrape_posto", "fk_id_scrape_base", "fk_id_scrape_dist", "bEtanol", "bGasComum", "bGasAdit", "bDieselS500", "bDieselS500Aditivado", "bDieselS10", "bDieselS10Adit", "bGasolinaPremium") SELECT 81,9,2,'1','1','1','0','0','1','0','0';
---- 56
INSERT INTO public."BuffonDistPostoComb" ("fk_id_scrape_posto", "fk_id_scrape_base", "fk_id_scrape_dist", "bEtanol", "bGasComum", "bGasAdit", "bDieselS500", "bDieselS500Aditivado", "bDieselS10", "bDieselS10Adit", "bGasolinaPremium") SELECT 82,9,2,'1','1','1','0','0','1','0','0';
---- 57
INSERT INTO public."BuffonDistPostoComb" ("fk_id_scrape_posto", "fk_id_scrape_base", "fk_id_scrape_dist", "bEtanol", "bGasComum", "bGasAdit", "bDieselS500", "bDieselS500Aditivado", "bDieselS10", "bDieselS10Adit", "bGasolinaPremium") SELECT 89,9,2,'0','1','1','1','0','1','0','0';
---- 61
INSERT INTO public."BuffonDistPostoComb" ("fk_id_scrape_posto", "fk_id_scrape_base", "fk_id_scrape_dist", "bEtanol", "bGasComum", "bGasAdit", "bDieselS500", "bDieselS500Aditivado", "bDieselS10", "bDieselS10Adit", "bGasolinaPremium") SELECT 84,9,2,'1','1','1','1','0','1','0','0';
---- 80
INSERT INTO public."BuffonDistPostoComb" ("fk_id_scrape_posto", "fk_id_scrape_base", "fk_id_scrape_dist", "bEtanol", "bGasComum", "bGasAdit", "bDieselS500", "bDieselS500Aditivado", "bDieselS10", "bDieselS10Adit", "bGasolinaPremium") SELECT 85,9,2,'0','1','1','1','0','1','0','0';
---- 90
INSERT INTO public."BuffonDistPostoComb" ("fk_id_scrape_posto", "fk_id_scrape_base", "fk_id_scrape_dist", "bEtanol", "bGasComum", "bGasAdit", "bDieselS500", "bDieselS500Aditivado", "bDieselS10", "bDieselS10Adit", "bGasolinaPremium") SELECT 86,9,2,'0','1','1','1','0','1','0','0';
---- 32
INSERT INTO public."BuffonDistPostoComb" ("fk_id_scrape_posto", "fk_id_scrape_base", "fk_id_scrape_dist", "bEtanol", "bGasComum", "bGasAdit", "bDieselS500", "bDieselS500Aditivado", "bDieselS10", "bDieselS10Adit", "bGasolinaPremium") SELECT 135,9,2,'0','1','1','1','0','1','0','0';

---- TRR / BANOAS (Vibra)

INSERT INTO public."BuffonDistPostoComb" ("fk_id_scrape_posto", "fk_id_scrape_base", "fk_id_scrape_dist", "bEtanol", "bGasComum", "bGasAdit", "bDieselS500", "bDieselS500Aditivado", "bDieselS10", "bDieselS10Adit", "bGasolinaPremium") SELECT 149,39,7,'1','1','1','0','0','0','0','0';

---- AIPAF 

---- 2
INSERT INTO public."BuffonDistPostoComb" ("fk_id_scrape_posto", "fk_id_scrape_base", "fk_id_scrape_dist", "bEtanol", "bGasComum", "bGasAdit", "bDieselS500", "bDieselS500Aditivado", "bDieselS10", "bDieselS10Adit", "bGasolinaPremium") SELECT 77,10,2,'1','1','1','1','0','1','0','0';
---- 17
INSERT INTO public."BuffonDistPostoComb" ("fk_id_scrape_posto", "fk_id_scrape_base", "fk_id_scrape_dist", "bEtanol", "bGasComum", "bGasAdit", "bDieselS500", "bDieselS500Aditivado", "bDieselS10", "bDieselS10Adit", "bGasolinaPremium") SELECT 54,10,2,'1','1','1','1','0','1','0','0';
---- 22
INSERT INTO public."BuffonDistPostoComb" ("fk_id_scrape_posto", "fk_id_scrape_base", "fk_id_scrape_dist", "bEtanol", "bGasComum", "bGasAdit", "bDieselS500", "bDieselS500Aditivado", "bDieselS10", "bDieselS10Adit", "bGasolinaPremium") SELECT 58,10,2,'0','1','1','1','0','1','0','0';
---- 60
INSERT INTO public."BuffonDistPostoComb" ("fk_id_scrape_posto", "fk_id_scrape_base", "fk_id_scrape_dist", "bEtanol", "bGasComum", "bGasAdit", "bDieselS500", "bDieselS500Aditivado", "bDieselS10", "bDieselS10Adit", "bGasolinaPremium") SELECT 67,10,2,'0','1','1','0','0','1','0','0';
---- 75
INSERT INTO public."BuffonDistPostoComb" ("fk_id_scrape_posto", "fk_id_scrape_base", "fk_id_scrape_dist", "bEtanol", "bGasComum", "bGasAdit", "bDieselS500", "bDieselS500Aditivado", "bDieselS10", "bDieselS10Adit", "bGasolinaPremium") SELECT 71,10,2,'0','1','1','1','0','1','0','0';
---- 87
INSERT INTO public."BuffonDistPostoComb" ("fk_id_scrape_posto", "fk_id_scrape_base", "fk_id_scrape_dist", "bEtanol", "bGasComum", "bGasAdit", "bDieselS500", "bDieselS500Aditivado", "bDieselS10", "bDieselS10Adit", "bGasolinaPremium") SELECT 74,10,2,'0','1','1','0','0','1','0','0';
---- 89
INSERT INTO public."BuffonDistPostoComb" ("fk_id_scrape_posto", "fk_id_scrape_base", "fk_id_scrape_dist", "bEtanol", "bGasComum", "bGasAdit", "bDieselS500", "bDieselS500Aditivado", "bDieselS10", "bDieselS10Adit", "bGasolinaPremium") SELECT 75,10,2,'0','1','1','1','0','1','0','0';

---- ARRIG 

---- 4
INSERT INTO public."BuffonDistPostoComb" ("fk_id_scrape_posto", "fk_id_scrape_base", "fk_id_scrape_dist", "bEtanol", "bGasComum", "bGasAdit", "bDieselS500", "bDieselS500Aditivado", "bDieselS10", "bDieselS10Adit", "bGasolinaPremium") SELECT 53,8,2,'0','1','1','1','0','1','0','0';
---- 18
INSERT INTO public."BuffonDistPostoComb" ("fk_id_scrape_posto", "fk_id_scrape_base", "fk_id_scrape_dist", "bEtanol", "bGasComum", "bGasAdit", "bDieselS500", "bDieselS500Aditivado", "bDieselS10", "bDieselS10Adit", "bGasolinaPremium") SELECT 55,8,2,'1','1','1','0','0','1','0','1';
---- 52
INSERT INTO public."BuffonDistPostoComb" ("fk_id_scrape_posto", "fk_id_scrape_base", "fk_id_scrape_dist", "bEtanol", "bGasComum", "bGasAdit", "bDieselS500", "bDieselS500Aditivado", "bDieselS10", "bDieselS10Adit", "bGasolinaPremium") SELECT 66,8,2,'1','1','1','1','0','0','0','0';
---- 53
INSERT INTO public."BuffonDistPostoComb" ("fk_id_scrape_posto", "fk_id_scrape_base", "fk_id_scrape_dist", "bEtanol", "bGasComum", "bGasAdit", "bDieselS500", "bDieselS500Aditivado", "bDieselS10", "bDieselS10Adit", "bGasolinaPremium") SELECT 50,8,2,'0','1','1','1','0','1','0','0';
---- 65
INSERT INTO public."BuffonDistPostoComb" ("fk_id_scrape_posto", "fk_id_scrape_base", "fk_id_scrape_dist", "bEtanol", "bGasComum", "bGasAdit", "bDieselS500", "bDieselS500Aditivado", "bDieselS10", "bDieselS10Adit", "bGasolinaPremium") SELECT 69,8,2,'1','1','1','1','0','1','0','0';
---- BB 
---- 7
INSERT INTO public."BuffonDistPostoComb" ("fk_id_scrape_posto", "fk_id_scrape_base", "fk_id_scrape_dist", "bEtanol", "bGasComum", "bGasAdit", "bDieselS500", "bDieselS500Aditivado", "bDieselS10", "bDieselS10Adit", "bGasolinaPremium") SELECT 78,8,2,'1','1','1','0','0','0','0','0';
---- 12
INSERT INTO public."BuffonDistPostoComb" ("fk_id_scrape_posto", "fk_id_scrape_base", "fk_id_scrape_dist", "bEtanol", "bGasComum", "bGasAdit", "bDieselS500", "bDieselS500Aditivado", "bDieselS10", "bDieselS10Adit", "bGasolinaPremium") SELECT 79,8,2,'0','1','1','0','0','1','0','0';
---- 59
INSERT INTO public."BuffonDistPostoComb" ("fk_id_scrape_posto", "fk_id_scrape_base", "fk_id_scrape_dist", "bEtanol", "bGasComum", "bGasAdit", "bDieselS500", "bDieselS500Aditivado", "bDieselS10", "bDieselS10Adit", "bGasolinaPremium") SELECT 83,8,2,'0','1','1','1','0','1','0','0';
---- 91
INSERT INTO public."BuffonDistPostoComb" ("fk_id_scrape_posto", "fk_id_scrape_base", "fk_id_scrape_dist", "bEtanol", "bGasComum", "bGasAdit", "bDieselS500", "bDieselS500Aditivado", "bDieselS10", "bDieselS10Adit", "bGasolinaPremium") SELECT 88,8,2,'0','1','1','1','0','1','0','0';

---- BAJUI 

---- 49
INSERT INTO public."BuffonDistPostoComb" ("fk_id_scrape_posto", "fk_id_scrape_base", "fk_id_scrape_dist", "bEtanol", "bGasComum", "bGasAdit", "bDieselS500", "bDieselS500Aditivado", "bDieselS10", "bDieselS10Adit", "bGasolinaPremium") SELECT 63,11,2,'0','1','1','1','0','1','0','0';
---- 51
INSERT INTO public."BuffonDistPostoComb" ("fk_id_scrape_posto", "fk_id_scrape_base", "fk_id_scrape_dist", "bEtanol", "bGasComum", "bGasAdit", "bDieselS500", "bDieselS500Aditivado", "bDieselS10", "bDieselS10Adit", "bGasolinaPremium") SELECT 65,11,2,'1','1','1','1','0','1','0','0';
---- 66
INSERT INTO public."BuffonDistPostoComb" ("fk_id_scrape_posto", "fk_id_scrape_base", "fk_id_scrape_dist", "bEtanol", "bGasComum", "bGasAdit", "bDieselS500", "bDieselS500Aditivado", "bDieselS10", "bDieselS10Adit", "bGasolinaPremium") SELECT 70,11,2,'0','1','1','1','0','1','0','0';
---- 75
INSERT INTO public."BuffonDistPostoComb" ("fk_id_scrape_posto", "fk_id_scrape_base", "fk_id_scrape_dist", "bEtanol", "bGasComum", "bGasAdit", "bDieselS500", "bDieselS500Aditivado", "bDieselS10", "bDieselS10Adit", "bGasolinaPremium") SELECT 71,11,2,'0','1','1','1','0','1','0','0';


---- ARFIG 

---- 95
INSERT INTO public."BuffonDistPostoComb" ("fk_id_scrape_posto", "fk_id_scrape_base", "fk_id_scrape_dist", "bEtanol", "bGasComum", "bGasAdit", "bDieselS500", "bDieselS500Aditivado", "bDieselS10", "bDieselS10Adit", "bGasolinaPremium") SELECT 76,12,2,'0','1','1','1','0','1','0','0';

---- ARFLO

---- 95
INSERT INTO public."BuffonDistPostoComb" ("fk_id_scrape_posto", "fk_id_scrape_base", "fk_id_scrape_dist", "bEtanol", "bGasComum", "bGasAdit", "bDieselS500", "bDieselS500Aditivado", "bDieselS10", "bDieselS10Adit", "bGasolinaPremium") SELECT 76,59,2,'0','1','1','1','0','1','0','0';

---- ARFLO-CIF

---- 95
INSERT INTO public."BuffonDistPostoComb" ("fk_id_scrape_posto", "fk_id_scrape_base", "fk_id_scrape_dist", "bEtanol", "bGasComum", "bGasAdit", "bDieselS500", "bDieselS500Aditivado", "bDieselS10", "bDieselS10Adit", "bGasolinaPremium") SELECT 76,60,2,'0','1','1','1','0','1','0','0';


----------------------------------------
---- RODOIL (4)
----------------------------------------

---- ESTEIO 

---- 16
INSERT INTO public."BuffonDistPostoComb" ("fk_id_scrape_posto", "fk_id_scrape_base", "fk_id_scrape_dist", "bEtanol", "bGasComum", "bGasAdit", "bDieselS500", "bDieselS10") SELECT 115,26,4,'1','1','1','1','1';
---- 20
INSERT INTO public."BuffonDistPostoComb" ("fk_id_scrape_posto", "fk_id_scrape_base", "fk_id_scrape_dist", "bEtanol", "bGasComum", "bGasAdit", "bDieselS500", "bDieselS10") SELECT 116,26,4,'0','1','1','1','1';
---- 24
INSERT INTO public."BuffonDistPostoComb" ("fk_id_scrape_posto", "fk_id_scrape_base", "fk_id_scrape_dist", "bEtanol", "bGasComum", "bGasAdit", "bDieselS500", "bDieselS10") SELECT 117,26,4,'0','1','1','1','1';
---- 27
INSERT INTO public."BuffonDistPostoComb" ("fk_id_scrape_posto", "fk_id_scrape_base", "fk_id_scrape_dist", "bEtanol", "bGasComum", "bGasAdit", "bDieselS500", "bDieselS10") SELECT 118,26,4,'0','1','1','1','1';
---- 77
INSERT INTO public."BuffonDistPostoComb" ("fk_id_scrape_posto", "fk_id_scrape_base", "fk_id_scrape_dist", "bEtanol", "bGasComum", "bGasAdit", "bDieselS500", "bDieselS10") SELECT 119,26,4,'1','1','1','1','1';
---- 101
INSERT INTO public."BuffonDistPostoComb" ("fk_id_scrape_posto", "fk_id_scrape_base", "fk_id_scrape_dist", "bEtanol", "bGasComum", "bGasAdit", "bDieselS500", "bDieselS10") SELECT 122,26,4,'0','1','1','1','1';
---- 106
INSERT INTO public."BuffonDistPostoComb" ("fk_id_scrape_posto", "fk_id_scrape_base", "fk_id_scrape_dist", "bEtanol", "bGasComum", "bGasAdit", "bDieselS500", "bDieselS10") SELECT 124,26,4,'0','1','1','1','1';
---- 105
INSERT INTO public."BuffonDistPostoComb" ("fk_id_scrape_posto", "fk_id_scrape_base", "fk_id_scrape_dist", "bEtanol", "bGasComum", "bGasAdit", "bDieselS500", "bDieselS10") SELECT 123,26,4,'1','1','1','1','1';
---- 107
INSERT INTO public."BuffonDistPostoComb" ("fk_id_scrape_posto", "fk_id_scrape_base", "fk_id_scrape_dist", "bEtanol", "bGasComum", "bGasAdit", "bDieselS500", "bDieselS10") SELECT 125,26,4,'1','1','1','1','1';
---- 97
INSERT INTO public."BuffonDistPostoComb" ("fk_id_scrape_posto", "fk_id_scrape_base", "fk_id_scrape_dist", "bEtanol", "bGasComum", "bGasAdit", "bDieselS500", "bDieselS10") SELECT 121,26,4,'0','1','1','1','1';
---- 110
INSERT INTO public."BuffonDistPostoComb" ("fk_id_scrape_posto", "fk_id_scrape_base", "fk_id_scrape_dist", "bEtanol", "bGasComum", "bGasAdit", "bDieselS500", "bDieselS10") SELECT 126,26,4,'1','1','1','0','1';
---- 111
INSERT INTO public."BuffonDistPostoComb" ("fk_id_scrape_posto", "fk_id_scrape_base", "fk_id_scrape_dist", "bEtanol", "bGasComum", "bGasAdit", "bDieselS500", "bDieselS10") SELECT 127,26,4,'0','1','1','1','1';

---- CEL BARROS 

---- 24
INSERT INTO public."BuffonDistPostoComb" ("fk_id_scrape_posto", "fk_id_scrape_base", "fk_id_scrape_dist", "bEtanol", "bGasComum", "bGasAdit", "bDieselS500", "bDieselS10") SELECT 117,27,4,'0','1','1','1','1';
---- 27
INSERT INTO public."BuffonDistPostoComb" ("fk_id_scrape_posto", "fk_id_scrape_base", "fk_id_scrape_dist", "bEtanol", "bGasComum", "bGasAdit", "bDieselS500", "bDieselS10") SELECT 118,27,4,'0','1','1','1','1';
---- 96
INSERT INTO public."BuffonDistPostoComb" ("fk_id_scrape_posto", "fk_id_scrape_base", "fk_id_scrape_dist", "bEtanol", "bGasComum", "bGasAdit", "bDieselS500", "bDieselS10") SELECT 120,27,4,'0','1','1','1','1';
---- 86
INSERT INTO public."BuffonDistPostoComb" ("fk_id_scrape_posto", "fk_id_scrape_base", "fk_id_scrape_dist", "bEtanol", "bGasComum", "bGasAdit", "bDieselS500", "bDieselS10") SELECT 129,27,4,'0','1','1','1','1';

---- RIO GRANDE 

---- 97
INSERT INTO public."BuffonDistPostoComb" ("fk_id_scrape_posto", "fk_id_scrape_base", "fk_id_scrape_dist", "bEtanol", "bGasComum", "bGasAdit", "bDieselS500", "bDieselS10") SELECT 121,28,4,'0','1','1','1','1';

---- 112
INSERT INTO public."BuffonDistPostoComb" ("fk_id_scrape_posto", "fk_id_scrape_base", "fk_id_scrape_dist", "bEtanol", "bGasComum", "bGasAdit", "bDieselS500", "bDieselS10") SELECT 130,30,4,'1','1','0','1','1';

---------------------------------------------
---- ALE (5)
---------------------------------------------

---- BANOAS

---- 13
INSERT INTO public."BuffonDistPostoComb" ("fk_id_scrape_posto", "fk_id_scrape_base", "fk_id_scrape_dist", "bEtanol", "bGasComum", "bGasAdit", "bDieselS500", "bDieselS500Aditivado", "bDieselS10", "bDieselS10Adit", "bGasolinaPremium") SELECT 114,25,5,'1','1','1','1','0','1','0','0';


update public."BuffonDistPostoComb" set "fkCompany" = 1;

