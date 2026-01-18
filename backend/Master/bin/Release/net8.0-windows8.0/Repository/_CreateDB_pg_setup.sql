
----------------------------------------------------
-- SETUP compra facil bb
----------------------------------------------------

INSERT INTO public."Company" ("stName","stCode","stEletronicInfo","ext_id","bActive","stColorBG","nuType") SELECT 'Teste CF','610701','testecf.com.br','59cb6e2e-3278-474d-877b-98e6c976ab21','1','#ff0000','0' WHERE NOT EXISTS (SELECT 1 FROM public."Company" WHERE "stName" = 'Teste CF');

INSERT INTO public."BusinessUnit" ("fkCompany","stName","bActive") SELECT '1','Porto Alegre','1' WHERE NOT EXISTS (SELECT 1 FROM public."BusinessUnit" WHERE "stName" = 'Porto Alegre' and "fkCompany" = '1');
INSERT INTO public."BusinessArea" ("fkCompany","fkBusinessUnit","stName","bActive") SELECT '1','1','Scraping','1' WHERE NOT EXISTS (SELECT 1 FROM public."BusinessArea" WHERE "stName" = 'Scraping' and "fkCompany" = '1');

INSERT INTO public."User" ("fkCompany","stEmail","stCorporateEmail","nuEmailIndex","stCPF","stName","stPassword","bActive","bAdmin","bManageUsers", "bManageBusiness", "stUserPicture", "stPhoneNumber", "fkBusinessArea", "fkBusinessUnit") SELECT '1','admin@testecf.com.br','admin@testecf.com.br','97100110','90511603053','Admin','142536','1','1','1','1','','', '1','1' WHERE NOT EXISTS (SELECT 1 FROM public."User" WHERE "stEmail" = 'admin@testecf.com.br');

INSERT INTO public."BuffonConfig" ("fkCompany","stCmdScrape","bReprocScrape","dtReprocStart") SELECT '1','','false',null WHERE NOT EXISTS (SELECT 1 FROM public."BuffonConfig" WHERE "fkCompany" = '1');

INSERT INTO public."BuffonRegional" ("fkCompany","stLocal") SELECT '1','Canoas / Esteio' WHERE NOT EXISTS (SELECT 1 FROM public."BuffonRegional" WHERE "fkCompany" = '1' and "stLocal" = 'Canoas / Esteio');
INSERT INTO public."BuffonRegional" ("fkCompany","stLocal") SELECT '1','Rio Grande / RS' WHERE NOT EXISTS (SELECT 1 FROM public."BuffonRegional" WHERE "fkCompany" = '1' and "stLocal" = 'Rio Grande / RS');
INSERT INTO public."BuffonRegional" ("fkCompany","stLocal") SELECT '1','Araucária / PR' WHERE NOT EXISTS (SELECT 1 FROM public."BuffonRegional" WHERE "fkCompany" = '1' and "stLocal" = 'Araucária / PR');

