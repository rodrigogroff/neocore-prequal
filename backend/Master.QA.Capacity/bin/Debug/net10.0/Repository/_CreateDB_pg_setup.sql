
INSERT INTO public."Company" ("stName","client_id","stSecret","bActive") 
SELECT 'Teste NX','7f8a9b2c4d6e1f3a5b7c9d2e4f6a8b1c', '9k4m2n8p1q5r7t3v6w9x2y5z8a1b4c7d9e2f5g8h1j4k7m9n2p5q8r1s4t7u9v2w5x8y1z4','1' 
WHERE NOT EXISTS (SELECT 1 FROM public."Company" WHERE "stName" = 'Teste NX');

INSERT INTO public."User" ("fkCompany","stEmail","stCPF","stName","stPhoneNumber","stPassword","bActive","bAdmin") SELECT 
'1','op@neocore.com.br','90511603053','Rodrigo Groff', '51995152432', '142536', '1', '1' 
WHERE NOT EXISTS (SELECT 1 FROM public."User" WHERE "stEmail" = 'operator@teste.com.br');
